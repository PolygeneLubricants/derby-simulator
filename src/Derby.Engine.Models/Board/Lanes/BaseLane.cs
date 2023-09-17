using Derby.Engine.Models.Board.Lanes.Fields;
using Derby.Engine.Models.Horses;

namespace Derby.Engine.Models.Board.Lanes;

public abstract class BaseLane : ILane
{
    protected BaseLane()
    {
        Fields = PopulateLane();
        _horseStates = new Dictionary<OwnedHorse, HorseState>();
    }

    public IList<IField> Fields { get; set; }

    private readonly IDictionary<OwnedHorse, HorseState> _horseStates;

    protected abstract IList<IField> PopulateLane();

    public IField Move(OwnedHorse ownedHorse, int moves)
    {
        var horseState = GetHorseState(ownedHorse);

        horseState.Location += moves;
        if (horseState.Location >= Fields.Count)
        {
            horseState.Location = Fields.Count - 1;
        }

        var fieldHorseLandedOn = Fields[horseState.Location];
        return fieldHorseLandedOn;
    }

    public void Register(OwnedHorse ownedHorse)
    {
        _horseStates.Add(ownedHorse, new HorseState { Horse = ownedHorse, Location = 0 });
    }

    public bool IsHomeStretch(OwnedHorse ownedHorse, int moves)
    {
        var horseState = GetHorseState(ownedHorse);
        return horseState.Location + moves >= Fields.Count;
    }

    private HorseState GetHorseState(OwnedHorse ownedHorse)
    {
        var exists = _horseStates.TryGetValue(ownedHorse, out var horseState);
        if (!exists)
        {
            throw new UnregisteredHorseException(ownedHorse);
        }

        return horseState;
    }
}