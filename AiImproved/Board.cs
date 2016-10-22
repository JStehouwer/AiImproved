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

        public Board(String[] boardState)
        {
            board = new String[42];
            board = boardState;
        }

        public Board move(int position, String player)
        {
            String[] b = board;
            for (int i = 5; i > -1; i--)
            {
                if (b[(i * 7) + position] == "0")
                {
                    b[(i * 7) + position] = player;
                }
            }
            return new Board(b);
        }

        public String[] BoardArray
        {
            get { return board; }
        }
    }
}
