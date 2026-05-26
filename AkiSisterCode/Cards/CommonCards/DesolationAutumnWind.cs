using AkiSister.Characters;
using AkiSister.AkiSisterCode.Powers;
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

namespace AkiSister.AkiSisterCode.Cards.CommonCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class DesolationAutumnWind() : AkiSisterCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        //new DamageVar(6m, ValueProp.Move),
        //new PowerVar<AutumnAuraPower>(8),
        new PowerVar<AutumnAuraLostPower>(10)
    ];
    
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<AutumnAuraLostPower>(),
        HoverTipFactory.FromPower<AutumnAuraPower>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        //await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(play.Target)
        //    .Execute(choiceContext);
        await PowerCmd.Apply<AutumnAuraPower>(choiceContext, Owner.Creature, DynamicVars["AutumnAuraLostPower"].BaseValue, base.Owner.Creature, this);
        await PowerCmd.Apply<AutumnAuraLostPower>(choiceContext, Owner.Creature, DynamicVars["AutumnAuraLostPower"].BaseValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        //DynamicVars["AutumnAuraPower"].UpgradeValueBy(2);
        DynamicVars["AutumnAuraLostPower"].UpgradeValueBy(3);
    }
}