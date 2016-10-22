using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;

namespace AiImproved
{
    class Program
    {
        static int Main(string[] args)
        {
            String boardString = "";
            String playerString = "";
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
                        break;
                    case "-t":
                        timeAllowed = Int32.Parse(args[i + 1]);
                        break;
                    default:
                        break;
                }
            }
            boardString = Regex.Replace(boardString, @"[\[\]]", "");
            Board board = new Board(boardString.Split(','));

            bool[] validMoves = new bool[7];
            for(int i = 0; i < 7; i++)
            {
                validMoves[i] = board.BoardArray[i] == "0";
            }

            int move = minMax(board, playerString, 15).Move;
            if (!((move < 7 && move > -1) && validMoves[move]))
            {
                while (true)
                {
                    Random rand = new Random();
                    move = rand.Next(0, 7);
                    if ((move < 7 && move > -1) && validMoves[move])
                    {
                        return move;
                    }
                }
            }
            else
            {
                return move;
            }
        }

        public static Result minMax(Board board, String player, int depth)
        {
            int bestValue = 0;
            // Check for valid moves
            bool[] validMoves = new bool[7];
            for (int i = 0; i < 7; i++)
            {
                validMoves[i] = board.BoardArray[i] == "0";
            }

            String opponent = (player == "1") ? "2" : "1";
            int winMove = checkForWin(board, player, opponent, validMoves);
            if (winMove != -1)
            {
                return new Result(winMove, 100000);
            }
            else
            {
                int loseMove = checkForWin(board, opponent, player, validMoves);
                if (loseMove != -1)
                {
                    return new Result(loseMove, -1000000);
                }
            }
            if (depth == 0)
            {
                //return new Result(0, evaluate(board, player));
            }

            bool max = true;
            if (player == "1")
            {
                bestValue = -100000;
            }
            else
            {
                bestValue = 1000000;
                max = false;
            }

            int bestMove = 0;
            for (int i = 0; i < 7; i++)
            {
                if (validMoves[i])
                {
                    bestMove = i;
                    break;
                }
            }

            for (int i = 0; i < 7; i++)
            {
                if (validMoves[i])
                {
                    Board b1 = board.move(i, player);
                    int value = minMax(b1, opponent, depth - 1).Value;
                    if ((value > bestValue && max) || (value < bestValue && !max))
                    {
                        bestValue = value;
                        bestMove = i;
                    }
                }
            }
            return new Result(bestMove, bestValue);
        }

        public static int evaluate(Board board, String player)
        {
            return 0;
        }

        public static int checkForWin(Board board, String playerString, String opponentString, bool[] validMoves)
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
                        count[j] = (board.BoardArray[i - j] == playerString) ? 1 : 0;
                    }

                    if (count.Sum() == 2)
                    {
                        return (i - Array.IndexOf(count, "0")) % 7;
                    }

                    //Diagonal Left
                    if ((i % 7) > 2)
                    {
                        if (board.BoardArray[i - 8] == opponentString || board.BoardArray[i - 16] == opponentString || board.BoardArray[i - 24] == opponentString)
                        {
                            continue;
                        }
                        count = new int[4];

                        for (int j = 0; j < 25; j += 8)
                        {
                            count[j/8] = (board.BoardArray[i - j] == playerString) ? 1 : 0;
                        }

                        if (count.Sum() == 2 && board.BoardArray[i + 7 - (Array.IndexOf(count, "0") * 8)] != "0")
                        {
                            return ((i - Array.IndexOf(count, "0")) * 8) % 7;
                        }
                    }

                    //Diagonal Right
                    if ((i % 7) < 4)
                    {
                        if (board.BoardArray[i - 6] == opponentString || board.BoardArray[i - 12] == opponentString || board.BoardArray[i - 18] == opponentString)
                        {
                            continue;
                        }
                        count = new int[4];

                        for (int j = 0; j < 4; j++)
                        {
                            count[j/8] = (board.BoardArray[i - j] == playerString) ? 1 : 0;
                        }

                        if (count.Sum() == 2 && board.BoardArray[i + 7 - (Array.IndexOf(count, "0") * 6)] != "0")
                        {
                            return ((i - Array.IndexOf(count, "0")) * 6) % 7;
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
