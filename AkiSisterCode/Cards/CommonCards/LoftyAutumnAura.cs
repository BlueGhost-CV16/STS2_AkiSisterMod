using AkiSister.AkiSisterCode.Cards;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace AkiSister.AkiSisterCode.Cards.CommonCards;

public class LoftyAutumnAura() : AkiSisterCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<AutumnAuraPower>(5),
        new PowerVar<WeakPower>(1),
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<AutumnAuraPower>(),
        HoverTipFactory.FromPower<WeakPower>()
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<AutumnAuraPower>(choiceContext, Owner.Creature, base.DynamicVars["AutumnAuraPower"].BaseValue, base.Owner.Creature, this);
        var enemies = base.CombatState?.HittableEnemies;
        if (enemies != null)
            await PowerCmd.Apply<WeakPower>(choiceContext, enemies, base.DynamicVars["WeakPower"].BaseValue,
                base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["AutumnAuraPower"].UpgradeValueBy(2m);
    }
}