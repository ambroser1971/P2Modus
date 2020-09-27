using System.Linq;

namespace P2Modus.Core.Conditions
{
    // Blinded condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=1
    //
    // The following is automated:
    //      * Flag for all terain to be considered difficult terrain for the affected character.
    //      * -4 status penalt to perception checks if the character's vision is the only precise sense.
    //      * Immunity to effects with the Visual trait.
    //      * Removal of the dazzled condition when blinded is applied to the character.
    //
    // The following is not automated and must be implemented manually by the GM in play:
    //      * Character can't detect anything using vision.
    //      * Automatically critically fail Perfeption checks that require being able to see.
    public class Blinded : ConditionBase
    {
        public override ConditionGroup Group { get => ConditionGroup.Senses; }
        
        public Blinded() 
            : base(DefaultName, DefaultDescription, EntityIds.BLINDED_CONDITION_ID)
        { }

        public Blinded(string name, string description)
            :base(name, description, EntityIds.BLINDED_CONDITION_ID)
        { }

        protected override bool CustomApply(IEntity entity)
        {
            if(entity is ICharacter character)
            {
                if(!character.Conditions.Any(c => c.Id == Id))
                {
                    ApplySenses(character);
                    ApplyImmunities(character);
                    _appliesToList.Add(EntityIds.ALL_TERRAIN_DIFFICULT_ID);
                    OverrideConditions(character);
                    character.Conditions.Add(this);
                    return true;
                }
                throw new ConditionExistsException(this, character.Conditions.First(c => c.Id == Id));
            }
            return false;
        }

        private void ApplySenses(ICharacter character)
        {
            if(character.Senses.Any(s => s.Id  == EntityIds.VISION_SENSE_ID && s.IsPrecise == true) && character.Senses.Count(s => s.IsPrecise == true) == 1)
            {
                Modifier = new Modifier() { Type = ModifierType.Status, Value = -4 };
            }
        }

        private void ApplyImmunities(ICharacter character)
        {
            if(!character.Immunities.Any(i => i.Id == EntityIds.VISUAL_TRAIT_ID))
            {
                //TODO: Adjust for localization
                IImmunity visualImmunity = new Immunity("Visual", "Imune to visual effects", EntityIds.VISUAL_TRAIT_ID);
                visualImmunity.Apply(character);
            }
        }

        private void OverrideConditions(ICharacter character)
        {                    
            var dazzledCondition = character.Conditions.FirstOrDefault(c => c.Id == EntityIds.DAZZLED_CONDITION_ID);
            if(dazzledCondition != null)
            {
                dazzledCondition.Remove(character);
            }
        }

        protected override bool CustomRemove(IEntity entity)
        {
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.BLINDED_CONDITION_ID))
            {
                var blindedCondition = (Blinded)character.Conditions.First(c => c.Id == EntityIds.BLINDED_CONDITION_ID); 
                RemoveImmunities(character, blindedCondition);
                character.Conditions.Remove(blindedCondition);
                return true;
            }
            return false;
        }

        private void RemoveImmunities(ICharacter character, Blinded blindedCondition)
        {
            var visualImmunity = character.Immunities.FirstOrDefault(i => i.Id == EntityIds.VISUAL_TRAIT_ID);
            if(visualImmunity != null)
            {
                visualImmunity.Remove(character);
            }
        }

        protected override void SetupAppliesToList()
        {
            _appliesToList.Add(EntityIds.PERCEPTION_ID);
            _appliesToList.Add(EntityIds.MOVEMENT_ID);
        }

        private void SetupModifier()
        {
            Modifier = new Modifier() { Type = ModifierType.Status, Value = -4 };
        }

        private static string DefaultName { get => "Blinded"; }

        private static string DefaultDescription 
        { 
            get => "You can’t see. All normal terrain is difficult terrain to you. You can’t detect anything using vision. You automatically critically fail " +
                   "Perception checks that require you to be able to see, and if vision is your only precise sense, you take a –4 status penalty to Perception " +
                   "checks. You are immune to visual effects. Blinded overrides dazzled."; 
        }
    }    
}