using AkiSister.Characters;
using AkiSister.Characters;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Interop.AutoRegistration;
using AkiSister.Characters;

namespace AkiSister.AkiSisterCode.Cards.BasicCards;
[RegisterCard(typeof(AkiSisterCardPool))]

[RegisterCharacterStarterCard(typeof(AkiSisterCharacter), 4)]
public class StrikeAkiSister() : AkiSisterCard(1,
    CardType.Attack, CardRarity.Basic,
    TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags =>
    [
        CardTag.Strike
    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6m, ValueProp.Move)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (this.IsInCombat && this.Pile.IsCombatPile)
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }
}