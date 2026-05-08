using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Nodes;
using AkiSister.AkiSisterCode.Powers;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Patches.UI;
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
using MegaCrit.Sts2.Core.Models.Powers;

namespace AkiSister.AkiSisterCode.Cards.StatusCards;

[Pool(typeof(TokenCardPool))]
public class ShepherdandApricotBlossom() : CustomCardModel(1,
    CardType.Status, CardRarity.Token,
    TargetType.None)
{
    public override void AfterCreated()
    {
        //base.AfterCreated();
        this.LeafAdd_Card();
    }

    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    
    //public override int MaxUpgradeLevel => 0;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CardsVar(1),
        new PowerVar<WitherPower>(1m),
        //new PowerVar<AutumnAuraPower>(1m),
    ];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        //CardKeyword.Exhaust,
        AkiSisterCardKeyWords.RedLeafFavor
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
            HoverTipFactory.FromPower<WitherPower>()
    ];

    //public override bool HasTurnEndInHandEffect => true;
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
        //if (base.IsUpgraded)
        //{
        //    await PowerCmd.Apply<AutumnAuraPower>(Owner.Creature, base.DynamicVars["AutumnAuraPower"].BaseValue ,Owner.Creature, this);
        //}
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
        DynamicVars["WitherPower"].UpgradeValueBy(1);
    }

    public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel? source)
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
        CombatState? combatState = card.Owner.Creature.CombatState;
        if (combatState == null)
        {
            return;
        }
        Creature enemy = card.Owner.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies);
        if (enemy != null)
        {
            await PowerCmd.Apply<WitherPower>(enemy, DynamicVars["WitherPower"].BaseValue, card.Owner.Creature, card);
        }
        
        if (card.LeafCheck() || card.PotatoCheck())
        {
            (card.Enchantment as RedLeafMarkEnchantment)?.StatusChange();
            (card.Enchantment as SweetPotatoMarkEnchantment)?.StatusChange();
            card.ClearEnchantmentInternal();
        }
        
        HookPlayerChoiceContext ctx = new HookPlayerChoiceContext(card, LocalContext.NetId.Value, combatState, GameActionType.Combat);
        //if (Enchantment is SweetPotatoMarkEnchantment)
        //{
        //    //await PowerCmd.Apply<DrawCardsNextTurnPower>(Owner.Creature, 1, Owner.Creature, this);
        //    await CardPileCmd.Draw(ctx, card.DynamicVars.Cards.BaseValue, card.Owner);
        //}
        await CardCmd.Exhaust(ctx, card, false, true);
    }
}