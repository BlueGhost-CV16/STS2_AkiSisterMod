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
using STS2RitsuLib.Keywords;

namespace AkiSister.AkiSisterCode.Cards.CommonCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class WarmColorHarvest() : AkiSisterCard(0,
    CardType.Skill, CardRarity.Common,
    TargetType.None)
{
    protected override bool ShouldGlowGoldInternal => this.PotatoCheck();
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        ModKeywordRegistry.CreateHoverTip(AkiSisterCardKeyWords.SweetPotatoResonance),
        HoverTipFactory.FromCard<ShepherdandApricotBlossom>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (this.PotatoCheck())
        {
            await Owner.PotatoAdd_Card(Owner.PlayerCombatState!.Hand.Cards.ToList());
        }
        var cardModel = await CardSelectCmd.FromHand(choiceContext, base.Owner, new CardSelectorPrefs(base.SelectionScreenPrompt, 0, 999), card => CustomMethods.PotatoCheck(card), this);
        var cardModels = cardModel.ToList();
        if (cardModels.Count > 0)
        {
            await CardCmd.Discard(choiceContext, cardModels);
            await Owner.GrassAdd(base.CombatState, cardModels.Count);
        }
    }

    protected override void OnUpgrade()
    {
        base.AddKeyword(CardKeyword.Retain);
    }
}