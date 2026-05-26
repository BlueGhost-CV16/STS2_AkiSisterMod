using AkiSister.Characters;
using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.Characters;
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

namespace AkiSister.AkiSisterCode.Cards.BasicCards;
[RegisterCard(typeof(AkiSisterCardPool))]

[RegisterCharacterStarterCard(typeof(AkiSisterCharacter), 1)]
public class ResentmentofAutumnColors() : AkiSisterCard(1,
    CardType.Skill, CardRarity.Basic,
    TargetType.Self)
{
    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(9m, ValueProp.Move),
        new CardsVar(1)
    ];
    
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromCard<HarvesterandPearBlossom>()
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        await base.Owner.GrassAdd_Deck(base.CombatState, (int)DynamicVars.Cards.BaseValue, IsUpgraded);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(1m);
    }
}