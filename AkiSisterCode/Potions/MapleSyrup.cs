using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace AkiSister.AkiSisterCode.Potions;

public class MapleSyrup : AkiSisterPotion
{
    // 稀有度
    public override PotionRarity Rarity => PotionRarity.Common;
    
    // 使用方式，CombatOnly表示只能在战斗中使用。
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    
    // 目标类型
    public override TargetType TargetType => TargetType.AnyAlly;
    
    // 定义动态变量
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<AutumnAuraPower>(6m),
        new PowerVar<FragrancePower>(6m),
    ];

    // 这里显示预览卡牌灵魂。或者你可以添加提示关键词
    public override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<AutumnAuraPower>(),
        HoverTipFactory.FromPower<FragrancePower>(),
    ];
    
    // 打出时的效果逻辑，这里是创造3张灵魂到手牌中。
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        // 这里的DynamicVars.Cards.IntValue就是我们在CanonicalVars中定义的CardsVar的数值，也就是3。
        PotionModel.AssertValidForTargetedPotion(target);
        await PowerCmd.Apply<AutumnAuraPower>(target, DynamicVars["AutumnAuraPower"].BaseValue, base.Owner.Creature, null);
        await PowerCmd.Apply<FragrancePower>(target, DynamicVars["FragrancePower"].BaseValue, base.Owner.Creature, null);
    }
}