using AkiSister.Characters;
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
[RegisterCard(typeof(AkiSisterCardPool))]

public class KillingofColdAutumn() : AkiSisterCard(1,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(5),
        new ExtraDamageVar(3),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, _) => card.Owner.PlayerCombatState?.ExhaustPile.Cards.Count(c => c.Type == CardType.Status) ?? 0)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.ExtraDamage.UpgradeValueBy(1m);
    }
}