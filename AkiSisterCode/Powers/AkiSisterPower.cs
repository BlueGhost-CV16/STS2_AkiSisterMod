using BaseLib.Abstracts;
using BaseLib.Extensions;
using AkiSister.AkiSisterCode.Extensions;
using Godot;

namespace AkiSister.AkiSisterCode.Powers;

public abstract class AkiSisterPower : CustomPowerModel
{
    //Loads from AkiSister/images/powers/your_power.png
    public override string CustomPackedIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
            return ResourceLoader.Exists(path) ? path : "power.png".PowerImagePath();
        }
    }

    public override string CustomBigIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
            return ResourceLoader.Exists(path) ? path : "power.png".BigPowerImagePath();
        }
    }
}