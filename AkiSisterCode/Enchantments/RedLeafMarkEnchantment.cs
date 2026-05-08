using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Enchantments;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Enchantments;

public class RedLeafMarkEnchantment : AkiSisterEnchantment
{
    public override bool IsStackable => true;
    
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
        return card.Enchantment is null
                or SweetPotatoMarkEnchantment; // && !card.Keywords.Contains(CardKeyword.Unplayable);
    }
    
    protected override void OnEnchant()
    {
        if (!base.Card.Keywords.Contains(CardKeyword.Retain))
        {
            AddRetain = true;
            base.Card.AddKeyword(CardKeyword.Retain);
        }
        if (Card is HarvesterandPearBlossom)
        {
            Creature enemy = Card.Owner.RunState.Rng.CombatTargets.NextItem(Card.CombatState.HittableEnemies);
            PowerCmd.Apply<DrainPower>(new ThrowingPlayerChoiceContext(), enemy, (Card as HarvesterandPearBlossom).DynamicVars["DrainPower"].BaseValue, Card.Owner.Creature, Card);
        }
    }
    
    public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, ICombatState combatState)
    {
        if (side == CombatSide.Player && base.Card.Pile.Type == PileType.Hand)
        {
            StatusChange();
        }
        return Task.CompletedTask;
    }
    
    private bool AddRetain = false;

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

    public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel? source)
    {
        if (card != this.Card || CombatManager.Instance.IsOverOrEnding)
        {
            return;
        }

        if (card.Pile?.Type != PileType.Discard && card.Pile?.Type != PileType.Exhaust)
        {
            return;
        }

        if (!LocalContext.NetId.HasValue)
        {
            return;
        }

        //ICombatState? combatState = card.Owner.Creature.CombatState;
        //if (combatState == null)
        //{
        //    return;
        //}
        
        await PowerCmd.Apply<AutumnAuraPower>(new ThrowingPlayerChoiceContext(), Card.Owner.Creature, DynamicVars["AutumnAuraPower"].BaseValue, Card.Owner.Creature, Card);
        StatusChange();
        card.ClearEnchantmentInternal();
        //card.Enchantment?.ClearInternal();
        
        //return Task.CompletedTask;
        //HookPlayerChoiceContext ctx = new HookPlayerChoiceContext(card, LocalContext.NetId.Value, combatState, GameActionType.Combat);
        //await CardCmd.Exhaust(ctx, card);
    }

    public void StatusChange()
    {
        if (AddRetain)
        {
            AddRetain = false;
            base.Card.RemoveKeyword(CardKeyword.Retain);
            base.Status = EnchantmentStatus.Disabled;
        }
    }
}