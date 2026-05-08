using AkiSister.AkiSisterCode.Relics;
using BaseLib.Extensions;
using BaseLib.Hooks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace AkiSister.AkiSisterCode.Powers;

public class AutumnAuraPower : AkiSisterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override IEnumerable<HealthBarForecastSegment>
        GetHealthBarForecastSegments(HealthBarForecastContext context) =>
    [
        new HealthBarForecastSegment(base.Amount, new Color("FF8C00"), HealthBarForecastDirection.FromLeft)
    ];

    public override async Task AfterTurnEndLate(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Enemy && !Owner.HasPower<EternalAutumnPower>())
        {
            await PowerCmd.Apply<AutumnAuraLostPower>(Owner, Math.Max(Amount / 3, 1), Owner, null);
            //var num = Amount / 3;
            //for (int i = 0; i < num; i++)
            //{
            //    await PowerCmd.TickDownDuration(this);
            //}
        }
    }

    //public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    //{
    //    if (side == CombatSide.Enemy)
    //    {
    //        await PowerCmd.Apply<AutumnAuraLostPower>(Owner, Amount / 3, Owner, null);
    //        //var num = Amount / 3;
    //        //for (int i = 0; i < num; i++)
    //        //{
    //        //    await PowerCmd.TickDownDuration(this);
    //        //}
    //    }
    //}

    //private decimal DamageTaken = 0;
    
    //public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer,
    //    CardModel? cardSource)
    //{
    //    if (Owner != target)
    //    {
    //        return 0m;
    //    }
    //    if (!props.IsPoweredAttack_())
    //    {
    //        return 0m;
    //    }
    //    if (amount > base.Owner.Block && amount - base.Owner.Block <= base.Amount)
    //    {
    //        DamageTaken = amount - base.Owner.Block;
    //        return -amount;
    //    }
    //    return 0m;
    //}
    
    //public override decimal ModifyHpLostAfterOsty(Creature target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    //{
    //    if (!CombatManager.Instance.IsInProgress)
    //    {
    //        return amount;
    //    }
    //    if (target != base.Owner)
    //    {
    //        return amount;
    //    }
    //    if (amount > base.Amount)
    //    {
    //        return amount;
    //    }
    //    DamageTaken = amount;
    //    return Math.Min(0, amount);
    //}
    
    public override decimal ModifyHpLostAfterOstyLate(Creature target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource)
    {
        if (!CombatManager.Instance.IsInProgress && props != ValueProp.Unblockable || target != base.Owner)
        {
            return amount;
        }
        if (Owner.Block > 0)
        {
            if (amount <= Owner.Block || amount > base.Amount + Owner.Block)
            {
                return amount;
            }
            Flash();
            PowerCmd.ModifyAmount(this, Owner.Block - amount, null, null);
        }
        else
        {
            if (amount > base.Amount)
            {
                return amount;
            }
            Flash();
            PowerCmd.ModifyAmount(this, -amount, null, null);
        }

        return Math.Min(0, amount);
        //DamageTaken = amount;
        //return Math.Min(Owner.Block != 0 ? Owner.Block : 0, amount);
    }

    //public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? _, CardModel? __)
    //{
    //    if (target == base.Owner && props.IsPoweredAttack_() && DamageTaken > 0)
    //    {
    //        for (int i = 0; i < DamageTaken; i++)
    //        {
    //            await PowerCmd.Decrement(this);
    //        }
    //        Flash();
    //        DamageTaken = 0;
    //    }
    //}

    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power == this && amount <= 0)
        {
            var num = 0m;
            count -= amount;
            var witheredBranches = Owner?.Player?.GetRelic<WitheredBranches>();
            if (witheredBranches != null)
            {
                num = count;
                count = 0;
            }
            else
            {
                if (count >= 2)
                {
                    num = count / 2;
                    count = 0;
                }
            }
            if (num == 0)
            {
                return;
            }
            if (Owner.HasPower<PoisonedApplePower>())
            {
                foreach (var enemy in base.CombatState.HittableEnemies)
                {
                    await PowerCmd.Apply<WitherPower>(enemy, num, base.Owner, null);
                }
            }
            else
            {
                var enemy = base.Owner.Player.RunState.Rng.CombatTargets.NextItem(base.CombatState.HittableEnemies);
                if (enemy != null)
                {
                    await PowerCmd.Apply<WitherPower>(enemy, num, base.Owner, null);
                }
            }
        }
    }

    private decimal count = 0;
}