# derby-simulator
Simulator and engine for the board game Derby also known as Jägersro and Sport of Kings. 
The simulator is able to simulate the race-part of the Derby game, and not the
betting, or management-parts between each race.

The engine, however, has been designed in a way that makes it possible to extend with
these parts.

## How to run
1. Compile the project and locate the exe output in the bin folder.

2. The engine can be run from the command line:
```
Description:
  Run the derby simulator, to play through one or multiple Derby games

Usage:
  Derby.Simulator [command] [options]

Options:
  --version       Show version information
  -?, -h, --help  Show help and usage information

Commands:
  single  Run a single Derby race and show the log for each movement.
  random  Run a Derby race, where horses are chosen at random, and show the log for each movement.
  many    Run many Derby races and collect results for all games.
```

### Single
```
Description:
  Run a single Derby race and show the log for each movement.

Usage:
  Derby.Simulator single [options]

Options:
  --p1 <p1>       The stable of the first player. Indicate horse name. E.g. --p1 Avalon Isolde
  --p2 <p2>       The stable of the second player. Indicate horse name. E.g. --p2 Avalon Isolde
  --p3 <p3>       The stable of the third player. Indicate horse name. E.g. --p3 Avalon Isolde
  --p4 <p4>       The stable of the fourth player. Indicate horse name. E.g. --p4 Avalon Isolde
  --p5 <p5>       The stable of the fifth player. Indicate horse name. E.g. --p5 Avalon Isolde
  -?, -h, --help  Show help and usage information
```

### Random
```
Description:
  Run a Derby race, where horses are chosen at random, and show the log for each movement.

Usage:
  Derby.Simulator random [options]

Options:
  --c <c> (REQUIRED)  Number of horses to race. The horses will be equally distributed amongst the players 1 to 5.
  -?, -h, --help      Show help and usage information
```

### Many
```
Description:
  Run many Derby races and collect results for all games.

Usage:
  Derby.Simulator many [options]

Options:
  --mode <All|FiveYears|FourYears|ThreeYears|TwoYears>  Combination mode to run many games.
  --size <size> (REQUIRED)                              Race size. Number of horses in each race. From 1 to 5.
  --i <i>                                               Number of iterations (times) to run the race for the specified
                                                        combination. [default: 1]
  -?, -h, --help                                        Show help and usage information
```

## Command examples
### Run a single game

Run a 2-player race where player 1 has Comet and Avalon, and player 2 has Rapid.
```
Derby.Simulator single --p1 Comet Avalon --p2 Rapid
```

### Run a random game
Run a random game with 5 players, each having 1 horse. Random will distribute horses equally amongst players 1 to 5.
```
Derby.Simulator random --c 5
```

### Run many games for all horses in heats of 5
Run a game with all horses, in heat-sizes of n, i times. The number of games will 
be the cartesian product of the set size, excluding duplicates.

5 horses amongst all 20 horses for 1 iteration will run 20!/15! = 1.860.480 times.
```
Derby.Simulator many --mode all --size 5 --i 1
```

### Run many games for all 2-years in heats of 3, 1000 times
5 horses amongst all 5 2-years for 1000 iterations will run 5!/2! * 1000 = 60.000 times.
```
Derby.Simulator many --mode twoyears --size 3 --i 1000
```

## Rule interpretations
This section is dedicated to the interpretations for rules which are 
written ambiguously in the rulebook.

### Curvature
The rulebook states that horses in different lanes can be compared to each other
by observing the forward-most facing field between the lanes.

This can translate to the programmatic game, by associating each lane-field
with a tie-breaker value, that indicates how this relates to the other 3 lanes.

Because of lane curvature in the original game,
some lanes with fewer fields are ahead of lanes with more fields,
even though the lane with more fields are further from start.

For example, in the first curve, field 7 in lane 2 is ahead of
field 7 in lane 3, 4 and 5.
Field 8 in lane 3, however, is ahead of field 8 in lane 4 and 5 (but behind field 8 in lane 2).

### Steady-pace and home stretch gallop cards
The steady pace card states, that a horse will move 4 fields from next turn and onwards,
and will not pick up any gallop cards.

This has been interpreted as *all* gallop cards, including the card picked up when 
the horse is in home stretch.

Alternatively, this could be interpreted as all gallop cards from gallop card fields.
This interpretation has not been used.

### Await next and last horse

Multiple gallop cards state, that the horse must wait for the following
or last horse to be on-par with the current horse, before the current horse
may move again.

This has been interpreted as, when the card is drawn, observe who is
the next and last horse in the game, and wait for those horses specifically.
If the order changes throughout the turns, e.g. the last horse overtaking another horse,
rendering a new horse as the last, then the current horse must 
still wait for the original last horse.

Alternatively, this could be interpreted as, wait for the last or following horse, and
assess this *every* turn. E.g. if the last horse overtakes another horse,
then the horse-to-await is now a new horse, the new last. 
This interpretation has not been used because of
gallop card, crash, which specifically states that the horse must wait
for *all* horses to pass, such differentiating the wording on this card,
from the card, *Hesten falder tilbage*, which specifically states the last horse.

### Horses on the same field in the same lane
If two or more horses are on the same field in the same lane,
the rulebook states during scoring, that whichever horse is first in turn
is considered ahead of the other horse(s).

This interpretation has also been applied on the await-cards, when a horse has
to await the next or last horse.

### Awaiting a horse which is then eliminated
It can occur, that a horse is awaiting another horse, which is then eliminated
before it overtakes the awaiting horse. In this case, the awaiting horse
is effectively soft-locked from the game.

It has been interpreted as the awaiting horse may resume movement, should the horse 
be eliminated.