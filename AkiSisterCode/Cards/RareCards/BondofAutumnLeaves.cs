using AkiSister.AkiSisterCode.Cards;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AkiSister.AkiSisterCode.Cards.RareCards;

public class BondofAutumnLeaves() : AkiSisterCard(1,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<BondofAutumnLeavesPower>(1m)];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromEnchantment<RedLeafMarkEnchantment>();

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<BondofAutumnLeavesPower>(choiceContext, Owner.Creature, DynamicVars["BondofAutumnLeavesPower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        //this.LeafAdd_Card();
        AddKeyword(CardKeyword.Retain);
        //base.DynamicVars["BondofAutumnLeavesPower"].UpgradeValueBy(1m);
    }
}