using System.Collections.Generic;
using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Unconscious condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=38
    //
    // The following is automated:
    //      * -4 status penalty to AC, Perception, and Reflex saves.
    //      * Add the blinded and flat-footed conditions.
    //
    // The following must be implemented manually by the GM in game play:
    //      * Waking up.
    public class Unconscious : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.DeathAndDying; }

        private List<ICondition> _addedConditions = new List<ICondition>();

        public Unconscious() 
            : base(DefaultName, DefaultDescription, EntityIds.UNCONSCIOUS_CONDITION_ID)
        {
        }

        public Unconscious(string name, string description)
            :base(name, description, EntityIds.UNCONSCIOUS_CONDITION_ID)
        {
        }

        protected override void SetupAppliesToList()
        {
            Modifier = new Modifier() { Type = ModifierType.Status, Value = -4 };
            _appliesToList.Add(EntityIds.AC_ID);
            _appliesToList.Add(EntityIds.PERCEPTION_ID);
            _appliesToList.Add(EntityIds.REFLEX_SAVE_ID);
        }

        protected override bool CustomApply(IEntity entity)
        {
            if(entity is ICharacter character)
            {
                if(!character.Conditions.Any(c => c.Id == Id))
                {                    
                    character.Conditions.Add(this);
                    if(!character.Conditions.Any(c => c.Id == EntityIds.BLINDED_CONDITION_ID))
                    {
                        var blinded = new Blinded();
                        _addedConditions.Add(blinded);
                        character.Conditions.Add(blinded);
                    }
                    if(!character.Conditions.Any(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID))
                    {
                        var flatFooted = new FlatFooted(FlatFooted.AllCharacters);
                        _addedConditions.Add(flatFooted);
                        character.Conditions.Add(flatFooted);
                    }
                    return true;
                }
                throw new ConditionExistsException(this, character.Conditions.First(c => c.Id == Id));
            }
            return false;
        }

        protected override bool CustomRemove(IEntity entity)
        {
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.UNCONSCIOUS_CONDITION_ID))
            {
                var condition = (Unconscious)character.Conditions.First(c => c.Id == EntityIds.UNCONSCIOUS_CONDITION_ID); 
                foreach(var addedCondition in condition._addedConditions)
                {
                    if(character.Conditions.Contains(addedCondition))
                    {
                        character.Conditions.Remove(addedCondition);
                    }
                }
                condition._addedConditions.Clear();
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "Unconscious"; }

        private static string DefaultDescription 
        { 
            get => "You’re sleeping, or you’ve been knocked out. You can’t act. You take a –4 status penalty to AC, Perception, " +
                   "and Reflex saves, and you have the blinded and flat-footed conditions. When you gain this condition, you " +
                   "fall prone and drop items you are wielding or holding unless the effect states otherwise or the GM " +
                   "determines you’re in a position in which you wouldn’t.\n\n" + 
                   "If you’re unconscious because you’re dying, you can’t wake up while you have 0 Hit Points. If you are " +
                   "restored to 1 Hit Point or more via healing, you lose the dying and unconscious conditions and can act " +
                   "normally on your next turn.\n\n" +
                   "If you are unconscious and at 0 Hit Points, but not dying, you naturally return to 1 Hit Point and awaken " +
                   "after sufficient time passes. The GM determines how long you remain unconscious, from a minimum of 10 minutes " +
                   "to several hours. If you receive healing during this time, you lose the unconscious condition and can act " +
                   "normally on your next turn.\n\n" +
                   "If you’re unconscious and have more than 1 Hit Point (typically because you are asleep or unconscious due to " +
                   "an effect), you wake up in one of the following ways. Each causes you to lose the unconscious condition.\n\n" +
                   " - You take damage, provided the damage doesn’t reduce you to 0 Hit Points. If the damage reduces you to 0 " + 
                   "Hit Points, you remain unconscious and gain the dying condition as normal.\n" +
                   " - You receive healing, other than the natural healing you get from resting.\n" + 
                   " - Someone shakes you awake with an Interact action.\n" + 
                   " - There’s loud noise going on around you—though this isn’t automatic. At the start of your turn, you " +
                   "automatically attempt a Perception check against the noise’s DC (or the lowest DC if there is more than one " +
                   "noise), waking up if you succeed. If creatures are attempting to stay quiet around you, this Perception check " +
                   "uses their Stealth DCs. Some magical effects make you sleep so deeply that they don’t allow you to attempt " + 
                   "this Perception check.\n" + 
                   " - If you are simply asleep, the GM decides you wake up either because you have had a restful night’s sleep or " +
                   "something disrupted that rest."; 
        }
    }    
}