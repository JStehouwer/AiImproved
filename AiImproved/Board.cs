using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiImproved
{
    class Board
    {
        String[] board;

        public Board()
        {

        }

        public Board(String json)
        {
            Console.Write(json);
            for (int i = 0; i < 42; i++)
            {
                board[i] = json[i].ToString();
            }
        }
    }
}
