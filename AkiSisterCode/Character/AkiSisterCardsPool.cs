using BaseLib.Abstracts;
using AkiSister.AkiSisterCode.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace AkiSister.AkiSisterCode.Character;

public class AkiSisterCardPool : CustomCardPoolModel
{
    private const string FramePathAttack = "res://AkiSister/images/charui/bg_attack_aki_sister.png";
    private const string FramePathPower = "res://AkiSister/images/charui/bg_power_aki_sister.png";
    private const string FramePathSkill = "res://AkiSister/images/charui/bg_skill_aki_sister.png";
    
    public override string Title => AkiSister.CharacterId; //This is not a display name.

    public override string BigEnergyIconPath => "charui/cardEnergyAkiSizuha.png".ImagePath();
    public override string TextEnergyIconPath => "charui/cardEnergyAkiSizuha_small.png".ImagePath();

    //Color of small card icons
    public override Color DeckEntryCardColor => AkiSister.Color;
    
    //public override Color ShaderColor => new(0.132f, 0.74f, 1.46f);
    
    public override Color EnergyOutlineColor => new("BE2624");

    public override bool IsColorless => false;

    //Alternatively, leave these values at 1 and provide a custom frame image.
    /*public override Texture2D CustomFrame(CustomCardModel card)
    {
        //This will attempt to load AkiSister/images/cards/frame.png
        return PreloadManager.Cache.GetTexture2D("cards/frame.png".ImagePath());
    }*/
    public override float H => 1f;
    public override float S => 1f;
    public override float V => 1f;

    public override Texture2D CustomFrame(CustomCardModel card)
    {
        var path = card.Type switch
        {
            CardType.Attack => FramePathAttack,
            CardType.Power => FramePathPower,
            _ => FramePathSkill
        };
        return //ResourceLoader.Load<Texture2D>(path); 
            PreloadManager.Cache.GetTexture2D(path);
    }
}