using AkiSister.Characters;
using AkiSister.AkiSisterCode.Enchantments;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Nodes;
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
using STS2RitsuLib.Keywords;

namespace AkiSister.AkiSisterCode.Cards.CommonCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class Labor() : AkiSisterCard(2,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override bool ShouldGlowGoldInternal => this.LeafCheck();
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(10, ValueProp.Move),
        new RepeatVar(2),
        new EnergyVar(2)
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        base.EnergyHoverTip,
        ModKeywordRegistry.CreateHoverTip(AkiSisterCardKeyWords.RedLeafResonance)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).WithHitCount(base.DynamicVars.Repeat.IntValue).FromCard(this).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_slash", null, "slash_attack.mp3")
            .Execute(choiceContext);
        //await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(play.Target)
        //    .Execute(choiceContext);
        if (this.LeafCheck())
        {
            await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
        DynamicVars.Energy.UpgradeValueBy(1);
    }
}