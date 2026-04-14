using AkiSister.AkiSisterCode.Cards;
using AkiSister.AkiSisterCode.Cards.BasicCards;
using AkiSister.AkiSisterCode.Cards.CommonCards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace AkiSister.AkiSisterCode.Cards.RareCards;

public class WellPrepared() : AkiSisterCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<RedLeafStrike>(),
        HoverTipFactory.FromCard<SweetPotatoDefend>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var deck = base.Owner.PlayerCombatState.AllCards;
        foreach (CardModel allCard in deck)
        {
            if (allCard is StrikeAkiSister)
            {
                CardModel cardModel = base.CombatState.CreateCard<RedLeafStrike>(base.Owner);
                if (allCard.IsUpgraded)
                {
                    CardCmd.Upgrade(cardModel);
                }
                await CardCmd.Transform(allCard, cardModel);
            }
            else if (allCard is DefendAkiSister)
            {
                CardModel cardModel = base.CombatState.CreateCard<SweetPotatoDefend>(base.Owner);
                if (allCard.IsUpgraded)
                {
                    CardCmd.Upgrade(cardModel);
                }
                await CardCmd.Transform(allCard, cardModel);
            }
        }
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}