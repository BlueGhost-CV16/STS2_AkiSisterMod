using BaseLib.Abstracts;
using AkiSister.AkiSisterCode.Extensions;
using Godot;

namespace AkiSister.AkiSisterCode.Character;

public class AkiSisterCardPool : CustomCardPoolModel
{
    public override string Title => AkiSister.CharacterId; //This is not a display name.

    public override string BigEnergyIconPath => "charui/cardEnergyAkiSizuha.png".ImagePath();
    public override string TextEnergyIconPath => "charui/cardEnergyAkiSizuha_small.png".ImagePath();


    //Color of small card icons
    public override Color DeckEntryCardColor => AkiSister.Color;
    
    public override Color ShaderColor => new("F5DEB3");
    
    public override Color EnergyOutlineColor => new("BE2624");

    /* These HSV values will determine the color of your card back.
    They are applied as a shader onto an already colored image,
    so it may take some experimentation to find a color you like.
    Generally they should be values between 0 and 1. */
    //public override float H => 0f; //Hue; changes the color.
    //public override float S => 0.8f; //Saturation
    //public override float V => 0.7f; //Brightness
    //public override float H => 0.13f; //Hue; changes the color.
    //public override float S => 1f; //Saturation
    //public override float V => 1f; //Brightness

    //Alternatively, leave these values at 1 and provide a custom frame image.
    /*public override Texture2D CustomFrame(CustomCardModel card)
    {
        //This will attempt to load AkiSister/images/cards/frame.png
        return PreloadManager.Cache.GetTexture2D("cards/frame.png".ImagePath());
    }*/

    public override bool IsColorless => false;
}