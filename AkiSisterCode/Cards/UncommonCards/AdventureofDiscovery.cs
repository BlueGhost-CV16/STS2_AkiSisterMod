using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Interop.AutoRegistration;
using AkiSister.Characters;

namespace AkiSister.AkiSisterCode.Cards.UncommonCards;


public class AdventureofDiscovery() : AkiSisterCard(2,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(12, ValueProp.Move),
        new PowerVar<WitherPower>(4),
    ];

    private bool HasStateUsedThisTurn => CombatManager.Instance.History.Entries.OfType<CardPlayFinishedEntry>()
        .Any((CardPlayFinishedEntry e) => e.HappenedThisTurn(base.CombatState) && e.Actor == base.Owner.Creature &&
                                            e.CardPlay.Card.Type == CardType.Status);
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Execute(choiceContext);
        await PowerCmd.Apply<WitherPower>(choiceContext, play.Target, DynamicVars["WitherPower"].BaseValue, Owner.Creature, this);
    }
    
    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(4m);
        DynamicVars["WitherPower"].UpgradeValueBy(2);
    }

    public override Task AfterCardEnteredCombat(CardModel card)
    {
        if (card != this || !HasStateUsedThisTurn)
            return Task.CompletedTask;
        ReduceCost();
        return Task.CompletedTask;
    }

    public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner || cardPlay.Card == this || cardPlay.Card.Type != CardType.Status)
            return Task.CompletedTask;
        ReduceCost();
        return Task.CompletedTask;
    }

    private void ReduceCost()
    {
        base.EnergyCost.SetThisTurn(0);
    }
}