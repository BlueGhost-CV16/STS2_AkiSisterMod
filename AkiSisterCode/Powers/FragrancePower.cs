using AkiSister.AkiSisterCode.Relics;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
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

public class FragrancePower : AkiSisterPower, IHealthBarForecastSource
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    //public override IEnumerable<HealthBarForecastSegment>
    //    GetHealthBarForecastSegments(HealthBarForecastContext context) =>
    //[
    //    new HealthBarForecastSegment(base.Amount, new Color("F5DEB3"), HealthBarForecastDirection.FromLeft)
    //];
    public IEnumerable<HealthBarForecastSegment>
        GetHealthBarForecastSegments(HealthBarForecastContext context)
    {
        return HealthBarForecasts.Single(base.Amount, new Color("F5DEB3"), HealthBarForecastGrowthDirection.FromLeft);
    }

    public override async Task AfterSideTurnEndLate(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == CombatSide.Enemy/* && !Owner.HasPower<EternalAutumnPower>()*/)
        {
            await PowerCmd.Apply<FragranceLostPower>(choiceContext, Owner, Math.Max(Amount / 5, 1), Owner, null);
        }
    }

    public override async Task BeforeDamageReceived(PlayerChoiceContext choiceContext, Creature target, decimal amount, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (target == base.Owner && dealer != null && cardSource is Omnislice)
        {
            if (Owner.Block > 0)
            {
                if (amount <= Owner.Block || amount > base.Amount + Owner.Block)
                {
                    return;
                }
                Flash();
                var num = Owner.Block - amount;
                await CreatureCmd.GainBlock(base.Owner, -num, ValueProp.Unpowered, null);
                await PowerCmd.ModifyAmount(choiceContext, this, num, null, null);
            }
            else
            {
                if (amount > base.Amount)
                {
                    return;
                }
                Flash();
                await CreatureCmd.GainBlock(base.Owner, amount, ValueProp.Unpowered, null);
                await PowerCmd.ModifyAmount(choiceContext, this, -amount, null, null);
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

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
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
            //if (Owner.HasPower<PoisonedApplePower>())
            //{
            //    foreach (var enemy in base.CombatState.HittableEnemies)
            //    {
            //        await PowerCmd.Apply<DrainPower>(choiceContext, enemy, num, base.Owner, null);
            //    }
            //}
            //else
            //{
                var enemy = base.Owner.Player.RunState.Rng.CombatTargets.NextItem(base.CombatState.HittableEnemies);
                if (enemy != null)
                {
                    await PowerCmd.Apply<DrainPower>(choiceContext, enemy, num, base.Owner, null);
                }
            //}
        }
    }

    private decimal count = 0;
}