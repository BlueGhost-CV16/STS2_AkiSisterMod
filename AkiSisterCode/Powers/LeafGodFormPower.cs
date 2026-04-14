using AkiSister.AkiSisterCode.Cards.StatusCards;
using MegaCrit.Sts2.Core.Commands;
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
    
    public override async Task AfterCardGeneratedForCombat(CardModel card, bool addedByPlayer)
    {
        if (card.Owner == base.Owner.Player && card is ShepherdandApricotBlossom)
        {
            Flash();
            CardCmd.Upgrade(card);
        }
    }
    //protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    //[
    //    HoverTipFactory.FromPower<AutumnAuraPower>()
    //];
}