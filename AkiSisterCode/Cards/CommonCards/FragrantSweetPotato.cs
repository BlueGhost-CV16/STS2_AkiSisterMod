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


public class FragrantSweetPotato() : AkiSisterCard(0,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override bool ShouldGlowGoldInternal => this.PotatoCheck();
    
    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(5m, ValueProp.Move),
        new CardsVar(2)
    ];
    
    //protected override HashSet<CardTag> CanonicalTags =>
    //[
    //    ModCardTagRegistry.GetCardTag(AkiSisterCardKeyWords.SweetPotatoResonance)
    //];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        AkiSisterCardKeyWords.SweetPotatoResonance.GetModCardKeyword()
    ];
    
    //protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
    //    ModKeywordRegistry.CreateHoverTip(AkiSisterCardKeyWords.SweetPotatoResonance)
    //];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        var cards = CardPile.GetCards(base.Owner, PileType.Hand).ToList();
        cards.Reverse();
        if (cards.Count > 0)
        {
            await CardCmd.Discard(choiceContext, cards[0]);
        }
        if (this.PotatoCheck())
        {
            await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3m);
    }
}