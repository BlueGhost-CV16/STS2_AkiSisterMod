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


public class FragranceCondenses() : AkiSisterCard(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<FragrancePower>(6m),
        new PowerVar<FragranceLostPower>(9m)
    ];
    
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<FragranceLostPower>(),
        HoverTipFactory.FromPower<FragrancePower>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<FragrancePower>(choiceContext, Owner.Creature,
            DynamicVars["FragrancePower"].BaseValue + DynamicVars["FragranceLostPower"].BaseValue, base.Owner.Creature,
            this);
        await PowerCmd.Apply<FragranceLostPower>(choiceContext, Owner.Creature, DynamicVars["FragranceLostPower"].BaseValue,
            base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["FragrancePower"].UpgradeValueBy(2);
        DynamicVars["FragranceLostPower"].UpgradeValueBy(2);
    }
}