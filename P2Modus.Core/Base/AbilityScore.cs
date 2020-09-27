namespace P2Modus.Core
{
    public class AbilityScore : BaseEntity, IAbilityScore
    {
        public int Value { get; set; }

        public IModifier Modifier { get; private set; }

        public AbilityScore(int entityId, string name, string description, int value)
            :base(name, description, entityId)
        {
            var calc = new Calculator();

            Value = value;
            Modifier = new Modifier() 
            { 
                Type = ModifierType.Ability, 
                Value = calc.CalculateAbilityModifier(Value) 
            };            
        }
        public override void Apply(IEntity entity)
        {

        }

        public override void Remove(IEntity entity)
        {

        }

        protected override void SetupAppliesToList()
        {
            
        }
        
    }
}