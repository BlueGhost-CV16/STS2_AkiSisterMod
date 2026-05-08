using AkiSister.AkiSisterCode.Cards;
using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Nodes;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace AkiSister.AkiSisterCode.Cards.UncommonCards;

public class WhirlwindAutumnRaid() : AkiSisterCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override bool HasEnergyCostX => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<AutumnAuraPower>(1),
        new PowerVar<EnergyNextTurnPower>(1)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<AutumnAuraPower>(),
        HoverTipFactory.FromCard<ShepherdandApricotBlossom>(),
        base.EnergyHoverTip,
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        for (int i = 0; i < ResolveEnergyXValue(); i++)
        {
            await PowerCmd.Apply<AutumnAuraPower>(choiceContext, Owner.Creature, base.DynamicVars["AutumnAuraPower"].BaseValue, base.Owner.Creature, this);
        }
        await Owner.FlowerAdd_Deck(base.CombatState, ResolveEnergyXValue());
        await PowerCmd.Apply<EnergyNextTurnPower>(choiceContext, Owner.Creature, base.DynamicVars["EnergyNextTurnPower"].BaseValue * ResolveEnergyXValue(), base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
        DynamicVars["AutumnAuraPower"].UpgradeValueBy(1m);
        //base.AddKeyword(AkiSisterCardKeyWords.RedLeafFavor);
        //this.LeafAdd_Card();
    }
}