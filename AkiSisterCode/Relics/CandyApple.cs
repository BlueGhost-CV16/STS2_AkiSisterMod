using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Interop.AutoRegistration;
using AkiSister.Characters;

namespace AkiSister.AkiSisterCode.Relics;
[RegisterRelic(typeof(AkiSisterRelicPool))]

public class CandyApple() : AkiSisterRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Uncommon;

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<AutumnAuraPower>(),
        HoverTipFactory.FromPower<FragrancePower>(),
        HoverTipFactory.Static(StaticHoverTip.Block)
        //HoverTipFactory.FromKeyword(AkiSisterCardKeyWords.RedLeafMark)
    ];
    
    //protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(1m, ValueProp.Unpowered)];

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power is AutumnAuraPower or FragrancePower && amount > 0m)
        {
            Flash();
            await CreatureCmd.GainBlock(base.Owner.Creature, amount, ValueProp.Unpowered, null);
        }
    }
}