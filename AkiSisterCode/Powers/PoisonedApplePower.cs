using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Powers;
[RegisterPower]

public class PoisonedApplePower : AkiSisterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    [
        HoverTipFactory.FromPower<WitherPower>(),
        HoverTipFactory.FromPower<DrainPower>()
    ];
    
    private bool _isadding = false;

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        if (_isadding)
            return;
        _isadding = true;
        if (power is WitherPower && amount > 0 && applier == Owner)
        {
            await PowerCmd.Apply<DrainPower>(choiceContext, power.Owner, Amount, applier, null);
        }
        if (power is DrainPower && amount > 0 && applier == Owner)
        {
            await PowerCmd.Apply<WitherPower>(choiceContext, power.Owner, Amount, applier, null);
        }
        _isadding = false;
    }
}