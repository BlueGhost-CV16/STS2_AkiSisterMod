using AkiSister.Characters;
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

public class IndulgenceofAutumnGoddessSisters() : AkiSisterCard(1,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<IndulgenceofAutumnGoddessSistersPower>(1m),
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    [
        HoverTipFactory.FromPower<WitherPower>(),
        HoverTipFactory.FromPower<DrainPower>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<IndulgenceofAutumnGoddessSistersPower>(choiceContext, Owner.Creature, DynamicVars["IndulgenceofAutumnGoddessSistersPower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        //AddKeyword(CardKeyword.Innate);
        DynamicVars["IndulgenceofAutumnGoddessSistersPower"].UpgradeValueBy(1);
    }
}