using AkiSister.AkiSisterCode.Extensions;
using Godot;
using STS2RitsuLib.Scaffolding.Content;

namespace AkiSister.AkiSisterCode.Powers;

public abstract class AkiSisterPower : ModPowerTemplate
{
    // 自定义图标路径。1:1即可。原版游戏大图256x256，小图64x64。
    public override PowerAssetProfile AssetProfile => new(
        IconPath: $"{Entry.ResPath}/images/powers/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png",
        BigIconPath: $"{Entry.ResPath}/images/powers/big/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png"
    );
    
    //Loads from AkiSister/images/powers/your_power.png
    //public override string CustomPackedIconPath
    //{
    //    get
    //    {
    //        var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
    //        return ResourceLoader.Exists(path) ? path : "power.png".PowerImagePath();
    //    }
    //}
//
    //public override string CustomBigIconPath
    //{
    //    get
    //    {
    //        var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
    //        return ResourceLoader.Exists(path) ? path : "power.png".BigPowerImagePath();
    //    }
    //}
}