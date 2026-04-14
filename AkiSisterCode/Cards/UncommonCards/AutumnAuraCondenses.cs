using AkiSister.AkiSisterCode.Cards;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AkiSister.AkiSisterCode.Cards.UncommonCards;

public class AutumnAuraCondenses() : AkiSisterCard(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        //new DamageVar(6m, ValueProp.Move),
        new PowerVar<AutumnAuraPower>(13m),
        new PowerVar<AutumnAuraLostPower>(7m)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<AutumnAuraPower>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        //await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(play.Target)
        //    .Execute(choiceContext);
        await PowerCmd.Apply<AutumnAuraPower>(Owner.Creature, DynamicVars["AutumnAuraPower"].BaseValue, base.Owner.Creature, this);
        await PowerCmd.Apply<AutumnAuraLostPower>(Owner.Creature, DynamicVars["AutumnAuraLostPower"].BaseValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["AutumnAuraPower"].UpgradeValueBy(3);
        DynamicVars["AutumnAuraLostPower"].UpgradeValueBy(1);
    }
}