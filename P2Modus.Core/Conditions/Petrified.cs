using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Petrified condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=30
    //
    // This condition must be manually applied during gameplay.
    public class Petrified : ConditionBase
    {
        public Petrified() 
            : base(DefaultName, DefaultDescription, EntityIds.PETRIFIED_CONDITION_ID)
        {
        }

        public Petrified(string name, string description)
            :base(name, description, EntityIds.PETRIFIED_CONDITION_ID)
        {
        }

        protected override void SetupAppliesToList()
        {
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.PETRIFIED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.PETRIFIED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Petrified"; }

        private static string DefaultDescription 
        { 
            get => "You have been turned to stone. You can’t act, nor can you sense anything. You become an object with a Bulk " +
            "double your normal Bulk (typically 12 for a petrified Medium creature or 6 for a petrified Small creature), AC 9, " +
            "Hardness 8, and the same current Hit Points you had when alive. You don’t have a Broken Threshold. When you’re " +
            "turned back into flesh, you have the same number of Hit Points you had as a statue. If the statue is destroyed, you " +
            "immediately die. While petrified, your mind and body are in stasis, so you don’t age or notice the passing of time."; 
        }
    }    
}