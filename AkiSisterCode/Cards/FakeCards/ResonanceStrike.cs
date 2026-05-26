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
//public class ResonanceStrike() : AkiSisterCard(1,
//    CardType.Attack, CardRarity.Common,
//    TargetType.AnyEnemy)
//{
//    protected override bool ShouldGlowGoldInternal => this.LeafCheck();
//    
//    protected override HashSet<CardTag> CanonicalTags =>
//    [
//        CardTag.Strike
//    ];
//    
//    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(9m, ValueProp.Move)];
//    
//    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
//            HoverTipFactory.FromKeyword(AkiSisterCardKeyWords.RedLeafResonance)
//    ];
//    
//    protected override async Task OnPlay(
//        PlayerChoiceContext choiceContext,
//        CardPlay play)
//    {
//        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(play.Target)
//            .Execute(choiceContext);
//        if (this.LeafCheck())
//        {
//            await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(play.Target)
//                .Execute(choiceContext);
//        }
//    }
//
//    protected override void OnUpgrade()
//    {
//        DynamicVars.Damage.UpgradeValueBy(3);
//    }
//}