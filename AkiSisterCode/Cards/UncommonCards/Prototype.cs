using AkiSister.AkiSisterCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace AkiSister.AkiSisterCode.Cards.UncommonCards;

public class Prototype() : AkiSisterCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    public override bool GainsBlock => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        //new DamageVar(6, ValueProp.Move),
        new BlockVar(6, ValueProp.Move),
        new PowerVar<WeakPower>(2)
    ];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust,
        CardKeyword.Innate,
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        //await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(play.Target)
        //    .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
        //    .Execute(choiceContext);
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        await PowerCmd.Apply<WeakPower>(choiceContext, play.Target, DynamicVars.Weak.BaseValue, Owner.Creature, this);
        //var card = PileType.Draw.GetPile(base.Owner).Cards.Where((CardModel c) => c.Type == CardType.Status).ToList()
        //    //CardPile.GetCards(Owner, PileType.Deck).Where(card => card.Type == CardType.Status).ToList()
        //    .StableShuffle(Owner.RunState.Rng.Shuffle).FirstOrDefault();
        //if (card != null)
        //    await CardCmd.Exhaust(choiceContext, card);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(1);
        DynamicVars.Weak.UpgradeValueBy(1);
    }
}