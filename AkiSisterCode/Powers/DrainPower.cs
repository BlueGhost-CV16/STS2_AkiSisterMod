using AkiSister.AkiSisterCode.Relics;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Interop.AutoRegistration;
using AkiSister.Characters;
using STS2RitsuLib.Combat.HealthBars;

namespace AkiSister.AkiSisterCode.Powers;


public class DrainPower : AkiSisterPower, IHealthBarForecastSource
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("DamageDecrease", 0.85m)];

    //public override IEnumerable<HealthBarForecastSegment>
    //    GetHealthBarForecastSegments(HealthBarForecastContext context) =>
    //[
    //    new HealthBarForecastSegment(CalculateTotalDamageNextTurn(), new Color("556B2F"), HealthBarForecastDirection.FromRight)
    //];
    public IEnumerable<HealthBarForecastSegment>
        GetHealthBarForecastSegments(HealthBarForecastContext context)
    {
        return HealthBarForecasts.Single(base.Amount, new Color("556B2F"), HealthBarForecastGrowthDirection.FromRight);
    }
    
    //public override Color AmountLabelColor => PowerModel._normalAmountLabelColor;
    
    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (dealer != base.Owner)
        {
            return 1m;
        }
        if (!props.IsPoweredAttack())
        {
            return 1m;
        }
        var num = base.DynamicVars["DamageDecrease"].BaseValue;
        var witheredBranches = target?.Player?.GetRelic<WitheredBranches>();
        if (witheredBranches != null)
        {
            num = witheredBranches.ModifyDrainMultiplier(target, num, props, dealer, cardSource);
        }
        var power = dealer.GetPower<ReturningWheelofAutumnFrostPower>();
        if (power != null)
        {
            num = power.ModifyDrainMultiplier(target, num, props, dealer, cardSource);
        }
        return num;
    }   
    
    private int TriggerCount
    {
        get
        {
            IEnumerable<Creature> source = from c in base.Owner.CombatState.GetOpponentsOf(base.Owner)
                where c.IsAlive
                select c;
            return 1 + source.Sum((Creature a) => a.GetPowerAmount<IndulgenceofAutumnGoddessSistersPower>());
            return Math.Min(base.Amount, 1 + source.Sum((Creature a) => a.GetPowerAmount<IndulgenceofAutumnGoddessSistersPower>()));
        }
    }

    public int CalculateTotalDamageNextTurn()
    {
        decimal num = default(decimal);
        //int num2 = Math.Min(base.Amount, TriggerCount);
        var num3 = Amount;
        for (int i = 0; i < TriggerCount; i++)
        {
            //decimal damage = base.Amount - i;
            decimal damage = num3;
            //num3 -= Math.Max(num3 / 5, 1);
            damage = Hook.ModifyDamage(base.Owner.CombatState.RunState, base.Owner.CombatState, base.Owner, null,
                damage, ValueProp.Unblockable | ValueProp.Unpowered, null, ModifyDamageHookType.All,
                CardPreviewMode.None, out IEnumerable<AbstractModel> _);
            num += damage;
        }
        return (int)num;
    }

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (side != base.Owner.Side)
        {
            return;
        }
        int iterations = TriggerCount;
        var num = Amount;
        for (int i = 0; i < iterations; i++)
        {
            decimal damage = num;
            //num -= Math.Max(num / 5, 1);
            await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner, damage,
                ValueProp.Unblockable | ValueProp.Unpowered, null, null);
            //var num = ;
            //if (base.Owner.IsAlive)
            //{
            //    await PowerCmd.Apply<DrainPower>(Owner, -Math.Max(base.Amount / 5, 1), null, null);
            //    //await PowerCmd.Decrement(this);
            //}
            //else
            //{
            //    await Cmd.CustomScaledWait(0.1f, 0.25f);
            //}
        }
    }

    public override async Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side != base.Owner.Side)
        {
            return;
        }
        int iterations = Math.Min(base.Amount, TriggerCount);
        for (int i = 0; i < iterations; i++)
        {
            if (base.Owner.IsAlive)
            {
                await PowerCmd.Apply<DrainPower>(choiceContext, Owner,// -1, null, null);
                    -Math.Max(base.Amount / 5, 1), null, null);
                //await PowerCmd.Decrement(this);
            }
            else
            {
                await Cmd.CustomScaledWait(0.1f, 0.25f);
            }
        }
    }
}