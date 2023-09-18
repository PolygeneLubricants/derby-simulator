namespace Derby.Engine.Models.Horses;

public class HorseCollection
{
    public HorseCollection()
    {
        Year2Horses = Populate2Year();
        Year3Horses = Populate3Year();
        Year4Horses = Populate4Year();
        Year5Horses = Populate5Year();
    }

    public IList<Horse> Year2Horses { get; }
    public IList<Horse> Year3Horses { get; }
    public IList<Horse> Year4Horses { get; }
    public IList<Horse> Year5Horses { get; }

    // TODO: Convert to state JSON object to load on init.
    private static IList<Horse> Populate2Year()
    {
        return new List<Horse>
        {
            new Horse { Name = "Caruso",    Color = Color.Blue,   Years = 2, Moves = new List<int> { 1,2,3,3,4,2,4,3,1,3,2,4 }},
            new Horse { Name = "Tristan",   Color = Color.Black,  Years = 2, Moves = new List<int> { 4,3,2,2,2,3,2,4,2,3,1,4 }},
            new Horse { Name = "Figaro",    Color = Color.White,  Years = 2, Moves = new List<int> { 4,3,2,3,2,3,4,1,4,3,2,1 }},
            new Horse { Name = "Rigoletto", Color = Color.Yellow, Years = 2, Moves = new List<int> { 3,2,3,4,4,2,3,1,1,3,2,4 }},
            new Horse { Name = "Isolde",    Color = Color.Red,    Years = 2, Moves = new List<int> { 4,1,2,3,3,4,2,3,1,4,2,3 }},
        };
    }

    private static IList<Horse> Populate3Year()
    {
        return new List<Horse>
        {
            new Horse { Name = "Orkan",   Color = Color.Blue,   Years = 3, Moves = new List<int> { 2,3,5,4,1,3,2,4,5,1,2,4 }},
            new Horse { Name = "Rapid",   Color = Color.Black,  Years = 3, Moves = new List<int> { 5,2,1,3,4,1,5,2,4,3,5,1 }},
            new Horse { Name = "Vitesse", Color = Color.White,  Years = 3, Moves = new List<int> { 3,4,5,2,1,3,2,4,5,1,2,4 }},
            new Horse { Name = "Comet",   Color = Color.Yellow, Years = 3, Moves = new List<int> { 5,3,4,2,1,5,1,3,2,4,5,1 }},
            new Horse { Name = "Rusch",   Color = Color.Red,    Years = 3, Moves = new List<int> { 4,5,1,2,3,4,4,5,3,2,1,2 }},
        };
    }

    private static IList<Horse> Populate4Year()
    {
        return new List<Horse>
        {
            new Horse { Name = "Avalon",     Color = Color.Blue,   Years = 4, Moves = new List<int> { 1,3,5,2,4,6,4,3,6,2,1,6 }},
            new Horse { Name = "Whispering", Color = Color.Black,  Years = 4, Moves = new List<int> { 2,3,4,5,6,1,1,2,3,4,5,6 }},
            new Horse { Name = "Solitude",   Color = Color.White,  Years = 4, Moves = new List<int> { 6,1,3,3,5,5,2,2,4,4,6,1 }},
            new Horse { Name = "Sweet Sue",  Color = Color.Yellow, Years = 4, Moves = new List<int> { 6,6,5,5,3,3,2,1,1,2,4,4 }},
            new Horse { Name = "Rose Room",  Color = Color.Red,    Years = 4, Moves = new List<int> { 3,3,3,3,3,3,6,6,3,3,6,1 }},
        };
    }

    private static IList<Horse> Populate5Year()
    {
        return new List<Horse>
        {
            new Horse { Name = "Cassiopeja", Color = Color.Blue,   Years = 5, Moves = new List<int> { 3,4,5,6,3,3,4,5,6,3,3,3 }},
            new Horse { Name = "Castor",     Color = Color.Black,  Years = 5, Moves = new List<int> { 1,1,4,6,5,5,5,5,3,5,5,3 }},
            new Horse { Name = "Aldebaran",  Color = Color.White,  Years = 5, Moves = new List<int> { 6,2,3,3,4,4,5,5,6,6,6,2 }},
            new Horse { Name = "Rigel",      Color = Color.Yellow, Years = 5, Moves = new List<int> { 6,6,6,1,3,6,4,5,6,2,3,4 }},
            new Horse { Name = "Pollux",     Color = Color.Red,    Years = 5, Moves = new List<int> { 1,1,4,6,5,5,5,5,5,5,5,1 }},
        };
    }
}