using BaseLib.Abstracts;
using BaseLib.Utils;
using AkiSister.AkiSisterCode.Character;

namespace AkiSister.AkiSisterCode.Potions;

[Pool(typeof(AkiSisterPotionPool))]
public abstract class AkiSisterPotion : CustomPotionModel;