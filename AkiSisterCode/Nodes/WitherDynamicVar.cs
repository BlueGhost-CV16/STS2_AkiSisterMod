using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2RitsuLib.Cards.DynamicVars;

namespace AkiSister.AkiSisterCode.Nodes;

public class WitherDynamicVar : DynamicVar
{
    // 在描述中用作占位符的键，推荐添加前缀避免撞车
    public const string Key = "AkiSister-Wither";
    // 本地化键，这里设置为大写的Key，也就是"TEST-LEECH"
    public static readonly string LocKey = Key.ToUpperInvariant();

    public WitherDynamicVar(decimal baseValue) : base(Key, baseValue)
    {
        this.WithSharedTooltip(LocKey);
    }
}