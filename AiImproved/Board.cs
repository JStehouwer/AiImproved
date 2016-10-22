using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AiImproved
{
    class Board
    {
        String[] board;

        public Board()
        {
            board = new String[42];
        }

        public Board(String[] boardState)
        {
            board = new String[42];
            StreamWriter blah = new StreamWriter("blah.txt");
            blah.WriteLine("Reading the board state");
            board = boardState;
            blah.WriteLine(board.ToString());
            //for (int i = 0; i < 42; i++)
            //{
            //    blah.WriteLine(i.ToString() + " " + boardState[i].ToString());
            //    board[i] = boardState[i].ToString();
            //}
            blah.WriteLine("Finished reading the board state");
            blah.Close();
        }
    }
}
