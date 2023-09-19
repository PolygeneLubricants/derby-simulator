namespace Derby.Engine.Race.Board.Lanes.Fields
{
    public abstract class BaseField : IField
    {
        protected BaseField(int tieBreaker)
        {
            TieBreaker = tieBreaker;
        }

        public int TieBreaker { get; }
    }
}
