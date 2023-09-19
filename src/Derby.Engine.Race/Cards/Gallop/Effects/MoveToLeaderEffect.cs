using Derby.Engine.Race.Board.Lanes;

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

        var leaderTiebreaker = leader.GetLaneTiebreaker();
        var closestLocation = GetClosestLocation(horseToPlay, leaderTiebreaker, _position);
        var moves = closestLocation - horseToPlay.Location;

        // Ignore max move rule.
        var field = horseToPlay.Move(moves);
        return new GallopCardResolution
        {
            NewField = field
        };
    }

    private int GetClosestLocation(HorseInRace horseToPlay, int leaderTiebreaker, Position position)
    {
        switch (position)
        {
            case Position.Behind:
                return horseToPlay.Lane.GetClosestLocation(leaderTiebreaker, SeekerStrategy.Before);
            case Position.OnPar:
                return horseToPlay.Lane.GetClosestLocation(leaderTiebreaker, SeekerStrategy.Closest);
            default:
                throw new ArgumentOutOfRangeException(nameof(position), position, null);
        }
    }
}

public enum Position
{
    Behind,
    OnPar
}