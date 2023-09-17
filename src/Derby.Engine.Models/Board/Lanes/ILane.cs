using Derby.Engine.Models.Board.Lanes.Fields;

namespace Derby.Engine.Models.Board.Lanes;

public interface ILane
{
    IField Move(OwnedHorse ownedHorse, int moves);

    void Register(OwnedHorse ownedHorse);

    bool IsHomeStretch(OwnedHorse ownedHorseHorse, int moves);
}