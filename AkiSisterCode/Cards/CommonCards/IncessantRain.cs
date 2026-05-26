using AkiSister.AkiSisterCode.Powers;
using AkiSister.Characters;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AkiSister.AkiSisterCode.Cards.CommonCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class IncessantRain() : AkiSisterCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        //new PowerVar<FragrancePower>(8),
        new PowerVar<FragranceLostPower>(10)
    ];
    
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<FragranceLostPower>(),
        HoverTipFactory.FromPower<FragrancePower>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<FragrancePower>(choiceContext, Owner.Creature, DynamicVars["FragranceLostPower"].BaseValue,
            base.Owner.Creature, this);
        await PowerCmd.Apply<FragranceLostPower>(choiceContext, Owner.Creature, DynamicVars["FragranceLostPower"].BaseValue,
            base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        //DynamicVars["FragrancePower"].UpgradeValueBy(2);
        DynamicVars["FragranceLostPower"].UpgradeValueBy(3);
    }
}