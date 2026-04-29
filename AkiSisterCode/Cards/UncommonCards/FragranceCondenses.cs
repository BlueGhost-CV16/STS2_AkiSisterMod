using AkiSister.AkiSisterCode.Cards;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AkiSister.AkiSisterCode.Cards.UncommonCards;

public class FragranceCondenses() : AkiSisterCard(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<FragrancePower>(6m),
        new PowerVar<FragranceLostPower>(7m)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<FragranceLostPower>(),
        HoverTipFactory.FromPower<FragrancePower>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<FragrancePower>(Owner.Creature,
            DynamicVars["FragrancePower"].BaseValue + DynamicVars["FragranceLostPower"].BaseValue, base.Owner.Creature,
            this);
        await PowerCmd.Apply<FragranceLostPower>(Owner.Creature, DynamicVars["FragranceLostPower"].BaseValue,
            base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["FragrancePower"].UpgradeValueBy(2);
        DynamicVars["FragranceLostPower"].UpgradeValueBy(1);
    }
}