using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using System.Text.RegularExpressions;


namespace NHLPlayers_Ass2_OOP
{
    public class Method
    {
        public double ConverttoTime(string minutesColonSeconds)
        {
            int colonIndex = minutesColonSeconds.IndexOf(":");
            if (colonIndex == -1)
            {
                // If there is no colon, return an error message or handle the situation
                return -1;
            }
            else
            {
                try
                {
                    string minutes = minutesColonSeconds.Substring(0, colonIndex);
                    string seconds = minutesColonSeconds.Substring(colonIndex + 1);
                    double convertedMins = double.Parse(minutes);
                    double convertedSeconds = double.Parse(seconds);
                    double result = (convertedMins * 60) + convertedSeconds;

                    return result;
                }
                catch
                {
                    return -1;
                }

            }

        }
    }
    public class Players
    {
        public string Name;
        public string Team;
        public string Pos;
        public double GP;
        public double G;
        public double A;
        public double P;
        public double Plus_Minus;
        public double PIM;
        public double P_GP;
        public double PPG;
        public double PPP;
        public double SHG;
        public double SHP;
        public double GWG;
        public double OTG;
        public double S;
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
            double.TryParse(NextValue(line, ref index), out GP);
            double.TryParse(NextValue(line, ref index), out G);
            double.TryParse(NextValue(line, ref index), out A);
            double.TryParse(NextValue(line, ref index), out P);
            double.TryParse(NextValue(line, ref index), out Plus_Minus);
            double.TryParse(NextValue(line, ref index), out PIM);
            double.TryParse(NextValue(line, ref index), out P_GP);
            double.TryParse(NextValue(line, ref index), out PPG);
            double.TryParse(NextValue(line, ref index), out PPP);
            double.TryParse(NextValue(line, ref index), out SHG);
            double.TryParse(NextValue(line, ref index), out SHP);
            double.TryParse(NextValue(line, ref index), out GWG);
            double.TryParse(NextValue(line, ref index), out OTG);
            double.TryParse(NextValue(line, ref index), out S);
            double.TryParse(NextValue(line, ref index), out Spercent);
            TOI_GP = NextValue(line, ref index);

            double.TryParse(NextValue(line, ref index), out Shifts_GP);
            double.TryParse(NextValue(line, ref index), out FOWpercent);

        }
    }

    internal class Program

    {

        private static List<Players> Players = new List<Players>();

        static void BuildDBFromFile() //building player stats from excel file.

        {

            using (var reader = System.IO.File.OpenText("NHL Player Stats 2017-18.csv"))

            {

                string input = reader.ReadLine(); ///skip the first line..

                while ((input = reader.ReadLine()) != null)

                {

                    Players playersStats = new Players(input);

                    Players.Add(playersStats); // u have to use static variable (list) in static method...

                }

            }

        }

        static void DisplayAllPlayersInTabularFormat(List<Players> Playerss)

        {

            int playerNameWidth = 24;

            int shifts_GpWidth = 9;

            int teamWidth = 6;

            int fowPercentWidth = 5;

            int defColumnWidth = 3;


            // creating UI for table to display data

            Console.WriteLine();

            Console.WriteLine();

            Console.WriteLine(" ________________________ ______ ___ ___ ___ ___ ___ ___   TABLE  ___ ___ ___ ___ ___ ___ ___ ___ ______ _________ _____ ");

            Console.WriteLine(" ________________________ ______ ___ ___ ___ ___ ___ ___ ___ ____ ___ ___ ___ ___ ___ ___ ___ ___ ______ _________ _____ ");

            Console.WriteLine("");

            Console.WriteLine("|      Player Name       | Team |Pos| GP| G | A | P |+/-|PIM|P/GP|PPG|PPP|SHG|SHP|GWG|OTG| S | S%|TOI/GP|Shifts/GP|FOW%|");

            Console.WriteLine(" ________________________ ______ ___ ___ ___ ___ ___ ___ ___ ____ ___ ___ ___ ___ ___ ___ ___ ___ ______ _________ _____ ");


            foreach (var player in Playerss)

            {

                Console.WriteLine("");

                Console.WriteLine("|" + player.Name.PadLeft((playerNameWidth + player.Name.Length) / 2).PadRight(playerNameWidth) + "|" +

                    player.Team.PadLeft((teamWidth + player.Team.Length) / 2).PadRight(teamWidth) + "|" +

                    player.Pos.PadRight((defColumnWidth + player.Pos.Length) / 2).PadLeft(defColumnWidth) + "|" +

                    player.GP.ToString().PadLeft((defColumnWidth + player.GP.ToString().Length) / 2).PadRight(defColumnWidth) + "|" +

                    player.G.ToString().PadLeft((defColumnWidth + player.G.ToString().Length) / 2).PadRight(defColumnWidth) + "|" +

                    player.A.ToString().PadLeft((defColumnWidth + player.A.ToString().Length) / 2).PadRight(defColumnWidth) + "|" +

                    player.P.ToString().PadLeft((defColumnWidth + player.P.ToString().Length) / 2).PadRight(defColumnWidth) + "|" +

                    player.Plus_Minus.ToString().PadLeft((defColumnWidth + player.Plus_Minus.ToString().Length) / 2).PadRight(defColumnWidth) + "|" +

                    player.PIM.ToString().PadLeft((defColumnWidth + player.PIM.ToString().Length) / 2).PadRight(defColumnWidth) + "|" +

                    player.P_GP.ToString().PadLeft((defColumnWidth + player.P_GP.ToString().Length) / 2).PadRight(defColumnWidth) + "|" +

                    player.PPG.ToString().PadLeft((defColumnWidth + player.PPG.ToString().Length) / 2).PadRight(defColumnWidth) + "|" +

                    player.PPP.ToString().PadLeft((defColumnWidth + player.PPP.ToString().Length) / 2).PadRight(defColumnWidth) + "|" +

                    player.SHG.ToString().PadLeft((defColumnWidth + player.SHG.ToString().Length) / 2).PadRight(defColumnWidth) + "|" +

                    player.SHP.ToString().PadLeft((defColumnWidth + player.SHP.ToString().Length) / 2).PadRight(defColumnWidth) + "|" +

                    player.GWG.ToString().PadLeft((defColumnWidth + player.GWG.ToString().Length) / 2).PadRight(defColumnWidth) + "|" +

                    player.OTG.ToString().PadLeft((defColumnWidth + player.OTG.ToString().Length) / 2).PadRight(defColumnWidth) + "|" +

                    player.S.ToString().PadLeft((defColumnWidth + player.S.ToString().Length) / 2).PadRight(defColumnWidth) + "|" +

                    player.Spercent.ToString().PadLeft((defColumnWidth + player.Spercent.ToString().Length) / 2).PadRight(defColumnWidth) + "|" +

                    player.TOI_GP.PadLeft((teamWidth + player.TOI_GP.Length) / 2).PadRight(teamWidth) + "|" +

                    player.Shifts_GP.ToString().PadLeft((shifts_GpWidth + player.Shifts_GP.ToString().Length) / 2).PadRight(shifts_GpWidth) + "|" +

                    player.FOWpercent.ToString().PadLeft((fowPercentWidth + player.FOWpercent.ToString().Length) / 2).PadRight(fowPercentWidth)

                );

                Console.WriteLine(" ________________________ ______ ___ ___ ___ ___ ___ ___ ___ ____ ___ ___ ___ ___ ___ ___ ___ ___ ______ _________ _____ ");

            }

        }
 