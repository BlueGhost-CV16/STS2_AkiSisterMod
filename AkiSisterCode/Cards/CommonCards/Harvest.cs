using AkiSister.Characters;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Nodes;
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
using STS2RitsuLib.CardTags;
using STS2RitsuLib.Keywords;

namespace AkiSister.AkiSisterCode.Cards.CommonCards;


public class Harvest() : AkiSisterCard(2,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override bool ShouldGlowGoldInternal => this.PotatoCheck();
    
    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(7m, ValueProp.Move),
        new RepeatVar(2),
        new EnergyVar(2)
    ];
    
    //protected override HashSet<CardTag> CanonicalTags =>
    //[
    //    ModCardTagRegistry.GetCardTag(AkiSisterCardKeyWords.SweetPotatoResonance)
    //];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        AkiSisterCardKeyWords.SweetPotatoResonance.GetModCardKeyword()
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        base.EnergyHoverTip,
        //ModKeywordRegistry.CreateHoverTip(AkiSisterCardKeyWords.SweetPotatoResonance)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        if (this.PotatoCheck())
        {
            await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(2m);
        DynamicVars.Energy.UpgradeValueBy(1);
    }
}