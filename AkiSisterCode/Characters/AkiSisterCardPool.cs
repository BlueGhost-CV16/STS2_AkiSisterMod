using Godot;
using STS2RitsuLib.Scaffolding.Content;
using STS2RitsuLib.Utils;

namespace AkiSister.Characters;

public sealed class AkiSisterCardPool : TypeListCardPoolModel
{
    private static readonly Material? PoolFrameTintMaterial =
          MaterialUtils.CreateHsvShaderMaterial(0.13f, 0.6f, 1.7f);
    //    MaterialUtils.CreateRgbShaderMaterial(0.13f, 0.6f, 1.7f);

    //private static readonly Material? PoolFrameTintMaterial =
    //    MaterialUtils.CreateUnmodulatedHsvShaderMaterial();

    // Title 和 EnergyColorName 是池子的稳定标识，不是玩家看到的角色名。
    // 自定义角色卡、遗物、药水池保持同一个 EnergyColorName，方便实验室和文本统一读取能量图标。
    public override string Title => "AkiSister";
    public override string EnergyColorName => "AkiSister";

    // 这里指定卡牌文本和大图使用的能量图标路径。
    // res://AkiSister/... 里的 AkiSister 是 PCK 资源目录，不是 C# namespace。
    public override string? BigEnergyIconPath => $"{Entry.ResPath}/images/characters/energy_big.png";
    public override string? TextEnergyIconPath => $"{Entry.ResPath}/images/characters/energy_text.png";

    public override Color DeckEntryCardColor => AkiSisterCharacter.ThemeColor;
    public override Color EnergyOutlineColor => new(0.08f, 0.18f, 0.24f);
    public override Material? PoolFrameMaterial => PoolFrameTintMaterial;

    // false 表示这是角色专属卡池，不是事件/状态那类无色卡池。
    public override bool IsColorless => false;
    
    // 如果你想用原版卡框换色，加这两行
    // private static readonly Material? _poolFrameMaterial = MaterialUtils.CreateRgbShaderMaterial(0.5f, 0.5f, 1f);
    // 如果你是自定义卡框，上面一行换成这个
    // private static readonly Material? _poolFrameMaterial = MaterialUtils.CreateUnmodulatedHsvShaderMaterial();
    // public override Material? PoolFrameMaterial => _poolFrameMaterial;
}