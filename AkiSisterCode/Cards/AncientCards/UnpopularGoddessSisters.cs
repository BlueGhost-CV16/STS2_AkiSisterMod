using AkiSister.AkiSisterCode.Cards;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AkiSister.AkiSisterCode.Cards.AncientCards;

public class UnpopularGoddessSisters() : AkiSisterCard(1,
    CardType.Power, CardRarity.Ancient,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<UnpopularGoddessSistersPower>(1m)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        HoverTipFactory.FromEnchantment<RedLeafMarkEnchantment>().Concat(
            HoverTipFactory.FromEnchantment<SweetPotatoMarkEnchantment>()).Concat(
            [
                HoverTipFactory.FromPower<WitherPower>(),
                HoverTipFactory.FromPower<DrainPower>()
            ]);

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<UnpopularGoddessSistersPower>(Owner.Creature, DynamicVars["UnpopularGoddessSistersPower"].BaseValue, Owner.Creature, this);
    }
    
    protected override void OnUpgrade()
    {
        base.AddKeyword(CardKeyword.Innate);
    }
}