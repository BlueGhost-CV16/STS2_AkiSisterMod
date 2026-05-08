using AkiSister.AkiSisterCode.Powers;
using AkiSister.AkiSisterCode.Relics;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace AkiSister.AkiSisterCode.Relics;

public class WitheredBranches() : AkiSisterRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Rare;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<WitherPower>(),
        HoverTipFactory.FromPower<DrainPower>()
    ];

    public decimal ModifyWitherMultiplier(Creature target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (target == base.Owner.Creature)
        {
            return amount;
        }
        if (!props.IsPoweredAttack())
        {
            return amount;
        }
        return amount + 0.2m;
    }

    public decimal ModifyDrainMultiplier(Creature target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (target != base.Owner.Creature)
        {
            return amount;
        }
        if (!props.IsPoweredAttack())
        {
            return amount;
        }
        return amount - 0.1m;
    }
}