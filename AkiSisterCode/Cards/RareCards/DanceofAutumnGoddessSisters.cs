using AkiSister.Characters;
using AkiSister.AkiSisterCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Interop.AutoRegistration;
using AkiSister.Characters;

namespace AkiSister.AkiSisterCode.Cards.RareCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class DanceofAutumnGoddessSisters() : AkiSisterCard(1,
    CardType.Attack, CardRarity.Rare,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(21m, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
            .WithHitFx("vfx/vfx_heavy_blunt", null, "blunt_attack.mp3")
            //.WithHitVfxSpawnedAtBase()
            .Execute(choiceContext);
        int num = CardPile.MaxCardsInHand - CardPile.GetCards(base.Owner, PileType.Hand).Count();
        for (int i = 0; i < num; i++)
        {
            if (i % 2 == 0)
            {
                await Owner.FlowerAdd(base.CombatState);
            }
            else
            {
                await Owner.GrassAdd(base.CombatState);
            }
        }
        //await CardPileCmd.AddGeneratedCardsToCombat(list, PileType.Hand, addedByPlayer: true);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(5m);
    }
}