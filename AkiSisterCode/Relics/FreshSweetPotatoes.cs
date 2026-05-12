using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Relics;

public class FreshSweetPotatoes() : AkiSisterRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Shop;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<AutumnAuraPower>(),
        HoverTipFactory.FromPower<FragrancePower>()
    ];
    
    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        switch (power)
        {
            case AutumnAuraPower when amount <= 0:
            {
                var enemy = base.Owner.RunState.Rng.CombatTargets.NextItem(power.Owner.CombatState.HittableEnemies);
                if (enemy != null)
                {
                    await PowerCmd.Apply<WitherPower>(choiceContext, enemy, -amount, base.Owner.Creature, null);
                }

                break;
            }
            case FragrancePower when amount <= 0:
            {
                var enemy = base.Owner.RunState.Rng.CombatTargets.NextItem(power.Owner.CombatState.HittableEnemies);
                if (enemy != null)
                {
                    await PowerCmd.Apply<DrainPower>(choiceContext, enemy, -amount, base.Owner.Creature, null);
                }

                break;
            }
        }
    }
}