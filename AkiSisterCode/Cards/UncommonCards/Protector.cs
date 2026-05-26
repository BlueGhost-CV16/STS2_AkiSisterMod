using AkiSister.Characters;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace AkiSister.AkiSisterCode.Cards.UncommonCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class Protector() : AkiSisterCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AnyAlly)
{
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<AutumnAuraPower>(5),
        new PowerVar<FragrancePower>(5)
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<AutumnAuraPower>(),
        HoverTipFactory.FromPower<FragrancePower>(),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        //await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
        await PowerCmd.Apply<AutumnAuraPower>(choiceContext, Owner.Creature, DynamicVars["AutumnAuraPower"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<FragrancePower>(choiceContext, Owner.Creature, DynamicVars["FragrancePower"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<CoveredPower>(choiceContext, cardPlay.Target, 1m, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["AutumnAuraPower"].UpgradeValueBy(2);
        base.DynamicVars["FragrancePower"].UpgradeValueBy(2);
        AddKeyword(CardKeyword.Retain);
    }
}