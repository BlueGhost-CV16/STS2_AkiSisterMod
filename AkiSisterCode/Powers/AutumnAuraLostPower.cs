using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Powers;

public class AutumnAuraLostPower : AkiSisterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<AutumnAuraPower>()];
    
    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Enemy)
        {
            await PowerCmd.Remove(this);
            var power = Owner.GetPower<AutumnAuraPower>();
            if (power != null)
                await PowerCmd.ModifyAmount(choiceContext, power, -Math.Min(Amount, power.Amount), null, null);
        }
    }

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        if (power is AutumnAuraPower && power.Owner == Owner && amount < 0)
        {
            await PowerCmd.ModifyAmount(choiceContext, this, Math.Max(-base.Amount, amount), null, null);
        }
    }
}