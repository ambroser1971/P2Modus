using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Prone condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=31
    //
    // The following is automated:
    //      * -2 to circumstance penalty to attack rolls.
    //      * Gain the flat-footed condition.
    //
    // The following must be manually enforced by the GM:
    //      * Only move actions available are Crawl and Stand
    //      * Whe taking Cover you gain a +4 circumstance bonus to AC (but remain flat-footed).
    public class Prone : ConditionBase
    {
        private bool _appliedFlatFooted;

        public Prone() 
            : base(DefaultName, DefaultDescription, EntityIds.PRONE_CONDITION_ID)
        {
        }

        public Prone(string name, string description)
            :base(name, description, EntityIds.PRONE_CONDITION_ID)
        {
        }

        protected override void SetupAppliesToList()
        {
            _appliesToList.Add(EntityIds.ALL_ATTACKS_ID);
            Modifier = new Modifier() { Type = ModifierType.Circumstance, Value = -2 };
        }

        protected override bool CustomApply(IEntity entity)
        {
            if(entity is ICharacter character)
            {
                if(!character.Conditions.Any(c => c.Id == Id))
                {                    
                    character.Conditions.Add(this);
                    var flatFooted = (FlatFooted)character.Conditions.FirstOrDefault(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID);
                    if(flatFooted == null)
                    {
                        flatFooted = new FlatFooted(FlatFooted.AllCharacters);
                        character.Conditions.Add(flatFooted);
                        _appliedFlatFooted = true;
                    }
                    else if(!flatFooted.FlatFootedFrom.Contains(FlatFooted.AllCharacters))
                    {
                        character.Conditions.Remove(flatFooted);
                        flatFooted = new FlatFooted(FlatFooted.AllCharacters);
                        character.Conditions.Add(flatFooted);
                        _appliedFlatFooted = true;
                    }
                    
                    return true;
                }
                throw new ConditionExistsException(this, character.Conditions.First(c => c.Id == Id));
            }
            return false;
        }

        protected override bool CustomRemove(IEntity entity)
        {
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.PRONE_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.PRONE_CONDITION_ID); 
                character.Conditions.Remove(condition);
                if(_appliedFlatFooted)
                {
                    var flatFooted = character.Conditions.FirstOrDefault(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID);
                    if(flatFooted != null)
                    {
                        character.Conditions.Remove(flatFooted);
                    }
                }
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Prone"; }

        private static string DefaultDescription 
        { 
            get => "You’re lying on the ground. You are flat-footed and take a –2 circumstance penalty to attack rolls. The " +
                   "only move actions you can use while you’re prone are Crawl and Stand. Standing up ends the prone " +
                   "condition. You can Take Cover while prone to hunker down and gain cover against ranged attacks, even if " +
                   "you don’t have an object to get behind, gaining a +4 circumstance bonus to AC against ranged attacks " +
                   "(but you remain flat-footed).\n\n" +
                   "If you would be knocked prone while you’re Climbing or Flying, you fall (see pages 463–464 for the rules " +
                   "on falling). You can’t be knocked prone when Swimming."; 
        }
    }    
}