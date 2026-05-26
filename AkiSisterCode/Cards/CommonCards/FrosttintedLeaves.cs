using AkiSister.Characters;
using AkiSister.AkiSisterCode.Cards.StatusCards;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Nodes;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Interop.AutoRegistration;
using AkiSister.Characters;

namespace AkiSister.AkiSisterCode.Cards.CommonCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class FrosttintedLeaves() : AkiSisterCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CardsVar(1),
        new PowerVar<WitherPower>(3),
        new DamageVar(6m, ValueProp.Move)
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => HoverTipFactory.FromEnchantment<RedLeafMarkEnchantment>();
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Execute(choiceContext);
        await PowerCmd.Apply<WitherPower>(choiceContext, play.Target, DynamicVars["WitherPower"].BaseValue, Owner.Creature, this);
        await base.Owner.LeafAdd_Hand((int)base.DynamicVars.Cards.BaseValue);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.UpgradeValueBy(1);
        DynamicVars.Damage.UpgradeValueBy(2);
        DynamicVars["WitherPower"].UpgradeValueBy(1);
    }
}