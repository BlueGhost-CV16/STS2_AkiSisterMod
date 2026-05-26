using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.Characters;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Interop.AutoRegistration;
using AkiSister.Characters;

namespace AkiSister.AkiSisterCode.Cards.BasicCards;
[RegisterCard(typeof(AkiSisterCardPool))]

[RegisterCharacterStarterCard(typeof(AkiSisterCharacter), 1)]
public class GlowofAutumnSunset() : AkiSisterCard(1,
    CardType.Attack, CardRarity.Basic,
    TargetType.AnyEnemy)
{
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(11m, ValueProp.Move),
        new CardsVar(1)
    ];
    
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromCard<ShepherdandApricotBlossom>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Execute(choiceContext);
        await base.Owner.FlowerAdd_Deck(base.CombatState, (int)DynamicVars.Cards.BaseValue, IsUpgraded);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(1);
    }
}