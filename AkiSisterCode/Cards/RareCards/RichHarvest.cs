using AkiSister.AkiSisterCode.Cards;
using AkiSister.AkiSisterCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace AkiSister.AkiSisterCode.Cards.RareCards;

public class RichHarvest() : AkiSisterCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int num = 0;
        var pile = PileType.Exhaust.GetPile(base.Owner).Cards.Where((CardModel card) =>
            card.Type == CardType.Status && !card.Keywords.Contains(CardKeyword.Unplayable)).ToList();
        foreach (CardModel item in pile)
        {
            if (!item.Keywords.Contains(CardKeyword.Exhaust)) 
                item.AddKeyword(CardKeyword.Exhaust);
            item.EnergyCost.AddUntilPlayed(-1);
            if (num % 2 == 0)
            {
                await item.LeafAdd_Card();
            }
            else
            {
                await item.PotatoAdd_Card();
            }
            await CardPileCmd.Add(pile, PileType.Draw, CardPilePosition.Random);
            //CardCmd.PreviewCardPileAdd()
        }
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}