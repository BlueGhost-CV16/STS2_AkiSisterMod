using AkiSister.AkiSisterCode.Cards.CommonCards;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace AkiSister.AkiSisterCode.Powers;
//[RegisterPower]

public class FragranceSurroundsPower : TemporaryStrengthPower
{
    public override AbstractModel OriginModel => ModelDb.Card<FragranceSurrounds>();

    protected override bool IsPositive => false;
}