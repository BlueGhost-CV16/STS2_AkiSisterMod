using AkiSister.Characters;
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

namespace AkiSister.AkiSisterCode.Cards.AncientCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class RedRainofFallenLeaves() : AkiSisterCard(1,
    CardType.Attack, CardRarity.Ancient,
    TargetType.AnyEnemy)
{
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(18m, ValueProp.Move),
        new CardsVar(1)
    ];
    
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromCard<ShepherdandApricotBlossom>(IsUpgraded)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Execute(choiceContext);
        await base.Owner.FlowerAdd_Deck(base.CombatState, (int)DynamicVars.Cards.BaseValue, IsUpgraded);
        await base.Owner.FlowerAdd(base.CombatState, (int)DynamicVars.Cards.BaseValue, IsUpgraded);
    }
    
    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(5);
        //base.EnergyCost.UpgradeBy(-1);
    }
}