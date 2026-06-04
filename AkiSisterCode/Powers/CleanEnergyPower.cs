using AkiSister.AkiSisterCode.Cards.StatusCards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Powers;


public class CleanEnergyPower : AkiSisterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new EnergyVar(1)
        //new CardsVar(2)
    ];

    //protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    //[
    //    HoverTipFactory.FromCard<ShepherdandApricotBlossom>(true)
    //];

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner == Owner.Player && cardPlay.Card.Type == CardType.Status && CombatManager.Instance.History.Entries.OfType<CardPlayFinishedEntry>().Count((CardPlayFinishedEntry e) => e.HappenedThisTurn(base.CombatState) && e.Actor == base.Owner && e.CardPlay.Card.Type == CardType.Status) <= 1)
        {
            Flash();
            await PlayerCmd.GainEnergy(Amount, Owner.Player);
        }
    }

    //public override async Task AfterCardGeneratedForCombat(CardModel card, bool addedByPlayer)
    //{
    //    if (card.Owner == base.Owner.Player && card is ShepherdandApricotBlossom)
    //    {
    //        Flash();
    //        CardCmd.Upgrade(card);
    //    }
    //}
}