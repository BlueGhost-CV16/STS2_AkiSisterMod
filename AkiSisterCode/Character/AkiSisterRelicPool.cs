using BaseLib.Abstracts;
using AkiSister.AkiSisterCode.Extensions;
using Godot;

namespace AkiSister.AkiSisterCode.Character;

public class AkiSisterRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => AkiSister.Color;

    public override string BigEnergyIconPath => "charui/cardEnergyAkiSizuha.png".ImagePath();
    public override string TextEnergyIconPath => "charui/cardEnergyAkiSizuha_small.png".ImagePath();
}