using AkiSister.AkiSisterCode.Cards.CommonCards;
using AkiSister.AkiSisterCode.Extensions;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2RitsuLib.Combat.Powers;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;

namespace AkiSister.AkiSisterCode.Powers;

[RegisterPower]
public class FragranceSurroundsPower : ModTemporaryPowerTemplate
{
    public override PowerAssetProfile AssetProfile => new(
        IconPath: $"{Entry.ResPath}/images/powers/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png",
        BigIconPath: $"{Entry.ResPath}/images/powers/big/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png"
    );
    
    public override PowerModel InternallyAppliedPower => ModelDb.Power<StrengthPower>();
    
    public override AbstractModel OriginModel => ModelDb.Card<FragranceSurrounds>();

    protected override bool IsPositive => false;
}