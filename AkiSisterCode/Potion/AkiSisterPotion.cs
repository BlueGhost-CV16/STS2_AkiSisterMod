using AkiSister.AkiSisterCode.Extensions;
using AkiSister.Characters;
using Godot;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;

namespace AkiSister.AkiSisterCode.Potion;

[RegisterPotion(typeof(AkiSisterPotionPool), Inherit = true)]
public abstract class AkiSisterPotion : ModPotionTemplate
{
    public override PotionAssetProfile AssetProfile => new(
        ImagePath: $"{Entry.ResPath}/images/potions/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png",
        OutlinePath: $"{Entry.ResPath}/images/potions/outline/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png"
    );
}