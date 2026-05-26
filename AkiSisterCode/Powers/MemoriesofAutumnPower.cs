using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Powers;
[RegisterPower]

public class MemoriesofAutumnPower : AkiSisterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [];

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player == Owner.Player)
        {
            Flash();
            var cards = PileType.Draw.GetPile(base.Owner.Player).Cards.Where<CardModel>((CardModel c) => c.Type == CardType.Status).ToList().Take(Amount);
            await CardPileCmd.Add(cards, PileType.Hand);
        }
    }
}