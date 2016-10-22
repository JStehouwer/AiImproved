using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AiImproved
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllText(@"input.json");
            var boardstring = System.Text.RegularExpressions.Regex.Match(input, @"\{.*\}").Value;
            AiImprovedPlayer player = new AiImprovedPlayer();
            Board board = JsonConvert.DeserializeObject<Board>(boardstring);
        }
    }
}
