// Solution to day 25 of the Code Advent tasks of 2021
// https://adventofcode.com/2021/day/25

using System;
using System.IO;

namespace Day25
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read input file
            //var field = ReadFile(@"..\..\TestInput.txt");
            var field = ReadFile(@"..\..\Input.txt");

            if (!field.VerifyTileCount())
                Console.WriteLine("There are too many or too few tiles in the field. Please check the input file if all rows have the same amount of symbols");
            else
            {
                // Show the initial playing field
                Console.WriteLine($"Field has {field.RowCount} rows and {field.ColumnCount} columns.");
                Console.WriteLine("Initial field state:");
                Console.Write(field.ToString());
                Console.WriteLine();

                int moveCounter = ExecuteTest1(field);

                // No more changes? Print the final state of the playing field
                Console.WriteLine($"Field status after {moveCounter} moves:");
                Console.Write(field.ToString());
            }

            Console.ReadLine();
        }

        #region Task description part 1
        //This is it: the bottom of the ocean trench, the last place the sleigh keys could be.
        //Your submarine's experimental antenna still isn't boosted enough to detect the keys, but they must be here.
        //All you need to do is reach the seafloor and find them.

        //At least, you'd touch down on the seafloor if you could; unfortunately, it's completely covered by two large herds of sea cucumbers, and there isn't an open space large enough for your submarine.
        //You suspect that the Elves must have done this before, because just then you discover the phone number of a deep-sea marine biologist on a handwritten note taped to the wall of the submarine's cockpit.

        //"Sea cucumbers? Yeah, they're probably hunting for food. 
        //But don't worry, they're predictable critters: they move in perfectly straight lines, only moving forward when there's space to do so. 
        //They're actually quite polite!"

        //You explain that you'd like to predict when you could land your submarine.

        //"Oh that's easy, they'll eventually pile up and leave enough space for-- wait, did you say submarine? 
        //And the only place with that many sea cucumbers would be at the very bottom of the Mariana--" 
        //You hang up the phone.

        //There are two herds of sea cucumbers sharing the same region; one always moves east(>), while the other always moves south(v). 
        //Each location can contain at most one sea cucumber; the remaining locations are empty(.). 
        //The submarine helpfully generates a map of the situation(your puzzle input).
        
        //For example:
        //v...>>.vv>
        //.vv>>.vv..
        //>>.>v>...v
        //>>v>>.>.v.
        //v>v.vv.v..
        //>.>>..v...
        //.vv..>.>v.
        //v.v..>>v.v
        //....v..v.>

        //Every step, the sea cucumbers in the east-facing herd attempt to move forward one location, then the sea cucumbers in the south-facing herd attempt to move forward one location.
        //When a herd moves forward, every sea cucumber in the herd first simultaneously considers whether there is a sea cucumber in the adjacent location it's facing (even another sea cucumber facing the same direction), and then every sea cucumber facing an empty location simultaneously moves into that location.

        //So, in a situation like this:
        //...>>>>>...

        //After one step, only the rightmost sea cucumber would have moved:
        //...>>>>.>..

        //After the next step, two sea cucumbers move:
        //...>>>.>.>.
        
        //During a single step, the east-facing herd moves first, then the south-facing herd moves.
        //So, given this situation:
        //..........
        //.>v....v..
        //.......>..
        //..........

        //After a single step, of the sea cucumbers on the left, only the south-facing sea cucumber has moved (as it wasn't out of the way in time for the east-facing cucumber on the left to move), but both sea cucumbers on the right have moved (as the east-facing sea cucumber moved out of the way of the south-facing sea cucumber):
        //..........
        //.>........
        //..v....v>.
        //..........

        //Due to strong water currents in the area, sea cucumbers that move off the right edge of the map appear on the left edge, and sea cucumbers that move off the bottom edge of the map appear on the top edge.
        //Sea cucumbers always check whether their destination location is empty before moving, even if that destination is on the opposite side of the map:
        #region Example of exiting map and re-entering on opposite side
        //Initial state:
        //...>...
        //.......
        //......>
        //v.....>
        //......>
        //.......
        //..vvv..

        //After 1 step:
        //..vv>..
        //.......
        //>......
        //v.....>
        //>......
        //.......
        //....v..

        //After 2 steps:
        //....v>.
        //..vv...
        //.>.....
        //......>
        //v>.....
        //.......
        //.......

        //After 3 steps:
        //......>
        //..v.v..
        //..>v...
        //>......
        //..>....
        //v......
        //.......

        //After 4 steps:
        //>......
        //..v....
        //..>.v..
        //.>.v...
        //...>...
        //.......
        //v......
        #endregion

        //To find a safe place to land your submarine, the sea cucumbers need to stop moving.
        //Again consider the first example:
        #region Walk-through of the first example
        //Initial state:
        //v...>>.vv>
        //.vv>>.vv..
        //>>.>v>...v
        //>>v>>.>.v.
        //v>v.vv.v..
        //>.>>..v...
        //.vv..>.>v.
        //v.v..>>v.v
        //....v..v.>

        //After 1 step:
        //....>.>v.>
        //v.v>.>v.v.
        //>v>>..>v..
        //>>v>v>.>.v
        //.>v.v...v.
        //v>>.>vvv..
        //..v...>>..
        //vv...>>vv.
        //>.v.v..v.v

        //After 2 steps:
        //>.v.v>>..v
        //v.v.>>vv..
        //>v>.>.>.v.
        //>>v>v.>v>.
        //.>..v....v
        //.>v>>.v.v.
        //v....v>v>.
        //.vv..>>v..
        //v>.....vv.

        //After 3 steps:
        //v>v.v>.>v.
        //v...>>.v.v
        //>vv>.>v>..
        //>>v>v.>.v>
        //..>....v..
        //.>.>v>v..v
        //..v..v>vv>
        //v.v..>>v..
        //.v>....v..

        //After 4 steps:
        //v>..v.>>..
        //v.v.>.>.v.
        //>vv.>>.v>v
        //>>.>..v>.>
        //..v>v...v.
        //..>>.>vv..
        //>.v.vv>v.v
        //.....>>vv.
        //vvv>...v..

        //After 5 steps:
        //vv>...>v>.
        //v.v.v>.>v.
        //>.v.>.>.>v
        //>v>.>..v>>
        //..v>v.v...
        //..>.>>vvv.
        //.>...v>v..
        //..v.v>>v.v
        //v.v.>...v.

        //...

        //After 10 steps:
        //..>..>>vv.
        //v.....>>.v
        //..v.v>>>v>
        //v>.>v.>>>.
        //..v>v.vv.v
        //.v.>>>.v..
        //v.v..>v>..
        //..v...>v.>
        //.vv..v>vv.

        //...

        //After 20 steps:
        //v>.....>>.
        //>vv>.....v
        //.>v>v.vv>>
        //v>>>v.>v.>
        //....vv>v..
        //.v.>>>vvv.
        //..v..>>vv.
        //v.v...>>.v
        //..v.....v>

        //...

        //After 30 steps:
        //.vv.v..>>>
        //v>...v...>
        //>.v>.>vv.>
        //>v>.>.>v.>
        //.>..v.vv..
        //..v>..>>v.
        //....v>..>v
        //v.v...>vv>
        //v.v...>vvv

        //...

        //After 40 steps:
        //>>v>v..v..
        //..>>v..vv.
        //..>>>v.>.v
        //..>>>>vvv>
        //v.....>...
        //v.v...>v>>
        //>vv.....v>
        //.>v...v.>v
        //vvv.v..v.>

        //...

        //After 50 steps:
        //..>>v>vv.v
        //..v.>>vv..
        //v.>>v>>v..
        //..>>>>>vv.
        //vvv....>vv
        //..v....>>>
        //v>.......>
        //.vv>....v>
        //.>v.vv.v..

        //...

        //After 55 steps:
        //..>>v>vv..
        //..v.>>vv..
        //..>>v>>vv.
        //..>>>>>vv.
        //v......>vv
        //v>v....>>v
        //vvv...>..>
        //>vv.....>.
        //.>v.vv.v..

        //After 56 steps:
        //..>>v>vv..
        //..v.>>vv..
        //..>>v>>vv.
        //..>>>>>vv.
        //v......>vv
        //v>v....>>v
        //vvv....>.>
        //>vv......>
        //.>v.vv.v..

        //After 57 steps:
        //..>>v>vv..
        //..v.>>vv..
        //..>>v>>vv.
        //..>>>>>vv.
        //v......>vv
        //v>v....>>v
        //vvv.....>>
        //>vv......>
        //.>v.vv.v..

        //After 58 steps:
        //..>>v>vv..
        //..v.>>vv..
        //..>>v>>vv.
        //..>>>>>vv.
        //v......>vv
        //v>v....>>v
        //vvv.....>>
        //>vv......>
        //.>v.vv.v..
        //In this example, the sea cucumbers stop moving after 58 steps.
        #endregion

        //Find somewhere safe to land your submarine.
        //What is the first step on which no sea cucumbers move?
        #endregion
        private static int ExecuteTest1(PlayingField field)
        {
            GameController gameController = new GameController(field);

            // If at least one tile moved last turn, keep playing
            bool somethingMoved = true;
            int moveCounter = 0;
            while (somethingMoved)
            {
                somethingMoved = gameController.PlayTurn();
                moveCounter++;
                Console.WriteLine($"Finished move number {moveCounter}.");
            }

            return moveCounter;
        }

        /// <summary>
        /// Parse an input file that contains 'v', '>' or '.' characters.
        /// Other characters are ignored.
        /// The parsed characters will be converted to tiles and stored in a playing field.
        /// </summary>
        /// <param name="intputFile">The folder path of the file</param>
        /// <returns>A playing field, which is a list of all tiles that were created during the parsing of the file</returns>
        private static PlayingField ReadFile(string intputFile)
        {
            PlayingField field = new PlayingField();

            if (File.Exists(intputFile))
            {
                using (StreamReader sr = new StreamReader(intputFile))
                {
                    int yCoordinate = 0;
                    int xCoordinate = 0;
                    try
                    {
                        while (sr.Peek() >= 0)
                        {
                            var item = (char)sr.Read();

                            if (item == '\n')
                            {
                                // The current line ends
                                yCoordinate++;
                                xCoordinate = 0;
                            }
                            else if (item != '\r')
                            {
                                var direction = TileDirection.Direction.None;
                                if (item == '.')
                                {
                                    direction = TileDirection.Direction.None;
                                }
                                else if (item == 'v' || item == 'V')
                                {
                                    direction = TileDirection.Direction.South;
                                }
                                else if (item == '>')
                                {
                                    direction = TileDirection.Direction.East;
                                }

                                field.AddTile(xCoordinate, yCoordinate, direction);

                                xCoordinate++;
                            }
                        }
                    }
                    catch (Exception) { }
                }
            }

            return field;
        }
    }
}
