using AkiSister.AkiSisterCode.Extensions;
using AkiSister.Characters;
using Godot;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;

namespace AkiSister.AkiSisterCode.Potion;

public abstract class AkiSisterPotion : ModPotionTemplate
{
    public override PotionAssetProfile AssetProfile => new(
        ImagePath: $"{Entry.ResPath}/images/potions/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png",
        OutlinePath: $"{Entry.ResPath}/images/potions/outline/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png"
    );
    //public override string CustomPackedImagePath
    //{
    //    get
    //    {
    //        var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionImagePath();
    //        return ResourceLoader.Exists(path) ? path : "potion.png".PotionImagePath();
    //    }
    //}
    //
    //public override string CustomPackedOutlinePath
    //{
    //    get
    //    {
    //        var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionImageOutlinePath();
    //        return ResourceLoader.Exists(path) ? path : "potion_outline.png".PotionImageOutlinePath();
    //    }
    //}
}