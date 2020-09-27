using System.Collections.Generic;
using System.Linq;

namespace P2Modus.Core
{
    public static class EntityIds
    {
        public const int CHARACTER_ID = 1;
        
        #region Traits

        private const int TRAIT_BASE_ID = 1000;

        public const int VISUAL_TRAIT_ID = TRAIT_BASE_ID + 1;

        #endregion

        #region Conditions

        private const int CONDITION_BASE_ID = 2000;

        public const int BLINDED_CONDITION_ID = CONDITION_BASE_ID + 1;
        public const int BROKEN_CONDITION_ID = BLINDED_CONDITION_ID + 1;
        public const int CLUMSY_CONDITION_ID = BROKEN_CONDITION_ID + 1;
        public const int CONCEALED_CONDITION_ID = CLUMSY_CONDITION_ID + 1;
        public const int CONFUSED_CONDITION_ID = CONCEALED_CONDITION_ID + 1;
        public const int FLAT_FOOTED_CONDITION_ID = CONFUSED_CONDITION_ID + 1;
        public const int CONTROLLED_CONDITION_ID = FLAT_FOOTED_CONDITION_ID + 1;
        public const int DAZZLED_CONDITION_ID = CONTROLLED_CONDITION_ID + 1;
        public const int DEAFENED_CONDITION_ID = DAZZLED_CONDITION_ID + 1;
        public const int DOOMED_CONDITION_ID = DEAFENED_CONDITION_ID + 1;
        public const int DRAINED_CONDITION_ID = DOOMED_CONDITION_ID + 1;
        public const int DYING_CONDITION_ID = DRAINED_CONDITION_ID + 1;
        public const int UNCONSCIOUS_CONDITION_ID = DYING_CONDITION_ID + 1;
        public const int ENCUMBERED_CONDITION_ID = UNCONSCIOUS_CONDITION_ID + 1;
        public const int ENFEEBLED_CONDITION_ID = ENCUMBERED_CONDITION_ID + 1;
        public const int FASCINATED_CONDITION_ID = ENFEEBLED_CONDITION_ID + 1;
        public const int FATIGUED_CONDITION_ID = FASCINATED_CONDITION_ID + 1;
        public const int FLEEING_CONDITION_ID = FATIGUED_CONDITION_ID + 1;
        public const int FRIENDLY_CONDITION_ID = FLEEING_CONDITION_ID + 1;
        public const int FRIGHTENED_CONDITION_ID = FRIENDLY_CONDITION_ID + 1;
        public const int IMMOBILIZED_CONDITION_ID = FRIGHTENED_CONDITION_ID + 1;
        public const int GRABBED_CONDITION_ID = IMMOBILIZED_CONDITION_ID + 1;
        public const int HELPFUL_CONDITION_ID = GRABBED_CONDITION_ID + 1;
        public const int HIDDEN_CONDITION_ID = HELPFUL_CONDITION_ID + 1;
        public const int HOSTILE_CONDITION_ID = HIDDEN_CONDITION_ID + 1;
        public const int INDIFFERENT_CONDITION_ID = HOSTILE_CONDITION_ID + 1;
        public const int INVISIBLE_CONDITION_ID = INDIFFERENT_CONDITION_ID + 1;
        public const int OBSERVED_CONDITION_ID = INVISIBLE_CONDITION_ID + 1;
        public const int PERSISTENT_DAMAGE_CONDITION_ID = OBSERVED_CONDITION_ID + 1;
        public const int PETRIFIED_CONDITION_ID = PERSISTENT_DAMAGE_CONDITION_ID + 1;
        public const int PRONE_CONDITION_ID = PETRIFIED_CONDITION_ID + 1;
        public const int QUICKENED_CONDITION_ID = PRONE_CONDITION_ID + 1;
        public const int RESTRAINED_CONDITION_ID = QUICKENED_CONDITION_ID + 1;
        public const int SICKENED_CONDITION_ID = RESTRAINED_CONDITION_ID + 1;
        public const int SLOWED_CONDITION_ID = SICKENED_CONDITION_ID + 1;
        public const int STUNNED_CONDITION_ID = SLOWED_CONDITION_ID + 1;
        public const int STUPEFIED_CONDITION_ID = STUNNED_CONDITION_ID + 1;
        public const int UNDETECTED_CONDITION_ID = STUPEFIED_CONDITION_ID + 1;
        public const int UNFRIENDLY_CONDITION_ID = UNDETECTED_CONDITION_ID + 1;
        public const int UNNOTICED_CONDITION_ID = UNFRIENDLY_CONDITION_ID + 1;
        public const int WOUNDED_CONDITION_ID = UNNOTICED_CONDITION_ID + 1;

        #endregion

        #region Senses

        private const int SENSE_BASE_ID = 3000;

        public const int VISION_SENSE_ID = SENSE_BASE_ID + 1;

        #endregion

        #region Immunities

        private const int IMMUNITY_ID = 4000;
        public const int BLIND_IMMUNITY_ID = IMMUNITY_ID + 1;

        #endregion

        #region Skills, DCs, and Perception

        private const int SDP_BASE_ID = 5000;

        public const int ALL_DCS = SDP_BASE_ID + 1;

        public const int ALL_CHECKS = ALL_DCS + 1;

        public const int PERCEPTION_ID = ALL_CHECKS + 1;

        public const int FORT_SAVE_ID = PERCEPTION_ID + 1;

        public const int REFLEX_SAVE_ID = FORT_SAVE_ID + 1;

        public const int WILL_SAVE_ID = REFLEX_SAVE_ID + 1;

        public const int AC_ID = WILL_SAVE_ID + 1;

        public const int SKILL_ACROBATICS_ID = AC_ID + 1;

        public const int SKILL_ARCANA_ID = SKILL_ACROBATICS_ID + 1;

        public const int SKILL_ATHLETICS_ID = SKILL_ARCANA_ID + 1;

        public const int SKILL_CRAFTING_ID = SKILL_ATHLETICS_ID + 1;

        public const int SKILL_DECEPTION_ID = SKILL_CRAFTING_ID + 1;

        public const int SKILL_DIPLOMACY_ID = SKILL_DECEPTION_ID + 1;

        public const int SKILL_INTIMIDATION_ID = SKILL_DIPLOMACY_ID + 1;

        public const int SKILL_LORE_ID = SKILL_INTIMIDATION_ID + 1;

        public const int SKILL_MEDICINE_ID = SKILL_LORE_ID + 1;

        public const int SKILL_NATURE_ID = SKILL_MEDICINE_ID + 1;

        public const int SKILL_OCCULTIMS_ID = SKILL_NATURE_ID + 1;

        public const int SKILL_PERFORMANCE_ID = SKILL_OCCULTIMS_ID + 1;

        public const int SKILL_RELIGION_ID = SKILL_PERFORMANCE_ID + 1;

        public const int SKILL_SOCIETY_ID = SKILL_RELIGION_ID + 1;

        public const int SKILL_STEALTH_ID = SKILL_SOCIETY_ID + 1;

        public const int SKILL_SURVIVAL_ID = SKILL_STEALTH_ID + 1;

        public const int SKILL_THIEVERY_ID = SKILL_SURVIVAL_ID + 1;

        public const int ATTRIBUTE_BASED_CHECK_STRENGTH = SKILL_THIEVERY_ID + 1;
        
        public const int ATTRIBUTE_BASED_CHECK_DEXTERITY = ATTRIBUTE_BASED_CHECK_STRENGTH + 1;
        
        public const int ATTRIBUTE_BASED_CHECK_CONSTITUTION = ATTRIBUTE_BASED_CHECK_DEXTERITY + 1;
        
        public const int ATTRIBUTE_BASED_CHECK_INTELLIGENCE = ATTRIBUTE_BASED_CHECK_CONSTITUTION + 1;
        
        public const int ATTRIBUTE_BASED_CHECK_WISDOM = ATTRIBUTE_BASED_CHECK_INTELLIGENCE + 1;
        
        public const int ATTRIBUTE_BASED_CHECK_CHARISMA = ATTRIBUTE_BASED_CHECK_WISDOM + 1;

        public const int PERCEPTION_BASED_INITIATIVE_ID = ATTRIBUTE_BASED_CHECK_CHARISMA + 1;

        public const int MAX_HP_ID = PERCEPTION_BASED_INITIATIVE_ID + 1;        

        #endregion

        #region Movement

        public const int MOVEMENT_ID = 6000;
        
        public const int ALL_TERRAIN_DIFFICULT_ID = MOVEMENT_ID + 1;

        #endregion

        #region Attacks

        public const int ALL_ATTACKS_ID = 7000;
        
        public const int MELEE_ATTACKS_ID = ALL_ATTACKS_ID + 1;

        public const int RANGED_ATTACKS_ID = MELEE_ATTACKS_ID + 1;

        #endregion

        #region ItemCategories

        public const int ARMOR_ID = 8000;

        #endregion

        #region Entity ID related extension methods

        public static bool HasConditionId(this IList<Conditions.ICondition> conditions, int id)
        {
            return conditions.Any(c => c.Id == id);
        }

        public static Conditions.ICondition GetById(this IList<Conditions.ICondition> conditions, int id)
        {
            return conditions.FirstOrDefault(c => c.Id == id);
        }

        public static T GetById<T>(this IList<Conditions.ICondition> conditions, int id)
        {
            return (T)conditions.GetById(id);
        }

        #endregion
    }
}