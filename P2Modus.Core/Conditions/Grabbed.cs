using System.Collections.Generic;
using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Grabbed condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=20
    //
    // The following is automated:
    //      * Addition of the flat-footed and immobilized conditions.
    //      * Remove of the flat footed and immobilized conditions added by Apply.
    //
    // The following must be implemented manually by the GM:
    //      * Make DC5 flat check to not lose an action with the manipulate trait.
    public class Grabbed : ConditionBase
    {
        private List<ICondition> _appliedConditions = new List<ICondition>();

        public Grabbed() 
            : base(DefaultName, DefaultDescription, EntityIds.GRABBED_CONDITION_ID)
        {
        }

        public Grabbed(string name, string description)
            :base(name, description, EntityIds.GRABBED_CONDITION_ID)
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
                    var flatFooted = (FlatFooted)character.Conditions.FirstOrDefault(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID);
                    if(flatFooted == null)
                    {
                        flatFooted = new FlatFooted(FlatFooted.AllCharacters);
                        _appliedConditions.Add(flatFooted);
                        character.Conditions.Add(flatFooted);
                    }
                    else if(!flatFooted.FlatFootedFrom.Contains(FlatFooted.AllCharacters))
                    {
                        flatFooted = new FlatFooted(flatFooted, false, FlatFooted.AllCharacters);
                    }

                    var immobilized = character.Conditions.FirstOrDefault(c => c.Id == EntityIds.IMMOBILIZED_CONDITION_ID);
                    if(immobilized == null)
                    {
                        immobilized = new Immobilized();
                        _appliedConditions.Add(immobilized);
                        character.Conditions.Add(immobilized);
                    }

                    character.Conditions.Add(this);
                    return true;
                }
                throw new ConditionExistsException(this, character.Conditions.First(c => c.Id == Id));
            }
            return false;
        }

        protected override bool CustomRemove(IEntity entity)
        {
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.GRABBED_CONDITION_ID))
            {
                foreach(var subCondition in _appliedConditions)
                {
                    if(character.Conditions.Contains(subCondition))
                    {
                        character.Conditions.Remove(subCondition);
                    }
                }
                _appliedConditions.Clear();
                var condition = character.Conditions.First(c => c.Id == EntityIds.GRABBED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Grabbed"; }

        private static string DefaultDescription 
        { 
            get => "This condition reflects a creature’s disposition toward a particular character, and only supernatural effects " +
                   "(like a spell) can impose these conditions on player characters. A creature that is friendly to a character " +
                   "likes that character. The character can attempt to make a Request of a friendly creature, and the friendly " +
                   "creature is likely to agree to a simple and safe request that doesn’t cost it much to fulfill. If the character " +
                   "or one of their allies uses hostile actions against the creature, the creature gains a worse attitude condition " +
                   "depending on the severity of the hostile action, as determined by the GM."; 
        }
    }    
}