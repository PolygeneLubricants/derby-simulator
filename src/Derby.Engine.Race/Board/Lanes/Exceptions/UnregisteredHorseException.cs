using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Board.Lanes.Exceptions;

public class UnregisteredHorseException : ArgumentException
{
    public UnregisteredHorseException(OwnedHorse ownedHorse) : base($"Player: '{ownedHorse.Owner.Stable.Code}', Horse: '{ownedHorse.Horse.Name}'")
    {
    }
}