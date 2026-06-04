using AkiSister.Characters;
using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Nodes;
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


public class FarewellWhispersofAutumnWaters() : AkiSisterCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override bool HasEnergyCostX => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<FragrancePower>(1),
        new PowerVar<EnergyNextTurnPower>(1)
    ];
    
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<FragrancePower>(),
        HoverTipFactory.FromCard<HarvesterandPearBlossom>(),
        base.EnergyHoverTip,
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        for (int i = 0; i < ResolveEnergyXValue(); i++)
        {
            await PowerCmd.Apply<FragrancePower>(choiceContext, Owner.Creature, base.DynamicVars["FragrancePower"].BaseValue, base.Owner.Creature, this);
        }
        await Owner.GrassAdd_Deck(base.CombatState, ResolveEnergyXValue());
        await PowerCmd.Apply<EnergyNextTurnPower>(choiceContext, Owner.Creature, base.DynamicVars["EnergyNextTurnPower"].BaseValue * ResolveEnergyXValue(), base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
        DynamicVars["FragrancePower"].UpgradeValueBy(1m);
        //base.AddKeyword(AkiSisterCardKeyWords.SweetPotatoFavor);
        //this.PotatoAdd_Card();
    }
}