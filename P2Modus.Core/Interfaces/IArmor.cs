using System.Collections.Generic;

namespace P2Modus.Core
{
    public interface IArmor : IEquipment
    {
        ArmorCategory Category { get; set; }

        int DexterityCap { get; set; }

        int CheckPenalty { get; set; }

        int SpeedPenalty { get; set; }

        int Strength { get; set; }

        ArmorGroup Group { get; set; }

    }
}