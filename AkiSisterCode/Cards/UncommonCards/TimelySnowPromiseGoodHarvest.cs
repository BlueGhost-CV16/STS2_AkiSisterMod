using AkiSister.Characters;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AkiSister.AkiSisterCode.Cards.UncommonCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class TimelySnowPromiseGoodHarvest() : AkiSisterCard(0,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<TimelySnowPromiseGoodHarvestPower>(1m),
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<TimelySnowPromiseGoodHarvestPower>(choiceContext, Owner.Creature, DynamicVars["TimelySnowPromiseGoodHarvestPower"].BaseValue, Owner.Creature, this);
    }
    
    protected override void OnUpgrade()
    {
        DynamicVars["TimelySnowPromiseGoodHarvestPower"].UpgradeValueBy(1);
    }
}