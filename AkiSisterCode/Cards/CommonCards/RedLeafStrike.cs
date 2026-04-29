using AkiSister.AkiSisterCode.Cards;
using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Nodes;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace AkiSister.AkiSisterCode.Cards.CommonCards;

public class RedLeafStrike() : AkiSisterCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    //public override void AfterCreated()
    //{
    //    base.AfterCreated();
    //    this.LeafAdd_Card();
    //}

    protected override HashSet<CardTag> CanonicalTags =>
    [
        CardTag.Strike
    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(9m, ValueProp.Move),
        new CardsVar(1),
        new EnergyVar(1)
    ];
    
    //public override IEnumerable<CardKeyword> CanonicalKeywords => [
    //    AkiSisterCardKeyWords.RedLeafFavor
    //];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        base.EnergyHoverTip,
        HoverTipFactory.FromCard<ShepherdandApricotBlossom>()
    ];
    //HoverTipFactory.FromEnchantment<RedLeafMarkEnchantment>();

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Execute(choiceContext);
        await Owner.FlowerAdd_Deck(base.CombatState);
        await PowerCmd.Apply<EnergyNextTurnPower>(Owner.Creature,
            1, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }

    //public override async Task BeforeCombatStart()
    //{
    //    await this.LeafAdd_Card();
    //}
}