using BaseLib.Abstracts;
using BaseLib.Utils;
using AkiSister.AkiSisterCode.Character;
using AkiSister.AkiSisterCode.Extensions;
using BaseLib.Extensions;
using Godot;

namespace AkiSister.AkiSisterCode.Potions;

[Pool(typeof(AkiSisterPotionPool))]
public abstract class AkiSisterPotion : CustomPotionModel
{
    public override string CustomPackedImagePath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionImagePath();
            return ResourceLoader.Exists(path) ? path : "potion.png".PotionImagePath();
        }
    }
    
    public override string CustomPackedOutlinePath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionImageOutlinePath();
            return ResourceLoader.Exists(path) ? path : "potion_outline.png".PotionImageOutlinePath();
        }
    }
}