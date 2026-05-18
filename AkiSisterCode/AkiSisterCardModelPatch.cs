using System.Diagnostics;
using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Powers;
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.GameInfo.Objects;

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
                //await Cmd.CustomScaledWait(0.1f, 0.2f);
            }
            //var method = AccessTools.Method(typeof(CardModel), "OnTurnEndInHand", [typeof(PlayerChoiceContext)]);
            //if (method == null)
            //{
            //    Console.WriteLine("OnTurnEndInHand方法查找失败！");
            //}
            //else
            //{
                //Console.WriteLine("OnTurnEndInHand方法查找成功！");
                await Traverse.Create(__instance).Method("OnTurnEndInHand", [typeof(PlayerChoiceContext)])
                    .GetValue<Task>(choiceContext);
            //}
            //if (__instance is ShepherdandApricotBlossom)
            //{
            //    Creature enemy = __instance.Owner.RunState.Rng.CombatTargets.NextItem(__instance.CombatState.HittableEnemies);
            //    if (enemy != null)
            //    {
            //        await PowerCmd.Apply<WitherPower>(new ThrowingPlayerChoiceContext(), enemy,
            //            __instance.DynamicVars["WitherPower"].BaseValue, __instance.Owner.Creature, __instance);
            //    }
            //}
            //else
            //{
            //    Creature enemy = __instance.Owner.RunState.Rng.CombatTargets.NextItem(__instance.CombatState.HittableEnemies);
            //    if (enemy != null)
            //    {
            //        await PowerCmd.Apply<DrainPower>(new ThrowingPlayerChoiceContext(), enemy,
            //            __instance.DynamicVars["DrainPower"].BaseValue, __instance.Owner.Creature, __instance);
            //    }
            //}
            if (__instance.Keywords.Contains(CardKeyword.Ethereal))
            {
                await CardCmd.Exhaust(choiceContext, __instance, causedByEthereal: true);
                return;
            }

            PileType pileType = PileType.Hand;
            //var method1 = AccessTools.Method(typeof(CardModel), "GetResultPileTypeForOnTurnEndInHandEffect", []);
            //if (method1 == null)
            //{
            //    Console.WriteLine("GetResultPileTypeForOnTurnEndInHandEffect方法查找失败！");
            //}
            //else
            //{
                //Console.WriteLine("GetResultPileTypeForOnTurnEndInHandEffect方法查找成功！");
                pileType = Traverse.Create(__instance).Method("GetResultPileTypeForOnTurnEndInHandEffect", [])
                    .GetValue<PileType>();
            //}
            CardPile pile = pileType.GetPile(__instance.Owner);
            await CardPileCmd.Add(__instance, pile);
            //await CardPileCmd.Add(__instance, PileType.Hand);
        }
    }
}