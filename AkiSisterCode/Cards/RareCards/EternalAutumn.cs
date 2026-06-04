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


public class EternalAutumn() : AkiSisterCard(2,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<EternalAutumnPower>(1m),
    ];
    
    //public override IEnumerable<CardKeyword> CanonicalKeywords => [
    //    CardKeyword.Ethereal
    //];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        //HoverTipFactory.FromCard<HarvesterandPearBlossom>(true)
        HoverTipFactory.FromPower<AutumnAuraPower>(),
        HoverTipFactory.FromPower<AutumnAuraLostPower>(),
        HoverTipFactory.FromPower<FragrancePower>(),
        HoverTipFactory.FromPower<FragranceLostPower>(),
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        //foreach (CardModel allCard in base.Owner.PlayerCombatState.AllCards)
        //{
        //    if (allCard is HarvesterandPearBlossom && allCard.IsUpgradable)
        //    {
        //        CardCmd.Upgrade(allCard);
        //    }
        //}
        await PowerCmd.Apply<EternalAutumnPower>(choiceContext, Owner.Creature, DynamicVars["EternalAutumnPower"].BaseValue, Owner.Creature, this);
        //await PowerCmd.Apply<FragrancePower>(Owner.Creature, DynamicVars["FragrancePower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
        //DynamicVars["FragrancePower"].UpgradeValueBy(4);
    }
}