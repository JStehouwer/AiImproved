using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace AiImproved
{
    class Program
    {
        public static String playerString;
        public static String opponentString;

        static int Main(string[] args)
        {
            //var input = System.IO.File.ReadAllText(@"input.json");
            //var boardstring = System.Text.RegularExpressions.Regex.Match(input, @"\{.*\}").Value;
            StreamWriter rawr = new StreamWriter("rawr.txt");
            String boardString = "";
            int timeAllowed = 0;
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-b":
                        boardString = args[i + 1];
                        break;
                    case "-p":
                        playerString = (args[i + 1] == "player-one") ? "1" : "2";
                        opponentString = (args[i + 1] == "player-one") ? "2" : "1";
                        break;
                    case "-t":
                        timeAllowed = Int32.Parse(args[i + 1]);
                        break;
                    default:
                        break;
                }
            }

            //Write to file
            rawr.WriteLine(boardString);
            rawr.WriteLine(playerString);
            rawr.WriteLine(timeAllowed.ToString());
            Board board = new Board(boardString.Trim(new char[] {'[', ']'}).Split(','));
            rawr.Close();

            // Check for valid moves
            bool[] validMoves = new bool[7];
            for (int i = 0; i < 7; i++) 
            {
                validMoves[i] = board.BoardArray[i] == "0";
            }

            return 0;
        }

        public Result minimaxSearch(Board board, bool[] validMoves) 
        {
            int bestVal = 0;

            return checkForWin(board, validMoves);
        }

        public int checkForWin(Board board, bool[] validMoves)
        {
            for (int i = 41; i > 20; i--)
            {
                if (board.BoardArray[i] == playerString)
                {
                    //Vertical win
                    if (board.BoardArray[i-7] == playerString && board.BoardArray[i-14] == playerString && board.BoardArray[i-21] == "0")
                    {
                        return i % 7;
                    }
                    //Horizontal win
                    if (board.BoardArray[i - 1] == opponentString || board.BoardArray[i - 2] == opponentString || board.BoardArray[i - 3] == opponentString)
                    {
                        continue;
                    }
                    int[] count = new int[4];

                    for (int j = 0; j < 4; j++)
                    {
                        count[j] += (board.BoardArray[i - j] == playerString) ? 1 : 0;
                    }

                    if (count.Sum() == 2)
                    {
                        return (i % 7) - (4 - Array.IndexOf(count, "0"));
                    }

                    //Diagonal Left
                    if (i % 7 < 4)
                    {
                        if (board.BoardArray[i - 8] == opponentString || board.BoardArray[i - 16] == opponentString || board.BoardArray[i - 24] == opponentString)
                        {
                            continue;
                        }
                        count = new int[4];

                        for (int j = 0; j < 25; j += 8)
                        {
                            count[j] += (board.BoardArray[i - j] == playerString) ? 1 : 0;
                        }

                        if (count.Sum() == 2)
                        {
                            return Array.IndexOf(count, "0");
                        }
                    }

                    //Diagonal Right
                    if (i % 7 > 2)
                    {
                        if (board.BoardArray[i - 6] == opponentString || board.BoardArray[i - 12] == opponentString || board.BoardArray[i - 18] == opponentString)
                        {
                            continue;
                        }
                        count = new int[4];

                        for (int j = 0; j < 4; j++)
                        {
                            count[j] += (board.BoardArray[i - j] == playerString) ? 1 : 0;
                        }

                        if (count.Sum() == 2)
                        {
                            return Array.IndexOf(count, "0");
                        }
                    }
                }
            }
            return -1;
        }

        //Class to hold returned moves and values
        public class Result
        {
            private int myMove;
            private int myVal;
            public Result(int move, int val)
            {
                myMove = move;
                myVal = val;
            }
            public int Move
            {
                get { return myMove; }
            }

            public int Value
            {
                get { return myVal; }
            }
        }
    }
}
