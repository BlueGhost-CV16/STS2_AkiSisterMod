using AkiSister.Characters;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Nodes;
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

namespace AkiSister.AkiSisterCode.Cards.CommonCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class BumperGrainHarvest() : AkiSisterCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CardsVar(1),
        new BlockVar(9m, ValueProp.Move)
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
        HoverTipFactory.FromEnchantment<SweetPotatoMarkEnchantment>();
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        await base.Owner.PotatoAdd_Hand((int)DynamicVars.Cards.BaseValue);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.UpgradeValueBy(1);
        DynamicVars.Block.UpgradeValueBy(3m);
    }
}