using AkiSister.Characters;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Nodes;
using AkiSister.Characters;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2RitsuLib.CardTags;
using STS2RitsuLib.Keywords;

namespace AkiSister.AkiSisterCode.Cards.RareCards;


public class AkiMinorikoGoldenLegend() : AkiSisterCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override bool ShouldGlowGoldInternal => this.PotatoCheck();
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
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
        if (this.PotatoCheck())
        {
            await Owner.PotatoAdd_Card(Owner.PlayerCombatState!.Hand.Cards.ToList());
        }
        var cardModels = play.Card.Owner.PlayerCombatState!.Hand.Cards.Where(card => card.PotatoCheck()).ToList();
        if (cardModels.Count > 0)
        {
            foreach (var cardModel in cardModels)
                cardModel.EnergyCost.AddThisTurnOrUntilPlayed(-1);
        }
    }
    
    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
        //base.AddKeyword(CardKeyword.Retain);
    }
}