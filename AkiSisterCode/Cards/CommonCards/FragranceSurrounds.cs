using AkiSister.AkiSisterCode.Cards;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace AkiSister.AkiSisterCode.Cards.CommonCards;

public class FragranceSurrounds() : AkiSisterCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<FragrancePower>(5),
        new PowerVar<FragranceSurroundsPower>(1m)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<FragrancePower>(),
        HoverTipFactory.FromPower<StrengthPower>()
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<FragrancePower>(Owner.Creature, base.DynamicVars["FragrancePower"].BaseValue, base.Owner.Creature, this);
        var enemies = base.CombatState?.HittableEnemies;
        await PowerCmd.Apply<FragranceSurroundsPower>(enemies, base.DynamicVars["FragranceSurroundsPower"].BaseValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["FragrancePower"].UpgradeValueBy(1m);
        DynamicVars["FragranceSurroundsPower"].UpgradeValueBy(1m);
    }
}