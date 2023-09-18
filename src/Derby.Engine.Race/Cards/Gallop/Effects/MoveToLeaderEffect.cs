namespace Derby.Engine.Race.Cards.Gallop.Effects;

public class MoveToLeaderEffect : IGallopCardEffect
{
    private readonly Position _position;

    public MoveToLeaderEffect(Position position)
    {
        _position = position;
    }

    public GallopCardResolution Resolve(HorseInRace horseToPlay, RaceState state)
    {
        var leader = state.GetLeaderHorse();
        if (leader == horseToPlay)
        {
            return new GallopCardResolution();
        }

        var moves = leader.Location - horseToPlay.Location;
        if (_position == Position.Behind)
        {
            moves--;
        }

        // Ignore max move rule.
        var field = horseToPlay.Move(moves);
        return new GallopCardResolution
        {
            NewField = field
        };
    }
}

public enum Position
{
    Behind,
    OnPar
}