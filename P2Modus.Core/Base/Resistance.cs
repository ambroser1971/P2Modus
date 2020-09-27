using System.Linq;

namespace P2Modus.Core
{
    public class Resistance: BaseEntity, IResistance
    {
        public int Value { get; set; }

         public Resistance(string name, string description, int traitId, int value)
            :base(name, description, traitId)
        {
            Value = value;
        }

        public override void Apply(IEntity entity)
        {
            if(entity is ICharacter character)
            {
                var oldResistance = character.Resistances.FirstOrDefault(i => i.Id == Id);
                if(oldResistance == null)
                {
                    this.EntityAffected = character;
                    character.Resistances.Add(this);
                }
                else if(oldResistance.Value < this.Value)
                {
                    oldResistance.Value = this.Value;
                }
            }
        }

        public override void Remove(IEntity entity)
        {
            if(entity is ICharacter character && character.Resistances.Contains(this))
            {
                character.Resistances.Remove(this);
            }
        }

        protected override void SetupAppliesToList()
        {
            _appliesToList.Add(EntityIds.CHARACTER_ID);
        }
    }
}