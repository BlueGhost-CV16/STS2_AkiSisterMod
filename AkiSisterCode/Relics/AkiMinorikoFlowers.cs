using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Nodes;
using AkiSister.AkiSisterCode.Relics;
using AkiSister.Characters;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using STS2RitsuLib.Interop.AutoRegistration;
using AkiSister.Characters;

namespace AkiSister.AkiSisterCode.Relics;
[RegisterRelic(typeof(AkiSisterRelicPool))]

public class AkiMinorikoFlowers() : AkiSisterRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;
    
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => 
        HoverTipFactory.FromEnchantment<SweetPotatoMarkEnchantment>().Concat([
            HoverTipFactory.FromCard<HarvesterandPearBlossom>(true),
            //HoverTipFactory.FromKeyword(AkiSisterCardKeyWords.SweetPotatoMark)
        ]);
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];
    
    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants,
        ICombatState combatState)
    {
        if (side != CombatSide.Player)
        {
            return;
        }
        Flash();
        await base.Owner.GrassAdd_Deck(combatState, upgrade: true);
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner)
        {
            return;
        }
        Flash();
        await player.PotatoAdd_Hand();
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