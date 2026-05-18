using AkiSister.AkiSisterCode.Cards;
using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AkiSister.AkiSisterCode.Cards.RareCards;

public class TacitApprovalofRedLeafGod() : AkiSisterCard(1,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<TacitApprovalofRedLeafGodPower>(1m)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromCard<ShepherdandApricotBlossom>(),
        HoverTipFactory.FromPower<AutumnAuraPower>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<TacitApprovalofRedLeafGodPower>(choiceContext, Owner.Creature, DynamicVars["TacitApprovalofRedLeafGodPower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["TacitApprovalofRedLeafGodPower"].UpgradeValueBy(1);
        //base.AddKeyword(CardKeyword.Innate);
        //this.LeafAdd_Card();
    }
}