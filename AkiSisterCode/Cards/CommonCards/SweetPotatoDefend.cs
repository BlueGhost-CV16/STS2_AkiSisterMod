using AkiSister.Characters;
using AkiSister.AkiSisterCode.Cards.StatusCards;
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
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Interop.AutoRegistration;
using AkiSister.Characters;

namespace AkiSister.AkiSisterCode.Cards.CommonCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class SweetPotatoDefend() : AkiSisterCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    //public override void AfterCreated()
    //{
    //    base.AfterCreated();
    //    this.PotatoAdd_Card();
    //}

    public override bool GainsBlock => true;

    protected override HashSet<CardTag> CanonicalTags => [CardTag.Defend];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(8m, ValueProp.Move),
        new CardsVar(1),
        new EnergyVar(1)
    ];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        //AkiSisterCardKeyWords.SweetPotatoFavor
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        base.EnergyHoverTip,
        HoverTipFactory.FromCard<HarvesterandPearBlossom>()
    ];
    
    //protected override IEnumerable<IHoverTip> AdditionalHoverTips => HoverTipFactory.FromEnchantment<SweetPotatoMarkEnchantment>();
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        await Owner.GrassAdd_Deck(base.CombatState);
        await PowerCmd.Apply<EnergyNextTurnPower>(choiceContext, Owner.Creature,
            1, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3m);
    }
    //public override async Task BeforeCombatStart()
    //{
    //    await base.Owner.LeafAdd_Card(this);
    //}
}