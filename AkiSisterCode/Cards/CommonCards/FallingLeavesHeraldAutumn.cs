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
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Cards.CommonCards;

public class FallingLeavesHeraldAutumn() : AkiSisterCard(0,
    CardType.Skill, CardRarity.Common,
    TargetType.None)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        HoverTipFactory.FromEnchantment<RedLeafMarkEnchantment>().Concat([
            HoverTipFactory.FromCard<ShepherdandApricotBlossom>()]);

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var cardModel = await CardSelectCmd.FromHand(choiceContext, base.Owner, new CardSelectorPrefs(base.SelectionScreenPrompt, (int)DynamicVars.Cards.BaseValue), card => !CustomMethods.LeafCheck(card), this);
        var cardModels = cardModel.ToList();
        if (cardModels.Count > 0)
        {
            await Owner.LeafAdd_Card(cardModels.ToList());
        }
        await Owner.FlowerAdd(base.CombatState);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}