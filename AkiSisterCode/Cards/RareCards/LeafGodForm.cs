using AkiSister.Characters;
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
using MegaCrit.Sts2.Core.Models;

namespace AkiSister.AkiSisterCode.Cards.RareCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class LeafGodForm() : AkiSisterCard(3,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<LeafGodFormPower>(1m),
        //new PowerVar<AutumnAuraPower>(12m)
    ];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Ethereal
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    [
        HoverTipFactory.FromCard<ShepherdandApricotBlossom>(true)
        //HoverTipFactory.FromPower<AutumnAuraPower>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        foreach (CardModel allCard in base.Owner.PlayerCombatState.AllCards)
        {
            if (allCard is ShepherdandApricotBlossom && allCard.IsUpgradable)
            {
                CardCmd.Upgrade(allCard);
            }
        }
        await PowerCmd.Apply<LeafGodFormPower>(choiceContext, Owner.Creature, DynamicVars["LeafGodFormPower"].BaseValue, Owner.Creature, this);
        //await PowerCmd.Apply<AutumnAuraPower>(Owner.Creature, DynamicVars["AutumnAuraPower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Ethereal);
        //DynamicVars["AutumnAuraPower"].UpgradeValueBy(4);
    }
}