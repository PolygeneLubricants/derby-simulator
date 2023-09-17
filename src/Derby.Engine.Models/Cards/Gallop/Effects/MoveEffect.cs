namespace Derby.Engine.Models.Cards.Gallop.Effects
{
    public class MoveEffect : IGallopCardEffect
    {
        private readonly int _moves;

        public MoveEffect(int moves)
        {
            _moves = moves;
        }

        public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
        {
            var field = horseToPlay.Lane.Move(horseToPlay.OwnedHorse, _moves);
            return new GallopCardResolution
            {
                NewField = field
            };
        }
    }
}
