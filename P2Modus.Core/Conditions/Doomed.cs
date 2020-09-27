using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Doomed condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=9
    //
    // This condition must be manually applied during gameplay.
    public class Doomed : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.DeathAndDying; }

        public Doomed() 
            : base(DefaultName, DefaultDescription, EntityIds.DOOMED_CONDITION_ID)
        {
        }

        public Doomed(string name, string description)
            :base(name, description, EntityIds.DOOMED_CONDITION_ID)
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.DOOMED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.DOOMED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Doomed"; }

        private static string DefaultDescription 
        { 
            get => "A powerful force has gripped your soul, calling you closer to death. Doomed always includes a value. " +
                   "The dying value at which you die is reduced by your doomed value. If your maximum dying value is " +
                   "reduced to 0, you instantly die. When you die, you’re no longer doomed.\n\n" + 
                   "Your doomed value decreases by 1 each time you get a full night’s rest."; 
        }
    }    
}