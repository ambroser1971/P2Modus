using System.Collections.Generic;
using System.Linq;

namespace P2Modus.Core.Conditions
{
    // FlatFooted condition as documented here: https://2e.aonprd.com/Conditions.aspx?ID=16
    //
    // The following is applied automatically:
    //      * -2 curcumstance penalty to AC
    //
    // The following must be applied manually by the GM during play:
    public class FlatFooted : ConditionBase
    {
        private List<int> _flatFootedFrom =  new List<int>();

        public static int AllCharacters { get => -1; }

        public IReadOnlyList<int> FlatFootedFrom { get => _flatFootedFrom; }

        public FlatFooted(params int[] characterIds) 
            : base(DefaultName, DefaultDescription, EntityIds.FLAT_FOOTED_CONDITION_ID)
        {
            _flatFootedFrom.AddRange(characterIds);
        }

        public FlatFooted(string name, string description, params int[] characterIds)
            :base(name, description, EntityIds.FLAT_FOOTED_CONDITION_ID)
        {
            _flatFootedFrom.AddRange(characterIds);
        }

        public FlatFooted(FlatFooted oldFlatFooted, bool IsRemove, params int[] characterIds)
            :base(oldFlatFooted.Name, oldFlatFooted.Description, EntityIds.FLAT_FOOTED_CONDITION_ID)
        {
            var buffer = new List<int>();
            buffer.AddRange(oldFlatFooted._flatFootedFrom.ToArray());
            foreach(var characterId in characterIds)
            {
                if(IsRemove && buffer.Contains(characterId))
                {
                    buffer.Remove(characterId);
                }
                if(!IsRemove && !buffer.Contains(characterId))
                {
                    buffer.Add(characterId);
                }
            }
            _flatFootedFrom.AddRange(buffer.ToArray());
        }

        protected override void SetupAppliesToList()
        {
            this.Modifier = new Modifier() { Type = ModifierType.Circumstance, Value = -2 };
            _appliesToList.Add(EntityIds.AC_ID);
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
            if(entity is ICharacter character && character.Conditions.Any(c => c.Id == EntityIds.FLAT_FOOTED_CONDITION_ID))
            {
                var condition = character.Conditions.First(c => c.Id == this.Id);
                character.Conditions.Remove(condition);
                return true;
            }
            return false;
        }

        private static string DefaultName { get => "FlatFooted"; }

        private static string DefaultDescription 
        { 
            get => "You’re distracted or otherwise unable to focus your full attention on defense. You take a –2 circumstance penalty " +
                   "to AC. Some effects give you the flat-footed condition only to certain creatures or against certain attacks. " +
                   "Others—especially conditions—can make you universally flat-footed against everything. If a rule doesn’t specify that " +
                   "the condition applies only to certain circumstances, it applies to all of them; for example, many effects simply say " +
                   "\"The target is flat-footed.\"";
        }
    }    
}