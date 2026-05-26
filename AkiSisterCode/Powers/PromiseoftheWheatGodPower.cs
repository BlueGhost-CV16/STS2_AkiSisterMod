using AkiSister.AkiSisterCode.Cards.StatusCards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Powers;
[RegisterPower]

public class PromiseoftheWheatGodPower : AkiSisterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    [
        HoverTipFactory.FromPower<DrainPower>()
    ];
    
    public override decimal ModifyPowerAmountGiven(PowerModel power, Creature giver, decimal amount, Creature? target, CardModel? cardSource)
    {
        if (power is not DrainPower || giver != base.Owner)
        {
            return amount;
        }
        return amount + Amount;
    }
}