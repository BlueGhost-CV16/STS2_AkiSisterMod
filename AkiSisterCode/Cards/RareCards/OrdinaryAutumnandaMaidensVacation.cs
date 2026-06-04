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


public class OrdinaryAutumnandaMaidensVacation() : AkiSisterCard(-1,
    CardType.Skill, CardRarity.Rare,
    TargetType.None)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CardsVar(2),
        new EnergyVar(2),
        new DynamicVar("Count", 6),
        new DynamicVar("NowCount", 0),
    ];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Unplayable,
        CardKeyword.Retain
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    [
        base.EnergyHoverTip,
        HoverTipFactory.FromCard<ShepherdandApricotBlossom>(),
        HoverTipFactory.FromCard<HarvesterandPearBlossom>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        //await PowerCmd.Apply<PoisonedApplePower>(Owner.Creature, DynamicVars["HarvestGodFormPower"].BaseValue, Owner.Creature, this);
    }

    private int _count;

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card.Owner == Owner && Pile.Type == PileType.Hand && card is ShepherdandApricotBlossom or HarvesterandPearBlossom)
        {
            _count++;
            await CardCmd.Exhaust(choiceContext, card);
            if (_count >= DynamicVars["Count"].BaseValue)
            {
                _count = 0;
                await CardCmd.Discard(choiceContext, this);
                await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, Owner);
                await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
            }
            await CardPileCmd.Draw(choiceContext, Owner);
            DynamicVars["NowCount"].BaseValue = _count;
        }
    }

    public override Task BeforeCombatStart()
    {
        _count = 0;
        return Task.CompletedTask;
    }
    
    protected override void OnUpgrade()
    {
        //AddKeyword(CardKeyword.Innate);
        //base.EnergyCost.UpgradeBy(-1);
        //DynamicVars["Count"].UpgradeValueBy(3);
        DynamicVars.Cards.UpgradeValueBy(1);
        DynamicVars.Energy.UpgradeValueBy(1);
    }
}