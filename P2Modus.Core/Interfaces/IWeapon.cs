using System.Collections.Generic;

namespace P2Modus.Core
{
    public interface IWeapon : IEquipment
    {
        bool IsTwoHanded { get; set; }
        
        IDie DamageDie { get; set; }

        IDie TwoHandedDamageDie { get; set; }
        
        IEnumerable<ITrait> DamageType { get; set; }

        AttackType AttackType { get; set; }
        string AttackBonusAttribute { get; set; }

        string DamageBonusAttribute { get; set; }

    }
}