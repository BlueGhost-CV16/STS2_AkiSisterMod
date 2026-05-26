using AkiSister.AkiSisterCode.Relics;
using AkiSister.Characters;
using MegaCrit.Sts2.Core.Entities.Relics;
using STS2RitsuLib.Interop.AutoRegistration;

namespace AkiSister.AkiSisterCode.Relics;
[RegisterRelic(typeof(AkiSisterRelicPool))]

public class AkiSisterGrasses() : AkiSisterRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.None;
}