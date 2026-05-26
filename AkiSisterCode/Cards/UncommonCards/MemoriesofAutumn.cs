using AkiSister.Characters;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AkiSister.AkiSisterCode.Cards.UncommonCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class MemoriesofAutumn() : AkiSisterCard(1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<MemoriesofAutumnPower>(1m)];
    
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        //HoverTipFactory.FromPower<MemoriesofAutumnPower>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<MemoriesofAutumnPower>(choiceContext, Owner.Creature, DynamicVars["MemoriesofAutumnPower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
        //base.DynamicVars["MemoriesofAutumnPower"].UpgradeValueBy(1m);
    }
}