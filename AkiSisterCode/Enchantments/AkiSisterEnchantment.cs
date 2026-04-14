using AkiSister.AkiSisterCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Enchantments;

public abstract class AkiSisterEnchantment : CustomEnchantmentModel
{
    protected override string? CustomIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".EnchantmentPath();
    //TODO
}