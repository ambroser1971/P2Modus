using System.Linq;

namespace P2Modus.Core
{
    public class Skill : BaseEntity, ISkill
    {
        public ProficiencyRank Proficiency { get; set; }

        public int AbilityScoreId { get; set; }

        public Skill(string name, string description, int id, ProficiencyRank proficiency, int abilityScoreId)
            :base(name, description, id)
        {
            Proficiency = proficiency;
            AbilityScoreId = abilityScoreId;
        }

        public override void Apply(IEntity entity)
        {
            if(entity is ICharacter character)
            {
                if(!character.Skills.Any(s => s.Id == Id))
                {
                    character.Skills.Add(this);
                    return;
                }
                var message = string.Format("Skill with Id {0} already present.", Id);
                throw new System.ArgumentException(message);
            }
            
        }

        public override void Remove (IEntity entity)
        {
            if(entity is ICharacter character && character.Skills.Contains(this))
            {
                character.Skills.Remove(this);
            }
        }

        protected override void SetupAppliesToList()
        {
            _appliesToList.Add(EntityIds.CHARACTER_ID);
        }
    }
}