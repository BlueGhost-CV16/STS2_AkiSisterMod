using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Powers;
using AkiSister.Characters;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AkiSister.AkiSisterCode.Cards.UncommonCards;


public class MyHeartIsLikeAutumn() : AkiSisterCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override bool ShouldGlowGoldInternal => Owner.PlayerCombatState!.Hand.Cards.Any(card => card is ShepherdandApricotBlossom);
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<AutumnAuraPower>(2),
        new CardsVar(2)
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<AutumnAuraPower>(),
        HoverTipFactory.FromCard<ShepherdandApricotBlossom>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<AutumnAuraPower>(choiceContext, Owner.Creature, DynamicVars["AutumnAuraPower"].BaseValue, Owner.Creature, this);
        var cards = Owner.PlayerCombatState!.Hand.Cards.Where(card => card is ShepherdandApricotBlossom).ToList();
        if (cards.Count > 0)
        {
            await CardCmd.Discard(choiceContext, cards.StableShuffle(base.Owner.RunState.Rng.Shuffle).First());
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["AutumnAuraPower"].UpgradeValueBy(1);
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}