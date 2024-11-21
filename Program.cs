using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NHLPlayers_Ass2_OOP
{
    public class Players

    {
        public string Name;
        public string Team;
        public string Pos;
        public int GP;
        public int G;
        public int A;
        public int P;
        public int plus_minus;
        public int PIM;
        public double P_GP;
        public int PPG;
        public int PPP;
        public int SHG;
        public int SHP;
        public int GWG;
        public int OTG;
        public int S;
        public double Spercent;
        public string TOI_GP; // may have to change type later
        public double Shifts_GP;
        public double FOWpercent;

        private string NextValue(string line, ref int index)
        {
            string result = "";
            if (index < line.Length)
            {
                if (line[index] == ',')
                {
                    index++;
                }
                else if (line[index] == '"')
                {
                    int endIndex = line.IndexOf('"', index + 1);
                    result = line.Substring(index + 1, endIndex - (index + 1));
                    index = endIndex + 2;
                }
                else
                {
                    int endIndex = line.IndexOf(',', index);
                    if (endIndex == -1) result = line.Substring(index);
                    else result = line.Substring(index, endIndex - index);
                    index = endIndex + 1;
                }
            }
            return result;
        }

        public Players(string line)
        {
            int index = 0;
            Name = NextValue(line, ref index);
            Team = NextValue(line, ref index);
            Pos = NextValue(line, ref index);
            int.TryParse(NextValue(line, ref index), out GP);
            int.TryParse(NextValue(line, ref index), out G);
            int.TryParse(NextValue(line, ref index), out A);
            int.TryParse(NextValue(line, ref index), out P);
            int.TryParse(NextValue(line, ref index), out plus_minus);
            int.TryParse(NextValue(line, ref index), out PIM);
            double.TryParse(NextValue(line, ref index), out P_GP);
            int.TryParse(NextValue(line, ref index), out PPG);
            int.TryParse(NextValue(line, ref index), out PPP);
            int.TryParse(NextValue(line, ref index), out SHG);
            int.TryParse(NextValue(line, ref index), out SHP);
            int.TryParse(NextValue(line, ref index), out GWG);
            int.TryParse(NextValue(line, ref index), out OTG);
            int.TryParse(NextValue(line, ref index), out S);
            double.TryParse(NextValue(line, ref index), out Spercent);
            TOI_GP = NextValue(line, ref index);
            double.TryParse(NextValue(line, ref index), out Shifts_GP);
            double.TryParse(NextValue(line, ref index), out FOWpercent);

        }
    }

        internal class Program
    {
        static List<Players> Players = new List<Players>();

        static void BuildDBFromFile()
        {
            using (var reader = File.OpenText("NHL Player Stats 2017-18.csv"))
            {
                string input = reader.ReadLine(); ///skip the first line..
                while ((input = reader.ReadLine()) != null)
                {

                    Players playersStats = new Players(input);
                    Players.Add(playersStats); // u have to use static variable (list) in static method...

                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Strating up list of players stats...\n");
            BuildDBFromFile();
            Console.WriteLine("I am Done..\n");
            //Query 1
            {

            }
            

        }
    }
}
