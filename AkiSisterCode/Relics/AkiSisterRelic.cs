using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using AkiSister.AkiSisterCode.Character;
using AkiSister.AkiSisterCode.Extensions;
using Godot;

namespace AkiSister.AkiSisterCode.Relics;

[Pool(typeof(AkiSisterRelicPool))]
public abstract class AkiSisterRelic : CustomRelicModel
{
    public override string PackedIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".RelicImagePath();
            return ResourceLoader.Exists(path) ? path : "relic.png".RelicImagePath();
        }
    }

    protected override string PackedIconOutlinePath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".RelicImageOutlinePath();
            return ResourceLoader.Exists(path) ? path : "relic_outline.png".RelicImageOutlinePath();
        }
    }

    protected override string BigIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigRelicImagePath();
            return ResourceLoader.Exists(path) ? path : "relic.png".BigRelicImagePath();
        }
    }
}