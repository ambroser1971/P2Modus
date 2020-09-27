namespace P2Modus.Core
{
    public class Die : IDie
    {
        public int Count { get; }

        public int Face { get; }

        public int Modifier { get; }

        public string Type { get; }

        public Die(string type, int count, int face, int modifier)
        {
            Type = type;
            Count = count;
            Face = face;
            Modifier = modifier;
        }
    }
}