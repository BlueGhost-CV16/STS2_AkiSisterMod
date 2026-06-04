using AkiSister.AkiSisterCode.Cards.AncientCards;
using AkiSister.AkiSisterCode.Cards.BasicCards;
using AkiSister.AkiSisterCode.Cards.FakeCards;
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Rooms;
using STS2RitsuLib.Patching.Models;

namespace AkiSister.AkiSisterCode;

[HarmonyPatch]
public class AkiSisterArchaicToothPatch
{
    private static CardModel? _starterCardAkiSizuha = null;
    private static CardModel? _starterCardAkiMinoriko = null;
    //private static CardModel? _transformedCardAkiSizuha = null;
    //private static CardModel? _transformedCardAkiMinoriko = null;
    
    [HarmonyPatch(typeof(ArchaicTooth), "GetTranscendenceStarterCard")]
    public static class ArchaicToothGetTranscendenceStarterCardPatch// : IPatchMethod
    {
        //public static string PatchId => "AkiSister_Archaic_Tooth_Get_Transcendence_Starter_Card_Patch";
        //public static string Description => "秋姐妹的古老牙齿patch，用于实现同时升级2个遗物。";
        //public static bool IsCritical => true;
//
        //public static ModPatchTarget[] GetTargets() =>
        //[
        //    new(typeof(ArchaicTooth), "GetTranscendenceStarterCard"),
        //];
        
        private static bool Prefix(ArchaicTooth __instance, Player player, ref CardModel? __result)
        {
            if (player.Character is not Characters.AkiSisterCharacter) return true;
            _starterCardAkiSizuha = player.Deck.Cards.FirstOrDefault(c => c is GlowofAutumnSunset);
            _starterCardAkiMinoriko = player.Deck.Cards.FirstOrDefault(c => c is ResentmentofAutumnColors);
            if (_starterCardAkiSizuha != null && _starterCardAkiMinoriko != null)
            {
                __result = player.RunState.CreateCard(ModelDb.Card<AkiSisterFakeStarterCard>(), player);
            }
            else if (_starterCardAkiSizuha != null)
            {
                __result = _starterCardAkiSizuha;
            }
            else if (_starterCardAkiMinoriko != null)
            {
                __result = _starterCardAkiMinoriko;
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(ArchaicTooth), "GetTranscendenceTransformedCard")]
    public static class ArchaicToothGetTranscendenceTransformedCardPatch// : IPatchMethod
    {
        //public static string PatchId => "AkiSister_Archaic_Tooth_Get_Transcendence_Transformed_Card_Patch";
        //public static string Description => "秋姐妹的古老牙齿patch，用于实现同时升级2个遗物。";
        //public static bool IsCritical => true;
//
        //public static ModPatchTarget[] GetTargets() =>
        //[
        //    new(typeof(ArchaicTooth), "GetTranscendenceTransformedCard"),
        //];

        private static bool Prefix(ArchaicTooth __instance, CardModel starterCard, ref CardModel? __result)
        {
            switch (starterCard)
            {
                case AkiSisterFakeStarterCard:
                    __result = starterCard.Owner.RunState.CreateCard(ModelDb.Card<AkiSisterFakeTransformedCard>(), starterCard.Owner);
                    return false;
                case GlowofAutumnSunset:
                    __result = TransformedCard(starterCard, true);
                    return false;
                case ResentmentofAutumnColors:
                    __result = TransformedCard(starterCard, false);
                    return false;
                default:
                    return true;
            }
        }
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(ArchaicTooth), nameof(ArchaicTooth.AfterObtained))]
    private static bool AfterObtainedPrefix(ArchaicTooth __instance, ref Task __result)
    {
        if (__instance.Owner.Character is not Characters.AkiSisterCharacter || _starterCardAkiSizuha == null ||
            _starterCardAkiMinoriko == null) return true;
        _starterCardAkiSizuha = __instance.Owner.Deck.Cards.FirstOrDefault(c => c is GlowofAutumnSunset);
        _starterCardAkiMinoriko = __instance.Owner.Deck.Cards.FirstOrDefault(c => c is ResentmentofAutumnColors);
        CardCmd.Transform(_starterCardAkiSizuha, TransformedCard(_starterCardAkiSizuha, true));
        CardCmd.Transform(_starterCardAkiMinoriko, TransformedCard(_starterCardAkiMinoriko, false));
        __result = Task.CompletedTask;
        return false;
    }

    private static CardModel TransformedCard(CardModel starterCard, bool sizuha)
    {
        var transformedCard = sizuha ? starterCard.Owner.RunState.CreateCard(ModelDb.Card<RedRainofFallenLeaves>(), starterCard.Owner) : starterCard.Owner.RunState.CreateCard(ModelDb.Card<GoldenBreezeofAbundance>(), starterCard.Owner);
        if (starterCard.IsUpgraded)
        {
            CardCmd.Upgrade(transformedCard);
        }
        if (starterCard.Enchantment != null)
        {
            EnchantmentModel enchantmentModel = (EnchantmentModel)starterCard.Enchantment.MutableClone();
            CardCmd.Enchant(enchantmentModel, transformedCard, enchantmentModel.Amount);
        }
        return transformedCard;
    }

    [HarmonyPatch(typeof(DustyTome), "SetupForPlayer")]
    internal static class DustyTomeSetupForPlayerPatch
    {
        private static bool Prefix(DustyTome __instance, Player player)
        {
            if (player.Character is Characters.AkiSisterCharacter)
            {
                __instance.AncientCard = ModelDb.Card<UnpopularGoddessSisters>().Id;
                return false;
            }
            return true;
        }
    }
    //private static readonly MethodInfo? GetTranscendenceStarterCard =
    //    AccessTools.DeclaredMethod(typeof(ArchaicTooth), "GetTranscendenceStarterCard");
    //
    //[HarmonyPatch(typeof(ArchaicTooth), nameof(ArchaicTooth.SetupForPlayer))]
    //internal static class ArchaicToothSetupForPlayerPatch
    //{
    //    [HarmonyPostfix]
    //    private static void Postfix(ArchaicTooth __instance, Player player, ref bool __result)
    //    {
    //        if (__result)
    //        {
    //            return;
    //        }
    //        CardModel? switchCard_Leaf = player.Deck.Cards.FirstOrDefault(c => c is ResentmentofAutumnColors);
    //        CardModel? switchCard_Potato = player.Deck.Cards.FirstOrDefault(c => c is GlowofAutumnSunset);
    //        if (switchCard_Leaf == null && switchCard_Potato == null)
    //        {
    //            return;
    //        }
    //        //NamieFamilyRelic? namie = player.GetRelic<NamieFamilyRelic>();
    //        if (player.Character is not Character.AkiSister)
    //        {
    //            return;
    //        }
    //        //((IEnumerable<DynamicVar>)__instance.GetType().GetField("CanonicalVars", AccessTools.all).GetValue(__instance)) => ;
    //        //if (GetTranscendenceStarter(__instance, player) != null)
    //        //{
    //        //    return;
    //        //}
    //        //CardModel ancientCanonical_Leaf = player.RunState.CreateCard(ModelDb.Card<RedRainofFallenLeaves>(), player);
    //        //CardModel ancientCanonical_Potato = player.RunState.CreateCard(ModelDb.Card<GoldenBreezeofAbundance>(), player);
    //        //if (switchCard_Leaf != null)
    //        //{
    //        //    __instance.SetupForTests(switchCard_Leaf.ToSerializable(), previewAncient.ToSerializable());
    //        //}
    //        //if (switchCard_Potato != null)
    //        //__result = true;
    //    }
    //}
    //
    //[HarmonyPrefix]
    //[HarmonyPatch(typeof(ArchaicTooth), nameof(ArchaicTooth.AfterObtained))]
    //private static bool AfterObtainedPrefix(ArchaicTooth __instance, ref Task __result)
    //{
    //    Player owner = __instance.Owner;
    //    //NamieFamilyRelic? namie = owner.GetRelic<NamieFamilyRelic>();
    //    bool isNamieCharacter = owner.Character is NamieFamilyMainCharacter;
    //    if (!isNamieCharacter)
    //    {
    //        return true;
    //    }
    //    namie?.EnsureArchaicToothAncientRoll();
    //    CardModel? switchCard = owner.Deck.Cards.FirstOrDefault(c => c.Id == ModelDb.Card<CardGroupSwitch>().Id);
    //    if (switchCard == null)
    //    {
    //        return true;
    //    }
//
    //    if (GetTranscendenceStarter(__instance, owner) != null)
    //    {
    //        return true;
    //    }
    //    __result = NamieArchaicToothAfterObtainedAsync(__instance, owner, switchCard, namie);
    //    return false;
    //}
//
    //private static CardModel? GetTranscendenceStarter(ArchaicTooth tooth, Player player)
    //{
    //    if (GetTranscendenceStarterCard == null)
    //    {
    //        return null;
    //    }
    //    return (CardModel?)GetTranscendenceStarterCard.Invoke(tooth, new object[] { player });
    //}
//
    //private static async Task NamieArchaicToothAfterObtainedAsync(
    //    ArchaicTooth tooth,
    //    Player owner,
    //    CardModel switchCard,
    //    NamieFamilyRelic? namie)
    //{
    //    CardModel ancientCanonical = namie != null
    //        ? namie.GetArchaicToothAncientCanonical()
    //        : ResolveNamieAncientCanonicalFromToothPreview(tooth);
    //    CardModel newCard = owner.RunState.CreateCard(ancientCanonical, owner);
    //    if (switchCard.IsUpgraded)
    //    {
    //        CardCmd.Upgrade(newCard);
    //    }
    //    if (switchCard.Enchantment != null)
    //    {
    //        EnchantmentModel enchantmentModel = (EnchantmentModel)switchCard.Enchantment.MutableClone();
    //        CardCmd.Enchant(enchantmentModel, newCard, enchantmentModel.Amount);
    //    }
    //    await CardCmd.Transform(switchCard, newCard);
    //}
//
    //private static CardModel ResolveNamieAncientCanonicalFromToothPreview(ArchaicTooth tooth)
    //{
    //    if (tooth.AncientCard == null)
    //    {
    //        return ModelDb.Card<CardGGShiningWill>();
    //    }
    //    CardModel deserialized = CardModel.FromSerializable(tooth.AncientCard);
    //    return ModelDb.GetById<CardModel>(deserialized.Id);
    //}
}