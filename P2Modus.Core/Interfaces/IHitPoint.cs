namespace P2Modus.Core
{
    public interface IHitPoint
    {
        int Current { get; set; }
        int Max { get; set; }

        int ModifiedMax { get; set; }

        int Temporary { get; set; }
    }
}