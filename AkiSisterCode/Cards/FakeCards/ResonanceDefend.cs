//using AkiSister.AkiSisterCode.Extensions;
//using AkiSister.AkiSisterCode.Nodes;
//using MegaCrit.Sts2.Core.Commands;
//using MegaCrit.Sts2.Core.Entities.Cards;
//using MegaCrit.Sts2.Core.GameActions.Multiplayer;
//using MegaCrit.Sts2.Core.HoverTips;
//using MegaCrit.Sts2.Core.Localization.DynamicVars;
//using MegaCrit.Sts2.Core.ValueProps;
//
//namespace AkiSister.AkiSisterCode.Cards.CommonCards;
//
//public class ResonanceDefend() : AkiSisterCard(1,
//    CardType.Skill, CardRarity.Common,
//    TargetType.Self)
//{
//    protected override bool ShouldGlowGoldInternal => this.PotatoCheck();
//    
//    public override bool GainsBlock => true;
//
//    protected override HashSet<CardTag> CanonicalTags => [CardTag.Defend];
//
//    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(7m, ValueProp.Move)];
//    
//    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
//            HoverTipFactory.FromKeyword(AkiSisterCardKeyWords.SweetPotatoResonance)
//    ];
//    
//    protected override async Task OnPlay(
//        PlayerChoiceContext choiceContext,
//        CardPlay play)
//    {
//        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
//        if (this.PotatoCheck())
//        {
//            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
//        }
//    }
//
//    protected override void OnUpgrade()
//    {
//        DynamicVars.Block.UpgradeValueBy(3m);
//    }
//}