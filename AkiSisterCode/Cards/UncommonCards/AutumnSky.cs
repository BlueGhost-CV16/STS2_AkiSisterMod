using AkiSister.Characters;
using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Nodes;
using AkiSister.Characters;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace AkiSister.AkiSisterCode.Cards.UncommonCards;


public class AutumnSky() : AkiSisterCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];
    
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => 
        HoverTipFactory.FromEnchantment<SweetPotatoMarkEnchantment>().Concat([
            HoverTipFactory.FromKeyword(CardKeyword.Exhaust),
            HoverTipFactory.FromCard<HarvesterandPearBlossom>()
    ]);

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        List<CardModel> cards = CardFactory.GetDistinctForCombat(base.Owner, from c in base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
            where c.Type == CardType.Skill && c.Rarity != CardRarity.Rare
            select c, 3, base.Owner.RunState.Rng.CombatCardGeneration).ToList();
        if (base.IsUpgraded)
        {
            CardCmd.Upgrade(cards, CardPreviewStyle.HorizontalLayout);
        }
        foreach (CardModel card in cards)
        {
            await Owner.PotatoAdd_Card(card);
            CardCmd.ApplyKeyword(card, CardKeyword.Exhaust);
            card.EnergyCost.AddThisCombat(-1);
        }
        CardModel? cardModel = await CardSelectCmd.FromChooseACardScreen(choiceContext, cards, base.Owner, canSkip: true);
        if (cardModel != null)
        {
            await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, base.Owner);
        }
        await base.Owner.GrassAdd_Deck(base.CombatState, (int)DynamicVars.Cards.BaseValue);
        //CardModel cardModel = CardFactory.GetDistinctForCombat(base.Owner, from c in base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
        //    where c.Type == CardType.Attack
        //    select c, 1, base.Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault();
        //if (cardModel != null)
        //{
        //    cardModel.SetToFreeThisTurn();
        //    await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, addedByPlayer: true);
        //}
    }

    protected override void OnUpgrade()
    {

    }
}