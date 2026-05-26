using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Powers;
[RegisterPower]

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
    
    private bool _isadding = false;

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        if (_isadding)
            return;
        _isadding = true;
        if (power is AutumnAuraPower && amount > 0 && applier == Owner)
        {
            await PowerCmd.Apply<AutumnAuraPower>(choiceContext, power.Owner, Amount, applier, null);
            await PowerCmd.Apply<AutumnAuraLostPower>(choiceContext, power.Owner, Amount, applier, null);
        }
        if (power is FragrancePower && amount > 0 && applier == Owner)
        {
            await PowerCmd.Apply<FragrancePower>(choiceContext, power.Owner, Amount, applier, null);
            await PowerCmd.Apply<FragranceLostPower>(choiceContext, power.Owner, Amount, applier, null);
        }
        _isadding = false;
    }
}