using AkiSister.Characters;
using AkiSister.AkiSisterCode.Cards.AncientCards;
using AkiSister.AkiSisterCode.Extensions;

using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using STS2RitsuLib.Scaffolding.Content;

namespace AkiSister.AkiSisterCode.Cards.FakeCards;
[RegisterCard(typeof(TokenCardPool))]
public class AkiSisterFakeTransformedCard() : ModCardTemplate(1,
    CardType.Skill, CardRarity.Ancient,
    TargetType.Self)
{
    private const bool ShowInCardLibrary = false;
    public override CardAssetProfile AssetProfile => new(
        PortraitPath: $"{Entry.ResPath}/images/cards/big/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png");
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromCard<RedRainofFallenLeaves>(),
        HoverTipFactory.FromCard<GoldenBreezeofAbundance>()
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