using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Powers;
using AkiSister.Characters;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Potion;


public class GrowthPotion : AkiSisterPotion
{
    // 稀有度
    public override PotionRarity Rarity => PotionRarity.Uncommon;
    
    // 使用方式，CombatOnly表示只能在战斗中使用。
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    
    // 目标类型
    public override TargetType TargetType => TargetType.Self;
    
    // 定义动态变量
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new EnergyVar(3),
        //new CardsVar(1),
    ];

    // 这里显示预览卡牌灵魂。或者你可以添加提示关键词
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromCard<ShepherdandApricotBlossom>(),
        HoverTipFactory.FromCard<HarvesterandPearBlossom>(),
    ];
    
    // 打出时的效果逻辑，这里是创造3张灵魂到手牌中。
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        // 这里的DynamicVars.Cards.IntValue就是我们在CanonicalVars中定义的CardsVar的数值，也就是3。
        if (target?.Player == null)
            return;
        PotionModel.AssertValidForTargetedPotion(target);
        await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, target.Player);
        await target.Player.FlowerAdd(target.CombatState);
        await target.Player.GrassAdd(target.CombatState);
    }
}