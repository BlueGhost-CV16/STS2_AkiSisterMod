using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Content;
using STS2RitsuLib.Interop.AutoRegistration;

namespace AkiSister.AkiSisterCode.Nodes;
[RegisterOwnedCardKeyword("Redleafmark")]
[RegisterOwnedCardKeyword("Sweetpotatomark")]
[RegisterOwnedCardKeyword("Redleaffavor")]
[RegisterOwnedCardKeyword("Sweetpotatofavor")]
[RegisterOwnedCardKeyword("Redleafresonance")]
[RegisterOwnedCardKeyword("Sweetpotatoresonance")]
public static class AkiSisterCardKeyWords
{
    // 自定义枚举的名字。最终会变成{前缀}-{枚举值大写}的形式，例如TEST-UNIQUE
    // 放在原版卡牌描述的位置，这里是卡牌描述的前面
    public static readonly string RedLeafMark = ModContentRegistry.GetQualifiedKeywordId(Entry.ModId, nameof(RedLeafMark));
    public static readonly string SweetPotatoMark = ModContentRegistry.GetQualifiedKeywordId(Entry.ModId, nameof(SweetPotatoMark));
    public static readonly string RedLeafFavor = ModContentRegistry.GetQualifiedKeywordId(Entry.ModId, nameof(RedLeafFavor));
    public static readonly string SweetPotatoFavor = ModContentRegistry.GetQualifiedKeywordId(Entry.ModId, nameof(SweetPotatoFavor));
    public static readonly string RedLeafResonance = ModContentRegistry.GetQualifiedKeywordId(Entry.ModId, nameof(RedLeafResonance));
    public static readonly string SweetPotatoResonance = ModContentRegistry.GetQualifiedKeywordId(Entry.ModId, nameof(SweetPotatoResonance));

}

//public static class AkiSisterCardTags
//{
//    [CustomEnum]
//    public static CardTag Spark;
//}