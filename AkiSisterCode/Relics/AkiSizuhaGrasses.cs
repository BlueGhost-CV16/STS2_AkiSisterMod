using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Nodes;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Relics;

public class AkiSizuhaGrasses() : AkiSisterRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        HoverTipFactory.FromEnchantment<RedLeafMarkEnchantment>().Concat([
            HoverTipFactory.FromCard<ShepherdandApricotBlossom>(),
            //HoverTipFactory.FromKeyword(AkiSisterCardKeyWords.RedLeafMark)
        ]);
    
    // 遗物的数值。替换本地化中的{Cards}。
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];

    //public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    //{
    //    // 这里的DynamicVars.Cards.IntValue为上面设置的CardsVar的数值。
    //    CardModel card = combatState.CreateCard(ModelDb.Card<Soul>(), base.Owner);
    //    CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Draw, addedByPlayer: true, CardPilePosition.Random));
    //    Flash();
    //    //await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.IntValue, player);
    //}
    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants,
        ICombatState combatState)
    {
        if (side != CombatSide.Player)
        {
            return;
        }
        Flash();
        await base.Owner.FlowerAdd_Deck(combatState);
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner)
        {
            return;
        }
        Flash();
        await player.LeafAdd_Hand();
    }
    
    //public override decimal ModifyHandDraw(Player player, decimal count)
    //{
    //    if (player != base.Owner)
    //    {
    //        return count;
    //    }
    //    if (player.Creature.CombatState.RoundNumber > 1)
    //    {
    //        return count;
    //    }
    //    return count + base.DynamicVars.Cards.BaseValue;
    //}

    public override RelicModel? GetUpgradeReplacement()
    {
        return ModelDb.Relic<AkiSizuhaFlowers>();
    }
    
    //public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    //{
    //    if (side == CombatSide.Player)
    //    {
    //        //CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1);
    //        RedLeafMarkEnchantment canonicalLeaf = ModelDb.Enchantment<RedLeafMarkEnchantment>();
    //        var pile = PileType.Hand.GetPile(base.Owner).Cards.ToList();
    //        foreach (var item in pile.Where(item => ModelDb.Enchantment<RedLeafMarkEnchantment>().CanEnchant(item)))
    //        {
    //            //CardCmd.Enchant<RedLeafMarkEnchantment>(item, 1m);
    //            CardCmd.Enchant(canonicalLeaf.ToMutable(), item, base.DynamicVars["RedLeafMarkEnchantment"].IntValue);
    //            break;
    //            //NRun.Instance?.GlobalUi.CardPreviewContainer.AddChildSafely(NCardEnchantVfx.Create(item));
    //        }
    //    }
    //}
}