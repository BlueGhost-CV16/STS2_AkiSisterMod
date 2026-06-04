using AkiSister.Characters;
using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Nodes;
using AkiSister.Characters;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2RitsuLib.CardTags;
using STS2RitsuLib.Keywords;

namespace AkiSister.AkiSisterCode.Cards.CommonCards;


public class LostWindrow() : AkiSisterCard(0,
    CardType.Skill, CardRarity.Common,
    TargetType.None)
{
    protected override bool ShouldGlowGoldInternal => this.LeafCheck();
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
    //protected override HashSet<CardTag> CanonicalTags =>
    //[
    //    ModCardTagRegistry.GetCardTag(AkiSisterCardKeyWords.RedLeafResonance)
    //];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        AkiSisterCardKeyWords.RedLeafResonance.GetModCardKeyword()
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        //ModKeywordRegistry.CreateHoverTip(AkiSisterCardKeyWords.RedLeafResonance),
        HoverTipFactory.FromCard<ShepherdandApricotBlossom>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (this.LeafCheck())
        {
            await Owner.LeafAdd_Hand(CardPile.MaxCardsInHand);
        }
        var cardModel = await CardSelectCmd.FromHand(choiceContext, base.Owner, new CardSelectorPrefs(base.SelectionScreenPrompt, 0, CardPile.MaxCardsInHand), card => CustomMethods.LeafCheck(card), this);
        var cardModels = cardModel.ToList();
        if (cardModels.Count > 0)   
        { 
            await CardCmd.Discard(choiceContext, cardModels);
            await Owner.FlowerAdd(base.CombatState, cardModels.Count);
        }
    }

    protected override void OnUpgrade()
    {
        base.AddKeyword(CardKeyword.Retain);
    }
}