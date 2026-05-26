using AkiSister.AkiSisterCode.Extensions;
using AkiSister.Characters;
using Godot;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;

namespace AkiSister.AkiSisterCode.Relics;

public abstract class AkiSisterRelic : ModRelicTemplate
{
    // 图片资源统一放在 AssetProfile 里配置。
    // 三个路径可以先指向同一张图。后续有高清图或轮廓图时再拆开。
    public override RelicAssetProfile AssetProfile => new(
        // 小图标（原版 85x85）。
        IconPath: $"{Entry.ResPath}/images/relics/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png",
        // 轮廓图标（原版 85x85）。
        IconOutlinePath: $"{Entry.ResPath}/images/relics/outline/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png",
        // 大图标（原版 256x256）。
        BigIconPath: $"{Entry.ResPath}/images/relics/big/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png");
    
    //public override string PackedIconPath
    //{
    //    get
    //    {
    //        var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".RelicImagePath();
    //        return ResourceLoader.Exists(path) ? path : "relic.png".RelicImagePath();
    //    }
    //}
//
    //protected override string PackedIconOutlinePath
    //{
    //    get
    //    {
    //        var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".RelicImageOutlinePath();
    //        return ResourceLoader.Exists(path) ? path : "relic_outline.png".RelicImageOutlinePath();
    //    }
    //}
//
    //protected override string BigIconPath
    //{
    //    get
    //    {
    //        var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigRelicImagePath();
    //        return ResourceLoader.Exists(path) ? path : "relic.png".BigRelicImagePath();
    //    }
    //}
}