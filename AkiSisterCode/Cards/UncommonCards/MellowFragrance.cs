using AkiSister.Characters;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AkiSister.AkiSisterCode.Cards.UncommonCards;
[RegisterCard(typeof(AkiSisterCardPool))]

public class MellowFragrance() : AkiSisterCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.Static(StaticHoverTip.Block),
        HoverTipFactory.FromPower<FragrancePower>(),
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int block = Owner.Creature.Block;
        await CreatureCmd.LoseBlock(Owner.Creature, block);
        await PowerCmd.Apply<FragrancePower>(choiceContext, Owner.Creature, block, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        this.PotatoAdd_Card();
    }
}