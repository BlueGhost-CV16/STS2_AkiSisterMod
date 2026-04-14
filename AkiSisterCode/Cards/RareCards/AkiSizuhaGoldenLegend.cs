using AkiSister.AkiSisterCode.Cards;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Nodes;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AkiSister.AkiSisterCode.Cards.RareCards;

public class AkiSizuhaGoldenLegend() : AkiSisterCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override bool ShouldGlowGoldInternal => this.LeafCheck();
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
            HoverTipFactory.FromKeyword(AkiSisterCardKeyWords.RedLeafResonance)
        ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (this.LeafCheck())
        {
            await Owner.LeafAdd_Card(Owner.PlayerCombatState!.Hand.Cards.ToList());
        }
        var cardModels = play.Card.Owner.PlayerCombatState!.Hand.Cards.Where(card => card.LeafCheck()).ToList();
        if (cardModels.Count > 0)
        {
            foreach (var cardModel in cardModels)
                cardModel.EnergyCost.AddThisTurnOrUntilPlayed(-1);
        }
    }
    
    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
        //base.AddKeyword(CardKeyword.Retain);
    }
}