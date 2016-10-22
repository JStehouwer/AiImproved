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
        static int Main(string[] args)
        {
            //var input = System.IO.File.ReadAllText(@"input.json");
            //var boardstring = System.Text.RegularExpressions.Regex.Match(input, @"\{.*\}").Value;
            StreamWriter rawr = new StreamWriter("rawr.txt");
            String boardString = "";
            String playerString = "";
            String timeString = "";
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-b":
                        boardString = args[i + 1];
                        break;
                    case "-p":
                        playerString = args[i + 1];
                        break;
                    case "-t":
                        timeString = args[i + 1];
                        break;
                    default:
                        break;
                }
            }

            //Write to file
            rawr.WriteLine(boardString);
            rawr.WriteLine(playerString);
            rawr.WriteLine(timeString);
            Board board = new Board(boardString.Trim(new char[] {'[', ']'}).Split(','));
            rawr.Close();

            for (int i = 0; i < 7; i++) 
            {
                if (board.BoardArray[i] == "0") 
                {
                    return i;
                }
            }
            return 0;
        }
    }
}
