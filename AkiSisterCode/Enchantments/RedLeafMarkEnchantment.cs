using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Enchantments;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;
using MultiEnchantmentMod;
using MultiEnchantmentMod.Api;

namespace AkiSister.AkiSisterCode.Enchantments;

[Enchantment(Stack = StackBehavior.DisallowDuplicate, Status = StatusAggregation.NotApplicable, Scope = ScopeKind.UntilCombatEnds)]
[EnchantmentKeyword(CardKeyword.Retain, Mode = KeywordEvalMode.Custom)]
public class RedLeafMarkEnchantment : AkiSisterEnchantment
{
    //public override bool IsStackable => false;
    
    //public override bool HasExtraCardText => true;

    public override bool ShowAmount => false;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<AutumnAuraPower>(1m),
        new PowerVar<DrainPower>(1m),
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        //HoverTipFactory.FromPower<AutumnAuraPower>()
    ];
    
    public override bool CanEnchantCardType(CardType cardType)
    {
        return cardType == CardType.Status || base.CanEnchantCardType(cardType);
    }
    
    public override bool CanEnchant(CardModel card)
    {
        return !MultiEnchantmentApi.HasEnchantment<RedLeafMarkEnchantment>(card);
    }
    
    protected override void OnEnchant()
    {
        if (Card is not HarvesterandPearBlossom) return;
        var enemy = Card.Owner.RunState.Rng.CombatTargets.NextItem(Card.CombatState.HittableEnemies);
        PowerCmd.Apply<DrainPower>(new ThrowingPlayerChoiceContext(), enemy, (Card as HarvesterandPearBlossom).DynamicVars["DrainPower"].BaseValue, Card.Owner.Creature, Card);
    }

    //public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants,
    //    ICombatState combatState)
    //{
    //    if (side == CombatSide.Player && base.Card.Pile.Type == PileType.Hand)
    //    {
    //        base.Status = EnchantmentStatus.Disabled;
    //    }
    //    return Task.CompletedTask;
    //}

    //public override Task AfterFlush(PlayerChoiceContext choiceContext, Player player, IReadOnlyCollection<CardModel> flushedCards,
    //    IReadOnlyCollection<CardModel> retainedCards)
    //{
    //    if (retainedCards.Any(card => card == Card))
    //    {
    //        StatusChange();
    //    }
    //    return Task.CompletedTask;
    //}

    //private bool _addRetain = false;

    //public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
    //{
    //    if (card == this.Card)
    //    {
    //        await PowerCmd.Apply<AutumnAuraPower>(choiceContext, Card.Owner.Creature, DynamicVars["AutumnAuraPower"].BaseValue, Card.Owner.Creature, Card);
    //        StatusChange();
    //        card.ClearEnchantmentInternal();
    //    }
    //}
//
    //public override async Task AfterCardDiscarded(PlayerChoiceContext choiceContext, CardModel card)
    //{
    //    if (card == this.Card)
    //    {
    //        await PowerCmd.Apply<AutumnAuraPower>(choiceContext, Card.Owner.Creature, DynamicVars["AutumnAuraPower"].BaseValue, Card.Owner.Creature, Card);
    //        StatusChange();
    //        card.ClearEnchantmentInternal();
    //    }
    //}

    //public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    //{
    //    if (cardPlay.Card == Card)
    //    {
    //        await PowerCmd.Apply<AutumnAuraPower>(choiceContext, Card.Owner.Creature, DynamicVars["AutumnAuraPower"].BaseValue, Card.Owner.Creature, Card);
    //    }
    //}

    //public override async Task BeforeCardPlayed(CardPlay cardPlay)
    //{
    //    if (cardPlay.Card == Card)
    //    {
    //        await PowerCmd.Apply<AutumnAuraPower>(Card.Owner.Creature, DynamicVars["AutumnAuraPower"].BaseValue, Card.Owner.Creature, Card);
    //    }
    //}

    //public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel? source)
    //{
    //    if (card != this.Card || CombatManager.Instance.IsOverOrEnding)
    //    {
    //        return;
    //    }
//
    //    if (card.Pile?.Type != PileType.Discard && card.Pile?.Type != PileType.Exhaust)
    //    {
    //        return;
    //    }
//
    //    if (!LocalContext.NetId.HasValue)
    //    {
    //        return;
    //    }
//
    //    //ICombatState? combatState = card.Owner.Creature.CombatState;
    //    //if (combatState == null)
    //    //{
    //    //    return;
    //    //}
//
    //    await PowerCmd.Apply<AutumnAuraPower>(new ThrowingPlayerChoiceContext(), Card.Owner.Creature,
    //        DynamicVars["AutumnAuraPower"].BaseValue, Card.Owner.Creature, Card);
    //    //StatusChange();
    //    //card.ClearEnchantmentInternal();
    //    //card.Enchantment?.ClearInternal();
    //    //return Task.CompletedTask;
    //    //HookPlayerChoiceContext ctx = new HookPlayerChoiceContext(card, LocalContext.NetId.Value, combatState, GameActionType.Combat);
    //    //await CardCmd.Exhaust(ctx, card);
    //}

    //public void StatusChange()
    //{
    //    base.Status = EnchantmentStatus.Disabled;
    //    //if (!_addRetain) return;
    //    //_addRetain = false;
    //    //base.Card.RemoveKeyword(CardKeyword.Retain);
    //}
}

public class RedLeafMarkEnchantmentDefinition : EnchantmentDefinition<RedLeafMarkEnchantment>
{
    protected override void OnTurnEnd(CardModel card, RedLeafMarkEnchantment enchantment)
    {
        if (card.Pile?.Type != PileType.Hand) return;
        Console.WriteLine("红叶附魔经过OnTurnEnd时点！");
        enchantment.Status = EnchantmentStatus.Disabled;
        base.OnTurnEnd(card, enchantment);
    }

    //protected override void OnBeforeSideTurnStart(CardModel card, RedLeafMarkEnchantment enchantment, CombatSide side)
    //{
    //    if (side != CombatSide.Player || card.Pile?.Type != PileType.Hand) return;
    //    //Console.WriteLine("红叶附魔经过OnBeforeSideTurnStart时点！");
    //    enchantment.Status = EnchantmentStatus.Disabled;
    //    //base.OnBeforeSideTurnStart(card, enchantment, side);
    //}

    //protected override bool OnRemoved(CardModel card, RedLeafMarkEnchantment enchantment, RemovalReason reason)
    //{
    //    enchantment.StatusChange();
    //    return base.OnRemoved(card, enchantment, reason);
    //}

    protected override void OnCardChangedPiles(CardModel card, RedLeafMarkEnchantment enchantment, PileType oldPile, AbstractModel? source)
    {
        base.OnCardChangedPiles(card, enchantment, oldPile, source);
        if (card.Pile?.Type != PileType.Discard && card.Pile?.Type != PileType.Exhaust)
        {
            return;
        }
        if (!LocalContext.NetId.HasValue)
        {
            return;
        }
        PowerCmd.Apply<AutumnAuraPower>(new ThrowingPlayerChoiceContext(), card.Owner.Creature,
            enchantment.DynamicVars["AutumnAuraPower"].BaseValue, card.Owner.Creature, card);
        MultiEnchantmentApi.RemoveEnchantment(card, enchantment);
    }

    //protected override IEnumerable<CardKeyword> TrackedKeywords => [CardKeyword.Retain];
    
    protected override int KeywordSourceAmount(EnchantmentStackSnapshot snapshot, CardKeyword keyword)
    {
        //if (keyword != CardKeyword.Retain)
        //{
        //    return 0;
        //}
        return snapshot.ActiveInstanceCount > 0 ? 1 : 0;
    }

    public override HistoryDisplayMode HistoryDisplay => HistoryDisplayMode.Hidden;
}