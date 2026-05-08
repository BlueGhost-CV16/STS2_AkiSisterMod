using AkiSister.AkiSisterCode.Cards.StatusCards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Powers;

public class LeafGodFormPower : AkiSisterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromCard<ShepherdandApricotBlossom>(true)
    ];
    
    public override Task AfterCardGeneratedForCombat(CardModel card, Player? creator)
    {
        if (card.Owner == base.Owner.Player && card is ShepherdandApricotBlossom)
        {
            Flash();
            CardCmd.Upgrade(card);
        }
        return Task.CompletedTask;
    }
    //protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    //[
    //    HoverTipFactory.FromPower<AutumnAuraPower>()
    //];
}