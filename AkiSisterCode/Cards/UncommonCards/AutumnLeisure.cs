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

namespace AkiSister.AkiSisterCode.Cards.UncommonCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class AutumnLeisure() : AkiSisterCard(1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AutumnLeisurePower>(1)];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => HoverTipFactory
        .FromEnchantment<RedLeafMarkEnchantment>()
        .Concat(HoverTipFactory.FromEnchantment<SweetPotatoMarkEnchantment>());

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<AutumnLeisurePower>(choiceContext, Owner.Creature, DynamicVars["AutumnLeisurePower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
        //AddKeyword(CardKeyword.Innate);
        //base.DynamicVars["AutumnLeisurePower"].UpgradeValueBy(1m);
    }
}