using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Nodes;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Relics;

public class AkiSizuhaFlowers() : AkiSisterRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<ShepherdandApricotBlossom>(true),
        HoverTipFactory.FromKeyword(AkiSisterCardKeyWords.RedLeafMark)
    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];
    
    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
    {
        if (side != CombatSide.Player)
        {
            return;
        }
        Flash();
        await base.Owner.FlowerAdd_Deck(combatState, upgrade: true);
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner)
        {
            return;
        }
        Flash();
        await player.LeafAdd_Hand();
    }
    
    public override decimal ModifyHandDraw(Player player, decimal count)
    {
        if (player != base.Owner)
        {
            return count;
        }
        if (player.Creature.CombatState.RoundNumber > 1)
        {
            return count;
        }
        return count + base.DynamicVars.Cards.BaseValue;
    }
}