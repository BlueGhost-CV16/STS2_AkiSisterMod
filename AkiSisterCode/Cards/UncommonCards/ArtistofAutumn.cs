using AkiSister.Characters;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.Characters;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AkiSister.AkiSisterCode.Cards.UncommonCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class ArtistofAutumn() : AkiSisterCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.None)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CardsVar(2)
    ];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust,
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        //await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        var num = Math.Min(base.DynamicVars.Cards.IntValue,
            CardPile.MaxCardsInHand - PileType.Hand.GetPile(base.Owner).Cards.Count);
        if (num > 0)
        {
            await CardPileCmd.Add(
                await CardSelectCmd.FromSimpleGrid(choiceContext, PileType.Discard.GetPile(base.Owner).Cards,
                    base.Owner, new CardSelectorPrefs(base.SelectionScreenPrompt, num)), PileType.Hand);
        }
        await base.Owner.FlowerAdd_Deck(base.CombatState);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}