using AkiSister.Characters;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Nodes;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Interop.AutoRegistration;
using AkiSister.Characters;
using STS2RitsuLib.CardTags;
using STS2RitsuLib.Keywords;

namespace AkiSister.AkiSisterCode.Cards.CommonCards;


public class FallingRedLeaves() : AkiSisterCard(0,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override bool ShouldGlowGoldInternal => this.LeafCheck();
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(7m, ValueProp.Move),
        new CardsVar(2)
    ];
    
    //protected override HashSet<CardTag> CanonicalTags =>
    //[
    //    ModCardTagRegistry.GetCardTag(AkiSisterCardKeyWords.RedLeafResonance)
    //];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        AkiSisterCardKeyWords.RedLeafResonance.GetModCardKeyword()
    ];
    
    //protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
    //    ModKeywordRegistry.CreateHoverTip(AkiSisterCardKeyWords.RedLeafResonance)
    //];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_slash", null, "slash_attack.mp3")
            .Execute(choiceContext);
        var cards = CardPile.GetCards(base.Owner, PileType.Hand).ToList();
        if (cards.Count > 0)
        {
            await CardCmd.Discard(choiceContext, cards[0]);
        }
        if (this.LeafCheck())
        {
            await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }
}