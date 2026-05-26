using AkiSister.AkiSisterCode.Extensions;
using MegaCrit.Sts2.Core.Models;
using STS2RitsuLib.Scaffolding.Content;

namespace AkiSister.AkiSisterCode.Enchantments;

public abstract class AkiSisterEnchantment : ModEnchantmentTemplate
{
    public override EnchantmentAssetProfile AssetProfile => new(
        IconPath: $"res://AkiSister/images/enchantments/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png"
    );
    //protected override string? CustomIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".EnchantmentPath();
    //TODO
}