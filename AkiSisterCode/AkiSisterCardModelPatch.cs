using AkiSister.AkiSisterCode.Cards.StatusCards;
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode;

[HarmonyPatch]
public static class AkiSisterCardModelPatch
{
    [HarmonyPatch(typeof(CardModel), nameof(CardModel.OnTurnEndInHandWrapper))]
    internal static class CardModelOnTurnEndInHandWrapperPatch
    {
        private static bool Prefix(CardModel __instance, PlayerChoiceContext choiceContext, ref Task __result)
        {
            if (__instance is not ShepherdandApricotBlossom && __instance is not HarvesterandPearBlossom) 
                return true;
            __result = AkiOnTurnEndInHandWrapper(__instance,  choiceContext);
            return false;
        }
        private static async Task AkiOnTurnEndInHandWrapper(CardModel __instance, PlayerChoiceContext choiceContext)
        {
            await CardPileCmd.Add(__instance, PileType.Play);
            if (LocalContext.IsMe(__instance.Owner))
            {
            }
            await Traverse.Create(__instance).Method("OnTurnEndInHand", [typeof(PlayerChoiceContext)])
                .GetValue<Task>(choiceContext);
            if (__instance.Keywords.Contains(CardKeyword.Ethereal))
            {
                await CardCmd.Exhaust(choiceContext, __instance, causedByEthereal: true);
                return;
            }
            PileType pileType = __instance.Keywords.Contains(CardKeyword.Retain) ? PileType.Hand : PileType.Discard;
            CardPile pile = pileType.GetPile(__instance.Owner);
            await CardPileCmd.Add(__instance, pile);
        }
    }
}