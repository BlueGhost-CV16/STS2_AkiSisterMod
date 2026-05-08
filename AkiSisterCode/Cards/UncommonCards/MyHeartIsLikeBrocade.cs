using AkiSister.AkiSisterCode.Cards;
using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AkiSister.AkiSisterCode.Cards.UncommonCards;

public class MyHeartIsLikeBrocade() : AkiSisterCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override bool ShouldGlowGoldInternal => Owner.PlayerCombatState!.Hand.Cards.Any(card => card is HarvesterandPearBlossom);
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<FragrancePower>(2),
        new CardsVar(2)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<FragrancePower>(),
        HoverTipFactory.FromCard<HarvesterandPearBlossom>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<FragrancePower>(choiceContext, Owner.Creature, DynamicVars["FragrancePower"].BaseValue, Owner.Creature, this);
        var cards = Owner.PlayerCombatState!.Hand.Cards.Where(card => card is HarvesterandPearBlossom).ToList();
        if (cards.Count > 0)
        {
            await CardCmd.Discard(choiceContext, cards.StableShuffle(base.Owner.RunState.Rng.Shuffle).First());
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["FragrancePower"].UpgradeValueBy(1);
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}