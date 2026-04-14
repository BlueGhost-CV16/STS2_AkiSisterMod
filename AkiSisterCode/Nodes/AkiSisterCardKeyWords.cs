using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace AkiSister.AkiSisterCode.Nodes;
public static class AkiSisterCardKeyWords
{
    // 自定义枚举的名字。最终会变成{前缀}-{枚举值大写}的形式，例如TEST-UNIQUE
    // 放在原版卡牌描述的位置，这里是卡牌描述的前面
    [CustomEnum("Redleafmark")]
    [KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword RedLeafMark;
    [CustomEnum("Sweetpotatomark")]
    [KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword SweetPotatoMark;
    [CustomEnum("Redleaffavor")]
    [KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword RedLeafFavor;
    [CustomEnum("Sweetpotatofavor")]
    [KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword SweetPotatoFavor;
    [CustomEnum("Redleafresonance")]
    [KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword RedLeafResonance;
    [CustomEnum("Sweetpotatoresonance")]
    [KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword SweetPotatoResonance;

}

//public static class AkiSisterCardTags
//{
//    [CustomEnum]
//    public static CardTag Spark;
//}