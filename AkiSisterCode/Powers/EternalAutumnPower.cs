using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Powers;


public class EternalAutumnPower : AkiSisterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    [
        HoverTipFactory.FromPower<AutumnAuraPower>(),
        HoverTipFactory.FromPower<AutumnAuraLostPower>(),
        HoverTipFactory.FromPower<FragrancePower>(),
        HoverTipFactory.FromPower<FragranceLostPower>(),
    ];
    
    //private bool _isadding = false;

    public override decimal ModifyPowerAmountGiven(PowerModel power, Creature giver, decimal amount, Creature? target,
        CardModel? cardSource)
    {
        if (power is AutumnAuraPower or AutumnAuraPower && amount > 0 && giver == Owner)
        {
            return amount + Amount;
        }
        return amount;
    }

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        //if (_isadding)
        //    return;
        //_isadding = true;
        switch (power)
        {
            case AutumnAuraPower when amount > 0 && applier == Owner:
                //await PowerCmd.Apply<AutumnAuraPower>(choiceContext, power.Owner, Amount, applier, null);
                await PowerCmd.Apply<AutumnAuraLostPower>(choiceContext, power.Owner, Amount, applier, null);
                break;
            case FragrancePower when amount > 0 && applier == Owner:
                //await PowerCmd.Apply<FragrancePower>(choiceContext, power.Owner, Amount, applier, null);
                await PowerCmd.Apply<FragranceLostPower>(choiceContext, power.Owner, Amount, applier, null);
                break;
        }

        //_isadding = false;
    }
}