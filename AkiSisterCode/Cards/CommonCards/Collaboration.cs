using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace AkiSister.AkiSisterCode.Cards.CommonCards;

public class Collaboration() : AkiSisterCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AllEnemies)
{
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(14m, ValueProp.Move)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<ShepherdandApricotBlossom>(),
        HoverTipFactory.FromCard<HarvesterandPearBlossom>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
            .WithHitFx("vfx/vfx_heavy_blunt", null, "blunt_attack.mp3")
            .Execute(choiceContext);
        await base.Owner.FlowerAdd_Deck(base.CombatState);
        await base.Owner.GrassAdd_Deck(base.CombatState);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(4);
    }
}