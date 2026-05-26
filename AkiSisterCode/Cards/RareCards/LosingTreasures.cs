using AkiSister.Characters;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Interop.AutoRegistration;
using AkiSister.Characters;

namespace AkiSister.AkiSisterCode.Cards.RareCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class LosingTreasures() : AkiSisterCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    public override bool GainsBlock => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(0),
        new CalculationExtraVar(3),
        new CalculatedBlockVar(ValueProp.Move).WithMultiplier((card,  _) => card.Owner.Creature.GetPowerAmount<AutumnAuraPower>() + card.Owner.Creature.GetPowerAmount<FragrancePower>())
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<AutumnAuraPower>(),
        HoverTipFactory.FromPower<FragrancePower>(),
    ];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Retain
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.CalculatedBlock.Calculate(play.Target), DynamicVars.CalculatedBlock.Props, play);
        await PowerCmd.Apply<AutumnAuraPower>(choiceContext, Owner.Creature, -Owner.Creature.GetPowerAmount<AutumnAuraPower>(), Owner.Creature, this);
        await PowerCmd.Apply<FragrancePower>(choiceContext, Owner.Creature, -Owner.Creature.GetPowerAmount<FragrancePower>(), Owner.Creature, this);
        //await PowerCmd.Remove<AutumnAuraPower>(Owner.Creature);
        //await PowerCmd.Remove<FragrancePower>(Owner.Creature);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}