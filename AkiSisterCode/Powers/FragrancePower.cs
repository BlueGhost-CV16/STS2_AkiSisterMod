using AkiSister.AkiSisterCode.Relics;
using BaseLib.Extensions;
using BaseLib.Hooks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace AkiSister.AkiSisterCode.Powers;

public class FragrancePower : AkiSisterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override IEnumerable<HealthBarForecastSegment>
        GetHealthBarForecastSegments(HealthBarForecastContext context) =>
    [
        new HealthBarForecastSegment(base.Amount, new Color("F5DEB3"), HealthBarForecastDirection.FromLeft)
    ];

    public override async Task AfterTurnEndLate(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Enemy && !Owner.HasPower<EternalAutumnPower>())
        {
            await PowerCmd.Apply<FragranceLostPower>(Owner, Math.Max(Amount / 3, 1), Owner, null);
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
    //        await PowerCmd.Apply<FragranceLostPower>(Owner, Amount / 3, Owner, null);
    //        //var num = Amount / 3;
    //        //for (int i = 0; i < num; i++)
    //        //{
    //        //    await PowerCmd.TickDownDuration(this);
    //        //}
    //        //await PowerCmd.TickDownDuration(this);
    //    }
    //}

    public override async Task BeforeDamageReceived(PlayerChoiceContext choiceContext, Creature target, decimal amount, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (target == base.Owner && dealer != null && (props.IsPoweredAttack_() || cardSource is Omnislice))
        {
            if (Owner.Block > 0)
            {
                if (amount <= Owner.Block || amount > base.Amount + Owner.Block)
                {
                    return;
                }
                Flash();
                await CreatureCmd.GainBlock(base.Owner, amount - Owner.Block, ValueProp.Unpowered, null);
                await PowerCmd.ModifyAmount(this, Owner.Block - amount, null, null);
            }
            else
            {
                if (amount > base.Amount)
                {
                    return;
                }
                Flash();
                await CreatureCmd.GainBlock(base.Owner, amount, ValueProp.Unpowered, null);
                await PowerCmd.ModifyAmount(this, -amount, null, null);
            }
            //Flash();
            //var block = amount - Owner.Block;
            //await PowerCmd.ModifyAmount(this, -block, null, null);
            //for (int i = 0; i < amount - Owner.Block; i++)
            //{
            //    PowerCmd.Decrement(this);
            //}
        }
    }

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
                    await PowerCmd.Apply<DrainPower>(enemy, num, base.Owner, null);
                }
            }
            else
            {
                var enemy = base.Owner.Player.RunState.Rng.CombatTargets.NextItem(base.CombatState.HittableEnemies);
                if (enemy != null)
                {
                    await PowerCmd.Apply<DrainPower>(enemy, num, base.Owner, null);
                }
            }
        }
    }

    private decimal count = 0;
}