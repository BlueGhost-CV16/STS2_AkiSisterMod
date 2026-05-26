using AkiSister.Characters;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Powers;
using AkiSister.Characters;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2RitsuLib.Interop.AutoRegistration;
using AkiSister.Characters;

namespace AkiSister.AkiSisterCode.Cards.AncientCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class UnpopularGoddessSisters() : AkiSisterCard(2,
    CardType.Power, CardRarity.Ancient,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<UnpopularGoddessSistersPower>(1m)
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => 
        //HoverTipFactory.FromEnchantment<RedLeafMarkEnchantment>().Concat(
            //HoverTipFactory.FromEnchantment<SweetPotatoMarkEnchantment>()).Concat(
            [
                HoverTipFactory.FromPower<WitherPower>(),
                HoverTipFactory.FromPower<DrainPower>()
            ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<UnpopularGoddessSistersPower>(choiceContext, Owner.Creature, DynamicVars["UnpopularGoddessSistersPower"].BaseValue, Owner.Creature, this);
    }
    
    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
        //base.AddKeyword(CardKeyword.Innate);
    }
}