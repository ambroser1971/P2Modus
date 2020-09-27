using System.Collections.Generic;
using P2Modus.Core.Conditions;

namespace P2Modus.Core
{
    public interface ICharacter : IEntity
    {
        int Level { get; set; }

        int Experience { get; set; }

        int HeroPoints { get; set; }

        string Class { get; set; }

        int ClassDC { get; set; }

        string  Ancestry { get; set; }

        string Heritage { get; set; }

        string BackGround { get; set; }

        IEnumerable<ITrait> Traits { get; set; }

        IEnumerable<string> Languages { get; set; }

        IEnumerable<ISpeed> Speed { get; set; }

        IAbilityScore Strength { get; set; }

        IAbilityScore Dexterity { get; set; }

        IAbilityScore Constitution { get; set; }

        IAbilityScore Intelligence { get; set; }

        IAbilityScore Wisdom { get; set; }

        IAbilityScore Charisma { get; set; }

        IList<ICondition> Conditions { get; set; }

        IList<ISense> Senses { get; set; }

        IList<IImmunity> Immunities {get; set; }

        IList<IResistance> Resistances { get; set; }
        int Perception { get; set; }

        IArmor WornArmor { get; set; }

        IHitPoint HitPoints { get; set; }

        IList<ISkill> Skills { get; set; }

        IAbilityScore GetAbilityScoreById(int id);

    }
}