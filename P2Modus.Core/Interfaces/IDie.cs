namespace P2Modus.Core
{
    public interface IDie
    {
        int Count { get; }

        int Face { get; }

        int Modifier { get; }

        string Type { get; }
    }
}