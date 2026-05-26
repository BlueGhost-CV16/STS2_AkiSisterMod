using AkiSister.AkiSisterCode.Relics;
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
using STS2RitsuLib.Interop.AutoRegistration;
using AkiSister.Characters;
using STS2RitsuLib.Combat.HealthBars;

namespace AkiSister.AkiSisterCode.Powers;
[RegisterPower]

public class AutumnAuraPower : AkiSisterPower, IHealthBarForecastSource
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public IEnumerable<HealthBarForecastSegment>
        GetHealthBarForecastSegments(HealthBarForecastContext context)
    {
        return HealthBarForecasts.Single(base.Amount, new Color("FF8C00"), HealthBarForecastGrowthDirection.FromLeft);
    }

    public override async Task AfterSideTurnEndLate(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == CombatSide.Enemy/* && !Owner.HasPower<EternalAutumnPower>()*/)
        {
            await PowerCmd.Apply<AutumnAuraLostPower>(choiceContext, Owner, Math.Max(Amount / 5, 1), Owner, null);
        }
    }
    
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
            PowerCmd.ModifyAmount(new ThrowingPlayerChoiceContext(), this, Owner.Block - amount, null, null);
        }
        else
        {
            if (amount > base.Amount)
            {
                return amount;
            }
            Flash();
            PowerCmd.ModifyAmount(new ThrowingPlayerChoiceContext(), this, -amount, null, null);
        }

        return Math.Min(0, amount);
        //DamageTaken = amount;
        //return Math.Min(Owner.Block != 0 ? Owner.Block : 0, amount);
    }

    //public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    //{
    //    if (power == this && amount <= 0)
    //    {
    //        var num = 0m;
    //        count -= amount;
    //        var witheredBranches = Owner?.Player?.GetRelic<WitheredBranches>();
    //        if (witheredBranches != null)
    //        {
    //            num = count;
    //            count = 0;
    //        }
    //        else
    //        {
    //            if (count >= 2)
    //            {
    //                num = count / 2;
    //                count = 0;
    //            }
    //        }
    //        if (num == 0)
    //        {
    //            return;
    //        }
    //        //if (Owner.HasPower<PoisonedApplePower>())
    //        //{
    //        //    foreach (var enemy in base.CombatState.HittableEnemies)
    //        //    {
    //        //        await PowerCmd.Apply<WitherPower>(choiceContext, enemy, num, base.Owner, null);
    //        //    }
    //        //}
    //        //else
    //        //{
    //            var enemy = base.Owner.Player.RunState.Rng.CombatTargets.NextItem(base.CombatState.HittableEnemies);
    //            if (enemy != null)
    //            {
    //                await PowerCmd.Apply<WitherPower>(choiceContext, enemy, num, base.Owner, null);
    //            }
    //        //}
    //    }
    //}

    //private decimal count = 0;
}