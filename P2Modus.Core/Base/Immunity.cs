using System.Linq;

namespace P2Modus.Core
{
    public class Immunity : BaseEntity, IImmunity
    {
        public Immunity(string name, string description, int traitId)
            :base(name, description, traitId)
        {}

        public override void Apply(IEntity entity)
        {
            if(entity is ICharacter character && !character.Immunities.Any(i => i.Id == Id))
            {
                this.EntityAffected = character;
                character.Immunities.Add(this);
            }
        }

        public override void Remove(IEntity entity)
        {
            if(entity is ICharacter character && character.Immunities.Contains(this))
            {
                character.Immunities.Remove(this);
            }
        }

        protected override void SetupAppliesToList()
        {
            _appliesToList.Add(EntityIds.CHARACTER_ID);
        }
    }
}