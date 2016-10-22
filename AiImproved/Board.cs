using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AiImproved
{
    public class Board
    {
        private String[] board;

        public Board()
        {
            board = new String[42];
        }

        public Board(String[] boardState)
        {
            board = new String[42];
            StreamWriter blah = new StreamWriter("blah.txt");
            board = boardState;
            blah.WriteLine(String.Join("", board));
            blah.Close();
        }

        public String[] BoardArray
        {
            get { return board; }
        }
    }
}
