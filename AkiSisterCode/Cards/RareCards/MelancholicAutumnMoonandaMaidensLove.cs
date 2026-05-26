using AkiSister.AkiSisterCode.Cards.StatusCards;
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

public class MelancholicAutumnMoonandaMaidensLove() : AkiSisterCard(2,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<MelancholicAutumnMoonandaMaidensLovePower>(2m)];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    [
        HoverTipFactory.FromCard<ShepherdandApricotBlossom>(),
        HoverTipFactory.FromPower<AutumnAuraPower>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<MelancholicAutumnMoonandaMaidensLovePower>(choiceContext, Owner.Creature, DynamicVars["MelancholicAutumnMoonandaMaidensLovePower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["MelancholicAutumnMoonandaMaidensLovePower"].UpgradeValueBy(1m);
    }
}