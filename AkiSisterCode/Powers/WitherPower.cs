using AkiSister.AkiSisterCode.Relics;
using BaseLib.Extensions;
using BaseLib.Hooks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace AkiSister.AkiSisterCode.Powers;

public class WitherPower : AkiSisterPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("DamageIncrease", 1.3m)];

    public override IEnumerable<HealthBarForecastSegment>
        GetHealthBarForecastSegments(HealthBarForecastContext context) =>
    [
        new HealthBarForecastSegment(CalculateTotalDamageNextTurn(), new Color("BA55D3"), HealthBarForecastDirection.FromRight)
    ];

    //public override Color AmountLabelColor => PowerModel._normalAmountLabelColor;
    
    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (target != base.Owner)
        {
            return 1m;
        }
        if (!props.IsPoweredAttack_())
        {
            return 1m;
        }
        var num = base.DynamicVars["DamageIncrease"].BaseValue;
        var witheredBranches = dealer?.Player?.GetRelic<WitheredBranches>();
        if (witheredBranches != null)
        {
            num = witheredBranches.ModifyWitherMultiplier(target, num, props, dealer, cardSource);
        }
        var power = dealer.GetPower<ReturningWheelofAutumnFrostPower>();
        if (power != null)
        {
            num = power.ModifyWitherMultiplier(target, num, props, dealer, cardSource);
        }
        return num;
    }

    //public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    //{
    //    if (side == CombatSide.Enemy)
    //    {
    //        await PowerCmd.TickDownDuration(this);
    //    }
    //}
    
    private int TriggerCount
    {
        get
        {
            IEnumerable<Creature> source = from c in base.Owner.CombatState.GetOpponentsOf(base.Owner)
                where c.IsAlive
                select c;
            return Math.Min(base.Amount, 1 + source.Sum((Creature a) => a.GetPowerAmount<IndulgenceofAutumnGoddessSistersPower>()));
        }
    }

    public int CalculateTotalDamageNextTurn()
    {
        decimal num = default(decimal);
        int num2 = Math.Min(base.Amount, TriggerCount);
        //var num3 = Amount;
        for (int i = 0; i < num2; i++)
        {
            decimal damage = Amount;
            //num3 -= Math.Max(num3 / 5, 1);
            //decimal damage = base.Amount - i;
            damage = Hook.ModifyDamage(base.Owner.CombatState.RunState, base.Owner.CombatState, base.Owner, null,
                damage, ValueProp.Unblockable | ValueProp.Unpowered, null, ModifyDamageHookType.All,
                CardPreviewMode.None, out IEnumerable<AbstractModel> _);
            num += damage;
        }
        return (int)num;
    }
    
    public override async Task AfterSideTurnStart(CombatSide side, ICombatState combatState)
    {
        if (side != base.Owner.Side)
        {
            return;
        }
        int iterations = TriggerCount;
        for (int i = 0; i < iterations; i++)
        {
            await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner, base.Amount,
                ValueProp.Unblockable | ValueProp.Unpowered, null, null);
            //if (base.Owner.IsAlive)
            //{
            //    await PowerCmd.Apply<WitherPower>(Owner, -Math.Max(base.Amount / 5, 1), null, null);
            //    //await PowerCmd.Decrement(this);
            //}
            //else
            //{
            //    await Cmd.CustomScaledWait(0.1f, 0.25f);
            //}
        }
    }

    public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != base.Owner.Side)
        {
            return;
        }
        int iterations = TriggerCount;
        for (int i = 0; i < iterations; i++)
        {
            if (base.Owner.IsAlive)
            {
                await PowerCmd.Apply<WitherPower>(choiceContext, Owner, -1, null, null);
                //    -Math.Max(base.Amount / 5, 1), null, null);
                //await PowerCmd.Decrement(this);
            }
            else
            {
                await Cmd.CustomScaledWait(0.1f, 0.25f);
            }
        }
    }
}