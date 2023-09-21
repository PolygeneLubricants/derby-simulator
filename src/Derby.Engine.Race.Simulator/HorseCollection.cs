using Derby.Engine.Race.Horses;

namespace Derby.Engine.Race.Simulator;

public static class HorseCollection
{
    public static Horse Get(string name)
    {
        return Horses[name];
    }

    public static IDictionary<string, Horse> Horses => new Dictionary<string, Horse>
    {
        { "Caruso"    , new Horse { Name = "Caruso"    , Color = Color.Blue,   Years = 2, Moves = new List<int> { 1,2,3,3,4,2,4,3,1,3,2,4 }}},
        { "Tristan"   , new Horse { Name = "Tristan"   , Color = Color.Black,  Years = 2, Moves = new List<int> { 4,3,2,2,2,3,2,4,2,3,1,4 }}},
        { "Figaro"    , new Horse { Name = "Figaro"    , Color = Color.White,  Years = 2, Moves = new List<int> { 4,3,2,3,2,3,4,1,4,3,2,1 }}},
        { "Rigoletto" , new Horse { Name = "Rigoletto" , Color = Color.Yellow, Years = 2, Moves = new List<int> { 3,2,3,4,4,2,3,1,1,3,2,4 }}},
        { "Isolde"    , new Horse { Name = "Isolde"    , Color = Color.Red,    Years = 2, Moves = new List<int> { 4,1,2,3,3,4,2,3,1,4,2,3 }}},

        { "Orkan"     , new Horse { Name = "Orkan"     , Color = Color.Blue,   Years = 3, Moves = new List<int> { 2,3,5,4,1,3,2,4,5,1,2,4 }}},
        { "Rapid"     , new Horse { Name = "Rapid"     , Color = Color.Black,  Years = 3, Moves = new List<int> { 5,2,1,3,4,1,5,2,4,3,5,1 }}},
        { "Vitesse"   , new Horse { Name = "Vitesse"   , Color = Color.White,  Years = 3, Moves = new List<int> { 3,4,5,2,1,3,2,4,5,1,2,4 }}},
        { "Comet"     , new Horse { Name = "Comet"     , Color = Color.Yellow, Years = 3, Moves = new List<int> { 5,3,4,2,1,5,1,3,2,4,5,1 }}},
        { "Rusch"     , new Horse { Name = "Rusch"     , Color = Color.Red,    Years = 3, Moves = new List<int> { 4,5,1,2,3,4,4,5,3,2,1,2 }}},

        { "Avalon"    , new Horse { Name = "Avalon"    , Color = Color.Blue,   Years = 4, Moves = new List<int> { 1,3,5,2,4,6,4,3,6,2,1,6 }}},
        { "Whispering", new Horse { Name = "Whispering", Color = Color.Black,  Years = 4, Moves = new List<int> { 2,3,4,5,6,1,1,2,3,4,5,6 }}},
        { "Solitude"  , new Horse { Name = "Solitude"  , Color = Color.White,  Years = 4, Moves = new List<int> { 6,1,3,3,5,5,2,2,4,4,6,1 }}},
        { "Sweet Sue" , new Horse { Name = "Sweet Sue" , Color = Color.Yellow, Years = 4, Moves = new List<int> { 6,6,5,5,3,3,2,1,1,2,4,4 }}},
        { "Rose Room" , new Horse { Name = "Rose Room" , Color = Color.Red,    Years = 4, Moves = new List<int> { 3,3,3,3,3,3,6,6,3,3,6,1 }}},

        { "Cassiopeja", new Horse { Name = "Cassiopeja", Color = Color.Blue,   Years = 5, Moves = new List<int> { 3,4,5,6,3,3,4,5,6,3,3,3 }}},
        { "Castor"    , new Horse { Name = "Castor"    , Color = Color.Black,  Years = 5, Moves = new List<int> { 1,1,4,6,5,5,5,5,3,5,5,3 }}},
        { "Aldebaran" , new Horse { Name = "Aldebaran" , Color = Color.White,  Years = 5, Moves = new List<int> { 6,2,3,3,4,4,5,5,6,6,6,2 }}},
        { "Rigel"     , new Horse { Name = "Rigel"     , Color = Color.Yellow, Years = 5, Moves = new List<int> { 6,6,6,1,3,6,4,5,6,2,3,4 }}},
        { "Pollux"    , new Horse { Name = "Pollux"    , Color = Color.Red,    Years = 5, Moves = new List<int> { 1,1,4,6,5,5,5,5,5,5,5,1 }}}
    };
}