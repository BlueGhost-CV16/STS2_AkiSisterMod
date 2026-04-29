using AkiSister.AkiSisterCode.Cards;
using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AkiSister.AkiSisterCode.Cards.RareCards;

public class AGodThatMissesPeople() : AkiSisterCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.AllAllies)
{
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CardsVar(1),
        new EnergyVar(2)
    ];
    
    //public override IEnumerable<CardKeyword> CanonicalKeywords => [
    //    CardKeyword.Exhaust
    //];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        base.EnergyHoverTip,
        HoverTipFactory.FromCard<ShepherdandApricotBlossom>(),
        HoverTipFactory.FromCard<HarvesterandPearBlossom>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        foreach (Creature item in base.CombatState.GetTeammatesOf(base.Owner.Creature).Where((Creature c) => c is { IsAlive: true, IsPlayer: true }))
        {
            await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, item.Player);
            await item.Player.FlowerAdd(base.CombatState, DynamicVars.Cards.IntValue);
            await item.Player.GrassAdd(base.CombatState, DynamicVars.Cards.IntValue);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Energy.UpgradeValueBy(1m);
    }
}