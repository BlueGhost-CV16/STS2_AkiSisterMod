using AkiSister.AkiSisterCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace AkiSister.AkiSisterCode.Cards.RareCards;

public class ColorGathering() : AkiSisterCard(0,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(7m, ValueProp.Move)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        //ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        List<PowerModel> originalDebuffs = new List<PowerModel>();
            //(from p in cardPlay.Target.Powers
            //where p.TypeForCurrentAmount == PowerType.Debuff
            //select (PowerModel)p.ClonePreservingMutability()).ToList();
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
        foreach (Creature enemy in base.CombatState.HittableEnemies)
        {
            if (enemy == cardPlay.Target)
            {
                continue;
            }
            originalDebuffs.AddRange((from p in enemy.Powers
                where p.TypeForCurrentAmount == PowerType.Debuff
                select (PowerModel)p.ClonePreservingMutability()).ToList());
        }
        foreach (PowerModel item in originalDebuffs)
        {
            PowerModel? powerById = cardPlay.Target.GetPowerById(item.Id);
            if (powerById != null && !powerById.IsInstanced)
            {
                DoHackyThingsForSpecificPowers(powerById);
                await PowerCmd.ModifyAmount(powerById, item.Amount, base.Owner.Creature, this);
            }
            else
            {
                PowerModel power = (PowerModel)item.ClonePreservingMutability();
                DoHackyThingsForSpecificPowers(power);
                await PowerCmd.Apply(power, cardPlay.Target, item.Amount, base.Owner.Creature, this);
            }
        }
    }

    private static void DoHackyThingsForSpecificPowers(PowerModel power)
    {
        if (power is ITemporaryPower temporaryPower)
        {
            temporaryPower.IgnoreNextInstance();
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(2m);
        RemoveKeyword(CardKeyword.Exhaust);
    }
}