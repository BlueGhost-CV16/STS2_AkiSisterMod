using AkiSister.Characters;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Interop.AutoRegistration;
using AkiSister.Characters;

namespace AkiSister.AkiSisterCode.Cards.UncommonCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class PeakEnsemble() : AkiSisterCard(2,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(17, ValueProp.Move)
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<WitherPower>(),
        HoverTipFactory.FromPower<DrainPower>(),
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        AttackCommand attackCommand = await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Execute(choiceContext);
        if (this.LeafCheck())
        {
            await PowerCmd.Apply<WitherPower>(choiceContext, play.Target, attackCommand.Results.SelectMany(r => r).Sum(r => r.TotalDamage), Owner.Creature, this);
        }

        if (this.PotatoCheck())
        {
            await PowerCmd.Apply<DrainPower>(choiceContext, play.Target, attackCommand.Results.SelectMany(r => r).Sum(r => r.TotalDamage), Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(4);
    }
}