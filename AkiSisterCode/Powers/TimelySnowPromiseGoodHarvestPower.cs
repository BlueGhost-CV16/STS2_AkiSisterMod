using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace AkiSister.AkiSisterCode.Powers;

public class TimelySnowPromiseGoodHarvestPower : AkiSisterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task BeforeSideTurnEndEarly(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == CombatSide.Enemy)
        {
            return;
        }
        var pile = CardPile.GetCards(base.Owner.Player, PileType.Hand).Where(card => card.Type == CardType.Status).ToList();
        if (Amount >= pile.Count)
        {
            foreach (var item in pile)
            {
                await CardCmd.Exhaust(choiceContext, item);
            }
            await PowerCmd.Apply<DrawCardsNextTurnPower>(choiceContext, Owner, pile.Count, Owner, null);
        }
        else
        {
            for (var i = 0; i < Amount; i++)
            {
                var card = pile.StableShuffle(base.Owner.Player.RunState.Rng.Shuffle).FirstOrDefault();
                if (card == null) continue;
                await CardCmd.Exhaust(choiceContext, card);
                await PowerCmd.Apply<DrawCardsNextTurnPower>(choiceContext, Owner, 1, Owner, null);
            }
        }
    }

    //public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    //{
    //    if (side == CombatSide.Enemy)
    //    {
    //        return;
    //    }
    //    var pile = CardPile.GetCards(base.Owner.Player, PileType.Hand).Where(card => card.Type == CardType.Status).ToList();
    //    if (Amount >= pile.Count)
    //    {
    //        foreach (CardModel item in pile)
    //        {
    //            await CardCmd.Exhaust(choiceContext, item);
    //        }
    //        await PowerCmd.Apply<DrawCardsNextTurnPower>(choiceContext, Owner, pile.Count, Owner, null);
    //    }
    //    else
    //    {
    //        for (int i = 0; i < Amount; i++)
    //        {
    //            var card = pile.StableShuffle(base.Owner.Player.RunState.Rng.Shuffle).FirstOrDefault();
    //            if (card != null)
    //            {
    //                await CardCmd.Exhaust(choiceContext, card);
    //                await PowerCmd.Apply<DrawCardsNextTurnPower>(choiceContext, Owner, 1, Owner, null);
    //            }
    //        }
    //    }
    //}
}