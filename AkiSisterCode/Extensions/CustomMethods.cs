using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Extensions;

public static class CustomMethods
{
    
    public static EnchantmentModel? Enchant(EnchantmentModel enchantment, CardModel card, decimal amount = 1)
    {
        enchantment.AssertMutable();
        if (!enchantment.CanEnchant(card))
        {
            throw new InvalidOperationException($"Cannot enchant {card.Id} with {enchantment.Id}.");
        }
        if (card.Enchantment == null)
        {
            card.EnchantInternal(enchantment, amount);
            enchantment.ModifyCard();
        }
        else
        {
            if (!(card.Enchantment.GetType() == enchantment.GetType()))
            {
                throw new InvalidOperationException($"Cannot enchant {card.Id} with {enchantment.Id} because it already has enchantment {card.Enchantment.Id}.");
            }
            card.Enchantment.Amount += (int)amount;
        }
        card.FinalizeUpgradeInternal();
        return card.Enchantment;
    }

    public static bool PotatoCheck(this CardModel card)
    {
        return card.Enchantment is SweetPotatoMarkEnchantment;
    }
    
    public static async Task PotatoAdd_Hand(this Player player, int amount = 1)
    {
        var cards = PileType.Hand.GetPile(player).Cards.ToList();
        cards.Reverse();
        await player.PotatoAdd(cards, amount);
    }
    
    public static async Task PotatoAdd_Card(this CardModel card)
    {
        await card.Owner.PotatoAdd([card]);
    }
    
    public static async Task PotatoAdd_Card(this Player player, CardModel card)
    {
        await player.PotatoAdd([card]);
    }
    
    public static async Task PotatoAdd_Card(this Player player, List<CardModel> cards)
    {
        await player.PotatoAdd(cards, cards.Count);
    }

    private static async Task PotatoAdd(this Player player, List<CardModel> cards, int amount = 1)
    {
        var enchant = ModelDb.Enchantment<SweetPotatoMarkEnchantment>().ToMutable();
        cards = cards.Where(enchant.CanEnchant).ToList();
        //cards = cards.Where(a => enchant.CanEnchant(a) || a.Keywords.Contains(AkiSisterCardKeyWords.SweetPotatoFavor)).ToList();
        //cards.Reverse();
        //for (int i = 0; i < amount && i < cards.Count; i++)
        //{
        //    if (cards[i].Enchantment is RedLeafMarkEnchantment)
        //    {
        //        cards[i].Enchantment?.ClearInternal();
        //        await PowerCmd.Apply<FragrancePower>(player.Creature, 2, player.Creature, null);
        //    }
        //    CustomMethods.Enchant(enchant, cards[i]);
        //    await Cmd.Wait(0.2f);
        //}
        int i = 0;
        foreach (var card in cards.TakeWhile(_ => i < amount))
        {
            var enchant1 = ModelDb.Enchantment<SweetPotatoMarkEnchantment>().ToMutable();
            if (card.LeafCheck())
            {
                var leaf = card.Enchantment as RedLeafMarkEnchantment;
                //card.Enchantment.ClearInternal();
                leaf?.StatusChange();
                card.ClearEnchantmentInternal();
                //await PowerCmd.Apply<AutumnAuraPower>(player.Creature, leaf.DynamicVars["AutumnAuraPower"].BaseValue, player.Creature, null);
            }
            CustomMethods.Enchant(enchant1, card);
            await Cmd.Wait(0.2f);
            //enchant = ModelDb.Enchantment<SweetPotatoMarkEnchantment>().ToMutable();
            i++;
        }
    }

    public static async Task GrassAdd(this Player player, CombatState? combatState, int amount = 1, bool upgrade = false)
    {
        if (combatState == null)
            return;
        //await player.PotatoAdd_Card(card);
        for (int i = 0; i < amount; i++)
        {
            CardModel card = combatState.CreateCard(ModelDb.Card<HarvesterandPearBlossom>(), player);
            if (upgrade)
                CardCmd.Upgrade(card);
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, addedByPlayer: true);
        }
    }

    public static async Task GrassAdd_Deck(this Player player, CombatState? combatState, int amount = 1, bool upgrade = false)
    {
        if (combatState == null)
            return;
        //await player.PotatoAdd_Card(card);
        for (int i = 0; i < amount; i++)
        {
            CardModel card = combatState.CreateCard(ModelDb.Card<HarvesterandPearBlossom>(), player);
            if (upgrade)
                CardCmd.Upgrade(card);
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Draw, addedByPlayer: true));
        }
    }

    public static bool LeafCheck(this CardModel card)
    {
        return card.Enchantment is RedLeafMarkEnchantment;
    }
    
    public static async Task LeafAdd_Hand(this Player player, int amount = 1)
    {
        var cards = PileType.Hand.GetPile(player).Cards.ToList();
        //cards.Reverse();
        await player.LeafAdd(cards, amount);
    }
    
    public static async Task LeafAdd_Card(this CardModel card)
    {
        await card.Owner.LeafAdd([card]);
    }
    
    public static async Task LeafAdd_Card(this Player player, CardModel card)
    {
        await player.LeafAdd([card]);
    }
    
    public static async Task LeafAdd_Card(this Player player, List<CardModel> cards)
    {
        await player.LeafAdd(cards, cards.Count);
    }

    private static async Task LeafAdd(this Player player, List<CardModel> cards, int amount = 1)
    {
        var enchant = ModelDb.Enchantment<RedLeafMarkEnchantment>().ToMutable();
        cards = cards.Where(enchant.CanEnchant).ToList();
        //cards = cards.Where(a => enchant.CanEnchant(a) || a.Keywords.Contains(AkiSisterCardKeyWords.RedLeafFavor)).ToList();
        //cards.Reverse();
        //for (int i = 0; i < amount && i < cards.Count; i++)
        //{
        //    if (cards[i].Enchantment is SweetPotatoMarkEnchantment)
        //    {
        //        cards[i].Enchantment?.ClearInternal();
        //        await PowerCmd.Apply<AutumnAuraPower>(player.Creature, 2, player.Creature, null);
        //    }
        //    CustomMethods.Enchant(enchant, cards[i]);
        //    await Cmd.Wait(0.2f);
        //}
        int i = 0;
        foreach (var card in cards.TakeWhile(_ => i < amount))
        {
            var enchant1 = ModelDb.Enchantment<RedLeafMarkEnchantment>().ToMutable();
            if (card.PotatoCheck())
            {
                var potato = card.Enchantment as SweetPotatoMarkEnchantment;
                //card.Enchantment.ClearInternal();
                potato?.StatusChange();
                card.ClearEnchantmentInternal();
                //await PowerCmd.Apply<FragrancePower>(player.Creature, potato.DynamicVars["FragrancePower"].BaseValue, player.Creature, null);
            }
            CustomMethods.Enchant(enchant1, card);
            await Cmd.Wait(0.2f);
            //enchant = ModelDb.Enchantment<SweetPotatoMarkEnchantment>().ToMutable();
            i++;
        }
    }

    public static async Task FlowerAdd(this Player player, CombatState? combatState, int amount = 1, bool upgrade = false)
    {
        if (combatState == null)
            return;
        //await player.LeafAdd_Card(card);
        for (int i = 0; i < amount; i++)
        {
            CardModel card = combatState.CreateCard(ModelDb.Card<ShepherdandApricotBlossom>(), player);
            if (upgrade && card.IsUpgradable)
                CardCmd.Upgrade(card);
            await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, addedByPlayer: true);
        }
    }

    public static async Task FlowerAdd_Deck(this Player player, CombatState? combatState, int amount = 1, bool upgrade = false)
    {
        if (combatState == null)
            return;
        //await player.LeafAdd_Card(card);
        for (int i = 0; i < amount; i++)
        {
            CardModel card = combatState.CreateCard(ModelDb.Card<ShepherdandApricotBlossom>(), player);
            if (upgrade && card.IsUpgradable)
                CardCmd.Upgrade(card);
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Draw, addedByPlayer: true, CardPilePosition.Top));
        }
    }
}