using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;

namespace AkiSister.AkiSisterCode.Relics;

public class ShavedIce() : AkiSisterRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Rare;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CardsVar(1),
        new EnergyVar(1),
    ];
    
    private CardModel? _statusCard;

    private bool _isActivating;

    private CardModel? StatusCard
    {
        get
        {
            return _statusCard;
        }
        set
        {
            AssertMutable();
            _statusCard = value;
        }
    }

    private bool IsActivating
    {
        get
        {
            return _isActivating;
        }
        set
        {
            AssertMutable();
            _isActivating = value;
        }
    }
    
    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (IsActivating && card.Owner == Owner && card.Type == CardType.Status)
        {
            Flash();
            base.Status = RelicStatus.Normal;
            StatusCard = card;
            IsActivating = false;
            await CardCmd.Exhaust(choiceContext, card);
            await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, Owner);
            await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
        }
    }public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
    {
        if (side != CombatSide.Player)
        {
            return Task.CompletedTask;
        }
        StatusCard = null;
        IsActivating = true;
        base.Status = RelicStatus.Normal;
        return Task.CompletedTask;
    }
    
    public override Task AfterCombatEnd(CombatRoom room)
    {
        StatusCard = null;
        IsActivating = true;
        base.Status = RelicStatus.Normal;
        return Task.CompletedTask;
    }
}