using AkiSister.Characters;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Powers;
using AkiSister.Characters;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AkiSister.AkiSisterCode.Cards.RareCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class BountifulofAutumnFruits() : AkiSisterCard(1,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<BountifulofAutumnFruitsPower>(1m)];
    
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => HoverTipFactory.FromEnchantment<SweetPotatoMarkEnchantment>();

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<BountifulofAutumnFruitsPower>(choiceContext, Owner.Creature, DynamicVars["BountifulofAutumnFruitsPower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        //this.PotatoAdd_Card();
        AddKeyword(CardKeyword.Retain);
        //base.DynamicVars["BountifulofAutumnFruitsPower"].UpgradeValueBy(1m);
    }
}