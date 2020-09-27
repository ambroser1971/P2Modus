using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Fascinated condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=14
    //
    // The following is automated:
    //      * -2 status penalty to Perception and all skill checks.
    //
    // The following is not automated and must be implemented manually by the GM in play:
    //      * Can't use actions with the concentrate trait except for target of fasination.
    //      * Condition removed if a crature uses a hostile action against you or your allies.
    public class Fascinated : ConditionBase
    {
        public Fascinated() 
            : base(DefaultName, DefaultDescription, EntityIds.FASCINATED_CONDITION_ID)
        {
        }

        public Fascinated(string name, string description)
            :base(name, description, EntityIds.FASCINATED_CONDITION_ID)
        {
        }

        protected override void SetupAppliesToList()
        {
            Modifier = new Modifier() { Type = ModifierType.Status, Value = -2 };
            _appliesToList.Add(EntityIds.PERCEPTION_ID);
            _appliesToList.Add(EntityIds.SKILL_ACROBATICS_ID);
            _appliesToList.Add(EntityIds.SKILL_ARCANA_ID);
            _appliesToList.Add(EntityIds.SKILL_ATHLETICS_ID);
            _appliesToList.Add(EntityIds.SKILL_CRAFTING_ID);
            _appliesToList.Add(EntityIds.SKILL_DECEPTION_ID);
            _appliesToList.Add(EntityIds.SKILL_DIPLOMACY_ID);
            _appliesToList.Add(EntityIds.SKILL_INTIMIDATION_ID);
            _appliesToList.Add(EntityIds.SKILL_LORE_ID);
            _appliesToList.Add(EntityIds.SKILL_MEDICINE_ID);
            _appliesToList.Add(EntityIds.SKILL_NATURE_ID);
            _appliesToList.Add(EntityIds.SKILL_OCCULTIMS_ID);
            _appliesToList.Add(EntityIds.SKILL_PERFORMANCE_ID);
            _appliesToList.Add(EntityIds.SKILL_RELIGION_ID);
            _appliesToList.Add(EntityIds.SKILL_SOCIETY_ID);
            _appliesToList.Add(EntityIds.SKILL_STEALTH_ID);
            _appliesToList.Add(EntityIds.SKILL_SURVIVAL_ID);
            _appliesToList.Add(EntityIds.SKILL_THIEVERY_ID);
        }

        protected override bool CustomApply(IEntity entity)
        {
            if(entity is ICharacter character)
            {
                if(!character.Conditions.Any(c => c.Id == Id))
                {                    
                    character.Conditions.Add(this);
                    return true;
                }
                throw new ConditionExistsException(this, character.Conditions.First(c => c.Id == Id));
            }
            return false;
        }

        protected override bool CustomRemove(IEntity entity)
        {
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.FASCINATED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.FASCINATED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Fascinated"; }

        private static string DefaultDescription 
        { 
            get => "You are compelled to focus your attention on something, distracting you from whatever else is going on " +
                   "around you. You take a –2 status penalty to Perception and skill checks, and you can’t use actions with " +
                   "the concentrate trait unless they or their intended consequences are related to the subject of your " +
                   "fascination (as determined by the GM). For instance, you might be able to Seek and Recall Knowledge " +
                   "about the subject, but you likely couldn’t cast a spell targeting a different creature. This condition " +
                   "ends if a creature uses hostile actions against you or any of your allies."; 
        }
    }    
}