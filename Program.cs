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