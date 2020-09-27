namespace P2Modus.Core
{
    public interface ISkill : IEntity
    {
        ProficiencyRank Proficiency { get; set; }

        int AbilityScoreId { get; set; }
    }
}