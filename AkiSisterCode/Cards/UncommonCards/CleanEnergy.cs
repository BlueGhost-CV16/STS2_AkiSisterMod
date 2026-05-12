using AkiSister.AkiSisterCode.Cards;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AkiSister.AkiSisterCode.Cards.UncommonCards;

public class CleanEnergy() : AkiSisterCard(1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new EnergyVar(1),
        //new PowerVar<CleanEnergyPower>(1m),
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        base.EnergyHoverTip
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<CleanEnergyPower>(choiceContext, Owner.Creature, DynamicVars.Energy.BaseValue, Owner.Creature, this);
        //await PowerCmd.Apply<CleanEnergyPower>(Owner.Creature, DynamicVars["CleanEnergyPower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
        //DynamicVars.Energy.UpgradeValueBy(1);
        //DynamicVars["CleanEnergyPower"].UpgradeValueBy(1);
    }
}