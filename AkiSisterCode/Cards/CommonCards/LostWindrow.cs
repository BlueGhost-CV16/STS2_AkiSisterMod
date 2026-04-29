using AkiSister.AkiSisterCode.Cards;
using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Nodes;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AkiSister.AkiSisterCode.Cards.CommonCards;

public class LostWindrow() : AkiSisterCard(0,
    CardType.Skill, CardRarity.Common,
    TargetType.None)
{
    protected override bool ShouldGlowGoldInternal => this.LeafCheck();
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromKeyword(AkiSisterCardKeyWords.RedLeafResonance),
        HoverTipFactory.FromCard<ShepherdandApricotBlossom>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (this.LeafCheck())
        {
            await Owner.LeafAdd_Card(Owner.PlayerCombatState!.Hand.Cards.ToList());
        }
        var cardModel = await CardSelectCmd.FromHand(choiceContext, base.Owner, new CardSelectorPrefs(base.SelectionScreenPrompt, 0, 999), card => CustomMethods.LeafCheck(card), this);
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