using AkiSister.AkiSisterCode.Enchantments;
using Godot;
using Godot.Bridge;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using MultiEnchantmentMod;
using MultiEnchantmentMod.Api;

namespace AkiSister.AkiSisterCode;

[ModInitializer(nameof(Initialize))]
public partial class MainFile : Node
{
    public const string ModId = "AkiSister"; //Used for resource filepath

    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } =
        new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

    public static void Initialize()
    {
        Log.Info($"{ModId} Init called");
        Harmony harmony = new(ModId);
        harmony.PatchAll();
        Log.Info($"{ModId} Harmony PatchAll completed");
        ScriptManagerBridge.LookupScriptsInAssembly(typeof(MainFile).Assembly);
        if (!MultiEnchantmentApi.RequireApiVersion(2))
        {
            return;
        }
        MultiEnchantmentApi.ScanCallingAssembly();
        //MultiEnchantmentStackApi.RegisterCompanionProviders<RedLeafMarkEnchantment>();
        //MultiEnchantmentStackApi.RegisterCompanionProviders<SweetPotatoMarkEnchantment>();
    }
    
    //[HarmonyPatch(typeof(CardModel), nameof(CardModel.PortraitPath), MethodType.Getter)]
    //public static class CardModel_GetPortrait_Patch
    //{
    //    private static readonly Dictionary<string, string> CustomPortraits = new(StringComparer.OrdinalIgnoreCase)
    //    {
    //        [nameof(StrikeIronclad)] = "res://test/images/image.png",
    //        [nameof(DefendIronclad)] = "res://test/images/image.png",
    //    };
    //    static void Postfix(CardModel __instance, ref string __result)
    //    {
    //        var className = __instance?.GetType().Name;
    //        if (string.IsNullOrEmpty(className)) return;
    //        if (!CustomPortraits.TryGetValue(className, out var path)) return;
    //        if (!ResourceLoader.Exists(path)) return;
    //        __result = path;
    //    }
    //}

    //[HarmonyPatch(typeof(ArchaicTooth), "GetTranscendenceStarterCard")]
    //internal static class ArchaicToothGetTranscendenceStarterCardPatch
    //{
    //    private static bool Prefix(ArchaicTooth __instance, Player player, ref CardModel? __result)
    //    {
    //        if (player.Character is Character.AkiSister)
    //        {
    //            __result = player.Deck.Cards.FirstOrDefault(c => c is GlowofAutumnSunset or ResentmentofAutumnColors);
    //            return false;
    //        }
    //        return true;
    //    }
    //}
    //
    //[HarmonyPatch(typeof(ArchaicTooth), "GetTranscendenceTransformedCard")]
    //internal static class ArchaicToothGetTranscendenceTransformedCardPatch
    //{
    //    private static bool Prefix(ArchaicTooth __instance, CardModel starterCard, ref CardModel? __result)
    //    {
    //        if (starterCard is GlowofAutumnSunset or ResentmentofAutumnColors)
    //        {
    //            __result = starterCard.Owner.RunState.CreateCard(ModelDb.Card<FinalMasterSpark>(), starterCard.Owner);
    //            if (starterCard.IsUpgraded)
    //            {
    //                CardCmd.Upgrade(__result);
    //            }
    //            return false;
    //        }
    //        return true;
    //    }
    //}

    //[HarmonyPatch(typeof(DustyTome), "SetupForPlayer")]
    //internal static class DustyTomeSetupForPlayerPatch
    //{
    //    private static bool Prefix(DustyTome __instance, Player player)
    //    {
    //        if (player.Character is MarisaCharacter)
    //        {
    //            __instance.AncientCard = ModelDb.Card<MagicAndRedDream>().Id;
    //            return false;
    //        }
//
    //        return true;
    //    }
    //}
}