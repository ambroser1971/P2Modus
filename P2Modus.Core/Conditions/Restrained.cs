using System.Collections.Generic;
using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Restrained condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=33
    //
    // The following is automated:
    //      * Gain the flat-footed and immobilized conditions.
    //      * Restrained overrides Grabbed.
    //
    // The following must be enforced by GM:
    //      * Can't use any acctions with the attack or manipylate traits except to attempt to Escape or Force Open.
    public class Restrained : ConditionBase
    {
        private List<ICondition> _conditionsAdded = new List<ICondition>();

        public Restrained() 
            : base(DefaultName, DefaultDescription, EntityIds.RESTRAINED_CONDITION_ID)
        {
        }

        public Restrained(string name, string description)
            :base(name, description, EntityIds.RESTRAINED_CONDITION_ID)
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
                    RemoveGrabbedCondition(character);
                    AddFlatFootedCondition(character);
                    AddImmobilizedCondition(character);
                    return true;
                }
                throw new ConditionExistsException(this, character.Conditions.First(c => c.Id == Id));
            }
            return false;
        }

        private void RemoveGrabbedCondition(ICharacter character)
        {
            var grabbed = character.Conditions.FirstOrDefault(c => c.Id == EntityIds.GRABBED_CONDITION_ID);
            if(grabbed != null)
            {
                grabbed.Remove(character);
            }
        }

        private void AddFlatFootedCondition(ICharacter character)
        {
            var flatFooted = (FlatFooted)character.Conditions.FirstOrDefault(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID);
            if(flatFooted == null)
            {
                flatFooted = new FlatFooted(FlatFooted.AllCharacters);
                flatFooted.Apply(character);
                _conditionsAdded.Add(flatFooted);
            }
            else if(!flatFooted.FlatFootedFrom.Contains(FlatFooted.AllCharacters))
            {
                flatFooted.Remove(character);
                flatFooted = new FlatFooted(FlatFooted.AllCharacters);
                flatFooted.Apply(character);
                _conditionsAdded.Add(flatFooted);
            }
        }

        private void AddImmobilizedCondition(ICharacter character)
        {
            var immobilized = character.Conditions.FirstOrDefault(c => c.Id == EntityIds.IMMOBILIZED_CONDITION_ID);
            if(immobilized == null)
            {
                immobilized = new Immobilized();
                immobilized.Apply(character);
                _conditionsAdded.Add(immobilized);
            }
        }

        protected override bool CustomRemove(IEntity entity)
        {
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.RESTRAINED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == EntityIds.RESTRAINED_CONDITION_ID); 
                character.Conditions.Remove(condition);
                foreach(var addedCondition in _conditionsAdded)
                {
                    addedCondition.Remove(character);
                }
                _conditionsAdded.Clear();
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Restrained"; }

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