using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;

namespace AkiSister.AkiSisterCode.Powers;


public class BondofAutumnLeavesPower : AkiSisterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => HoverTipFactory.FromEnchantment<RedLeafMarkEnchantment>();

    public override async Task AfterCardPlayedLate(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner == base.Owner.Player && cardPlay.Card.LeafCheck())
        {
            await CardPileCmd.Draw(choiceContext, Amount, base.Owner.Player);
        }
    }

    //public override async Task BeforeCardPlayed(CardPlay cardPlay)
    //{
    //    if (cardPlay.Card.Owner == base.Owner.Player && cardPlay.Card.LeafCheck())
    //    {
    //        await CardPileCmd.Draw(choiceContext, Amount, base.Owner.Player);
    //    }
    //}
}