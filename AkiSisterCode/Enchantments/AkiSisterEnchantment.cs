using AkiSister.AkiSisterCode.Extensions;
using MegaCrit.Sts2.Core.Models;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;

namespace AkiSister.AkiSisterCode.Enchantments;

[RegisterEnchantment(Inherit = true)]
public abstract class AkiSisterEnchantment : ModEnchantmentTemplate
{
    public override EnchantmentAssetProfile AssetProfile => new(
        IconPath: $"res://AkiSister/images/enchantments/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png"
    );
    //protected override string? CustomIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".EnchantmentPath();
    //TODO
}