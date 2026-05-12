using AkiSister.AkiSisterCode.Cards;
using AkiSister.AkiSisterCode.Cards.BasicCards;
using BaseLib.Abstracts;
using AkiSister.AkiSisterCode.Extensions;
using AkiSister.AkiSisterCode.Relics;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;

namespace AkiSister.AkiSisterCode.Character;

public class AkiSister : PlaceholderCharacterModel
{
    public const string CharacterId = "AkiSister";

    public static readonly Color Color = new("F5DEB3");

    public override Color MapDrawingColor => Color;
    public override Color RemoteTargetingLineColor => Color;
    public override Color RemoteTargetingLineOutline => Color; 
    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Feminine;
    public override int StartingHp => 66;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeAkiSister>(),
        ModelDb.Card<StrikeAkiSister>(),
        ModelDb.Card<StrikeAkiSister>(),
        ModelDb.Card<StrikeAkiSister>(),
        ModelDb.Card<DefendAkiSister>(),
        ModelDb.Card<DefendAkiSister>(),
        ModelDb.Card<DefendAkiSister>(),
        ModelDb.Card<DefendAkiSister>(),
        ModelDb.Card<GlowofAutumnSunset>(),
        ModelDb.Card<ResentmentofAutumnColors>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<AkiSizuhaGrasses>(),
        ModelDb.Relic<AkiMinorikoGrasses>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<AkiSisterCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<AkiSisterRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<AkiSisterPotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name_new.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();

    public override string CustomArmPointingTexturePath => "hand_point.png".CharacterUiPath();
    public override string CustomArmRockTexturePath => "hand_rock.png".CharacterUiPath();
    public override string CustomArmPaperTexturePath => "hand_paper.png".CharacterUiPath();
    public override string CustomArmScissorsTexturePath => "hand_scissors.png".CharacterUiPath();
    
    public override string CustomRestSiteAnimPath => "res://AkiSister/images/scenes/aki_sister_rest_site_new.tscn";
    public override string CustomMerchantAnimPath => "res://AkiSister/images/scenes/aki_sister_merchant.tscn";
    public override string CustomEnergyCounterPath => "res://AkiSister/images/scenes/aki_sister_energy_counter.tscn";
    public override string CustomIconPath => "res://AkiSister/images/scenes/aki_sister_icon.tscn";
    //public override string CustomVisualPath => "scenes/aki_sister.tscn".ImagePath();
    public override string CustomVisualPath => "res://AkiSister/images/scenes/aki_sister.tscn";
    public override string CustomCharacterSelectBg => "res://AkiSister/images/scenes/aki_sister_bg.tscn";
    public override string CharacterTransitionSfx => "event:/sfx/ui/wipe_ironclad";
    
    // 攻击建筑师的攻击特效列表
    public override List<string> GetArchitectAttackVfx() => [
        "vfx/vfx_attack_blunt",
        "vfx/vfx_heavy_blunt",
        "vfx/vfx_attack_slash",
        "vfx/vfx_bloody_impact",
        "vfx/vfx_rock_shatter"
    ];
}