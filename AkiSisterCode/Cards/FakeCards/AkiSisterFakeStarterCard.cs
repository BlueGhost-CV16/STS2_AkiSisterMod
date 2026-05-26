using AkiSister.AkiSisterCode.Cards.BasicCards;
using AkiSister.AkiSisterCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using STS2RitsuLib.Scaffolding.Content;

namespace AkiSister.AkiSisterCode.Cards.FakeCards;
[RegisterCard(typeof(TokenCardPool))]
public class AkiSisterFakeStarterCard() : ModCardTemplate(1,
    CardType.Skill, CardRarity.None,
    TargetType.Self)
{
    private const bool ShowInCardLibrary = false;
    public override CardAssetProfile AssetProfile => new(
        PortraitPath: $"{Entry.ResPath}/images/cards/big/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png");
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromCard<GlowofAutumnSunset>(),
        HoverTipFactory.FromCard<ResentmentofAutumnColors>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        
    }

    protected override void OnUpgrade()
    {

    }
}