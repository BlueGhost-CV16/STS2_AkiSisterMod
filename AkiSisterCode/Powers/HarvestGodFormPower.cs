using AkiSister.AkiSisterCode.Cards.StatusCards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Powers;
[RegisterPower]

public class HarvestGodFormPower : AkiSisterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    [
        HoverTipFactory.FromCard<HarvesterandPearBlossom>(true)
    ];

    public override Task AfterCardGeneratedForCombat(CardModel card, Player? creator)
    {
        if (card.Owner == base.Owner.Player && card is HarvesterandPearBlossom)
        {
            Flash();
            CardCmd.Upgrade(card);
        }
        return Task.CompletedTask;
    }
    //protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    //[
    //    HoverTipFactory.FromPower<AutumnAuraPower>()
    //];
}