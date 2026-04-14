using AkiSister.AkiSisterCode.Relics;
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;

namespace AkiSister.AkiSisterCode;

[HarmonyPatch]
internal static class AkiSisterTouchOfOrobasPatch
{
    private static RelicModel? _starterRelicAkiSizuha = null;
    private static RelicModel? _starterRelicAkiMinoriko = null;
    private static RelicModel? _transformedRelicAkiSizuha = null;
    private static RelicModel? _transformedRelicAkiMinoriko = null;
    
    [HarmonyPatch(typeof(TouchOfOrobas), "GetStarterRelic")]
    internal static class TouchOfOrobasGetStarterRelicPatch
    {
        private static bool Prefix(TouchOfOrobas __instance, Player p, ref RelicModel? __result)
        {
            if (p.Character is not Character.AkiSister) return true;
            _starterRelicAkiSizuha = p.Relics.FirstOrDefault(c => c is AkiSizuhaGrasses);
            _starterRelicAkiMinoriko = p.Relics.FirstOrDefault(c => c is AkiMinorikoGrasses);
            if (_starterRelicAkiSizuha != null && _starterRelicAkiMinoriko != null)
            {
                __result = ModelDb.Relic<AkiSisterGrasses>().ToMutable();
            }
            else if (_starterRelicAkiSizuha != null)
            {
                __result = _starterRelicAkiSizuha;
            }
            else if (_starterRelicAkiMinoriko != null)
            {
                __result = _starterRelicAkiMinoriko;
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(TouchOfOrobas), "GetUpgradedStarterRelic")]
    internal static class TouchOfOrobasGetTranscendenceTransformedCardPatch
    {
        private static bool Prefix(TouchOfOrobas __instance, RelicModel starterRelic, ref RelicModel? __result)
        {
            if (starterRelic is AkiSisterGrasses)
            {
                __result = ModelDb.Relic<AkiSisterFlowers>().ToMutable();
                _transformedRelicAkiSizuha =
                    ModelDb.Relic<AkiSizuhaFlowers>().ToMutable();
                _transformedRelicAkiMinoriko =
                    ModelDb.Relic<AkiMinorikoFlowers>().ToMutable();
                return false;
            }
            else if (starterRelic is AkiSizuhaGrasses)
            {
                _transformedRelicAkiSizuha =
                    ModelDb.Relic<AkiSizuhaFlowers>().ToMutable();
                __result = _transformedRelicAkiSizuha;
                return false;
            }
            else if (starterRelic is AkiMinorikoGrasses)
            {
                _transformedRelicAkiMinoriko =
                    ModelDb.Relic<AkiMinorikoFlowers>().ToMutable();
                __result = _transformedRelicAkiMinoriko;
                return false;
            }
            return true;
        }
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(TouchOfOrobas), nameof(TouchOfOrobas.AfterObtained))]
    private static bool AfterObtainedPrefix(TouchOfOrobas __instance, ref Task __result)
    {
        if (__instance.Owner.Character is not Character.AkiSister || _starterRelicAkiSizuha == null ||
            _starterRelicAkiMinoriko == null) return true;
        RelicCmd.Replace(__instance.Owner.GetRelicById(_starterRelicAkiSizuha.Id), _transformedRelicAkiSizuha ?? ModelDb.Relic<AkiSizuhaFlowers>().ToMutable());
        RelicCmd.Replace(__instance.Owner.GetRelicById(_starterRelicAkiMinoriko.Id), _transformedRelicAkiMinoriko ?? ModelDb.Relic<AkiMinorikoFlowers>().ToMutable());
        __result = Task.CompletedTask;
        return false;
    }
}