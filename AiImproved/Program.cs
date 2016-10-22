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
        static void Main(string[] args)
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
            rawr.WriteLine(boardString);
            rawr.WriteLine(playerString);
            rawr.WriteLine(timeString);
            rawr.Close();
            AiImprovedPlayer player = new AiImprovedPlayer();
            Board board = new Board(boardString.Split(new char[] {',', '[', ']'}));
        }
    }
}
