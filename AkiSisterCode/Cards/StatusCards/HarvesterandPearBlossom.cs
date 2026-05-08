using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Powers;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Multiplayer;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace AkiSister.AkiSisterCode.Cards.StatusCards;

[Pool(typeof(TokenCardPool))]
public class HarvesterandPearBlossom() : CustomCardModel(1,
    CardType.Status, CardRarity.Token,
    TargetType.None)
{
    public override void AfterCreated()
    {
        //base.AfterCreated();
        this.PotatoAdd_Card();
    }

    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    
    //public override int MaxUpgradeLevel => 0;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CardsVar(1),
        new PowerVar<DrainPower>(1m),
        new PowerVar<FragrancePower>(1m),
    ];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        //CardKeyword.Exhaust,
        //AkiSisterCardKeyWords.SweetPotatoFavor
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
            HoverTipFactory.FromPower<DrainPower>()
    ];
    
    public override bool HasTurnEndInHandEffect => true;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner);
        //if (base.IsUpgraded)
        //{
            await PowerCmd.Apply<FragrancePower>(choiceContext, Owner.Creature, base.DynamicVars["FragrancePower"].BaseValue ,Owner.Creature, this);
        //}
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
        //DynamicVars["DrainPower"].UpgradeValueBy(1);
        //DynamicVars["FragrancePower"].UpgradeValueBy(1);
    }

    public override async Task AfterCardChangedPilesLate(CardModel card, PileType oldPileType, AbstractModel? source)
    {
        if (card != this || CombatManager.Instance.IsOverOrEnding)
        {
            return;
        }
        if (card.Pile?.Type != PileType.Discard)
        {
            return;
        }
        if (!LocalContext.NetId.HasValue)
        {
            return;
        }
        ICombatState? combatState = card.Owner.Creature.CombatState;
        if (combatState == null)
        {
            return;
        }
        //Creature enemy = card.Owner.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies);
        //if (enemy != null)
        //{
        //    await PowerCmd.Apply<DrainPower>(new ThrowingPlayerChoiceContext(), enemy, base.DynamicVars["DrainPower"].BaseValue ,card.Owner.Creature, card);
        //}  
        
        //if (card.LeafCheck() || card.PotatoCheck())
        //{
        //    (card.Enchantment as RedLeafMarkEnchantment)?.StatusChange();
        //    (card.Enchantment as SweetPotatoMarkEnchantment)?.StatusChange();
        //    card.ClearEnchantmentInternal();
        //}
        
        HookPlayerChoiceContext ctx = new HookPlayerChoiceContext(card, LocalContext.NetId.Value, combatState, GameActionType.Combat);
        //if (Enchantment is RedLeafMarkEnchantment)
        //{
        //    //await PowerCmd.Apply<DrawCardsNextTurnPower>(Owner.Creature, 1, Owner.Creature, this);
        //    await CardPileCmd.Draw(ctx, card.DynamicVars.Cards.BaseValue, card.Owner);
        //}
        await CardCmd.Exhaust(ctx, card, false, true);
    }

    protected override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
    {
        Creature enemy = Owner.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies);
        if (enemy != null)
        {
            await PowerCmd.Apply<DrainPower>(new ThrowingPlayerChoiceContext(), enemy,
                DynamicVars["DrainPower"].BaseValue, Owner.Creature, this);
        }

        if (this.PotatoCheck())
        {
            await CardCmd.DiscardAndDraw(choiceContext, new List<CardModel> { this }, 1);
            //await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
        }
    }

    protected override PileType GetResultPileTypeForOnTurnEndInHandEffect()
    {
        return base.Keywords.Contains(CardKeyword.Retain) ? PileType.Hand : PileType.Discard;
    }
}