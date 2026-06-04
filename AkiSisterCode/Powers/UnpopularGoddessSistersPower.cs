using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;

namespace AkiSister.AkiSisterCode.Powers;


public class UnpopularGoddessSistersPower : AkiSisterPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => HoverTipFactory.FromEnchantment<RedLeafMarkEnchantment>().Concat(HoverTipFactory.FromEnchantment<SweetPotatoMarkEnchantment>());

    //public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    //{
    //    if (cardPlay.Card.Owner == base.Owner.Player && (cardPlay.Card.LeafCheck() || cardPlay.Card.PotatoCheck()))
    //    {
    //        foreach (var enemy in base.CombatState.HittableEnemies)
    //        {
    //            await PowerCmd.Apply<WitherPower>(enemy, Amount, base.Owner, null);
    //            await PowerCmd.Apply<DrainPower>(enemy, Amount, base.Owner, null);
    //        }
    //    }
    //}

    public override async Task AfterCardPlayedLate(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner == base.Owner.Player && (cardPlay.Card.LeafCheck() || cardPlay.Card.PotatoCheck()))
        {
            foreach (var enemy in base.CombatState.HittableEnemies)
            {
                await PowerCmd.Apply<WitherPower>(choiceContext, enemy, Amount, base.Owner, null);
                await PowerCmd.Apply<DrainPower>(choiceContext, enemy, Amount, base.Owner, null);
            }
        }
    }
}