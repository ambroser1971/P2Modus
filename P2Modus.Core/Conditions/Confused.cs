using System.Collections.Generic;
using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Confused condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=5
    //
    // The following is automated:
    //      * Adding of the Flat-Footed condition against all characters.
    //
    // The following must be implemented manually by the GM in game play:
    //      * Don't treat anyone as your ally.
    //      * Can't Delay, Ready, or use reactions.
    //      * Actions limited to Strike or cast offensive cantrips, and take all your actions.
    //      * Targets determined randomly.
    //      * Target and automatically hit yourself when you don't have any other targets.
    //      * If impossible to attack or cast spells, you babble incoherently, wasting your actions.
    //      * Each time you take damage from an attack or spell you can attempt a DC11 flat check to end confusion.
    public class Confused : ConditionBase
    {
        private bool _hasAppliedFlatFooted = false;

        public Confused() 
            : base(DefaultName, DefaultDescription, EntityIds.CONFUSED_CONDITION_ID)
        {
        }

        public Confused(string name, string description)
            :base(name, description, EntityIds.CONFUSED_CONDITION_ID)
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
                    if(!character.Conditions.Any(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID))
                    {
                        var flatFooted = new FlatFooted(FlatFooted.AllCharacters);
                        flatFooted.Apply(character);
                        _hasAppliedFlatFooted = true;
                        return true;
                    }
                    else
                    {
                        var flatFooted = (FlatFooted)character.Conditions.First(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID);
                        if(!flatFooted.FlatFootedFrom.Contains(FlatFooted.AllCharacters))
                        {
                            var newFlatFooted = new FlatFooted(flatFooted, false, FlatFooted.AllCharacters);
                            flatFooted.Remove(character);
                            newFlatFooted.Apply(character);
                            return true;
                        }
                    }
                }
                throw new ConditionExistsException(this, character.Conditions.First(c => c.Id == Id));
            }
            return false;
        }

        protected override bool CustomRemove(IEntity entity)
        {
            
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == Id))
            {
                var condition = character.Conditions.First(c => c.Id == Id); 
                character.Conditions.Remove(condition);
                if(_hasAppliedFlatFooted && character.Conditions.Any(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID))
                {
                    var flatFooted = (FlatFooted)character.Conditions.First(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID);
                    if(flatFooted.FlatFootedFrom.Contains(FlatFooted.AllCharacters))
                    {
                        character.Conditions.Remove(flatFooted);
                        var newFlatFooted = new FlatFooted(flatFooted, true, FlatFooted.AllCharacters);
                        if(newFlatFooted.FlatFootedFrom.Count > 0)
                        {
                            newFlatFooted.Apply(character);
                        }
                    }
                }
                _hasAppliedFlatFooted = false;
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Confused"; }

        private static string DefaultDescription 
        { 
            get => "You don’t have your wits about you, and you attack wildly. You are flat-footed, you don’t treat anyone as your " +
                   "ally (though they might still treat you as theirs), and you can’t Delay, Ready, or use reactions.\n\n" +
                   "You use all your actions to Strike or cast offensive cantrips, though the GM can have you use other actions to " +
                   "facilitate attack, such as draw a weapon, move so that a target is in reach, and so forth. Your targets are " +
                   "determined randomly by the GM. If you have no other viable targets, you target yourself, automatically hitting " +
                   "but not scoring a critical hit. If it’s impossible for you to attack or cast spells, you babble incoherently, " +
                   "wasting your actions.\n\n" +
                   "Each time you take damage from an attack or spell, you can attempt a DC 11 flat check to recover from your " +
                   "confusion and end the condition."; 
        }
    }    
}