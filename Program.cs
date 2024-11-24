using NHLPlayers_Ass2_OOP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
                static List<Players> PerformSort(List<Players> players, List<string> sortColumns, List<string> sortOrders)
                {
                    var query = players.AsQueryable();  // Using Linq to make it query
                    for (int i = 0; i < sortColumns.Count; i++)
                    {
                        var sortColumn = sortColumns[i].ToLower().Trim();
                        var sortOrder = sortOrders[i].ToLower().Trim();

                        // Ensure the column is valid
                        var column = typeof(Players).GetFields(BindingFlags.Public | BindingFlags.Instance)
                                                      .FirstOrDefault(f => f.Name.ToLower() == sortColumn);
                        if (column == null)
                        {
                            Console.WriteLine($"Invalid Column Name: {sortColumn}");
                            continue;
                        }
                        // Apply sorting based on the current column
                        if (sortOrder == "asc")
                        {
                            query = query.OrderBy(player => column.GetValue(player));
                        }
                        else if (sortOrder == "desc")
                        {
                            query = query.OrderByDescending(player => column.GetValue(player));
                        }
                    }
                    return query.ToList();
                }
                static readonly Dictionary<string, string> columnNameMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) //dictionary to help with columns stored with different name
                {
                    { "+/-", "Plus_Minus" },
                    { "p/gp", "P_GP" }
                    { "s%", "Spercent" },
                    { "toi/gp", "TOI_GP" },
                    { "shifts/gp", "Shifts_GP" },
                    { "fow%", "FOWpercent" }
                };

                //REFERENCING//
                //we used to AI help us integrate this method so that we can be able to map user input for column names 
                static string GetInternalColumnName(string userInput)  // Helper method to map user input to internal column name
                {
                    // Check if the input is mapped to an internal column name, else return the original input
                    if (columnNameMapping.ContainsKey(userInput))
                    {
                        return columnNameMapping[userInput];
                    }
                    return userInput; // return as is if no mapping exists
                }

                static bool IsValidColumn(string columnName) //bool to double check valid column name
                {
                    var validColumns = new List<string>
                    {
                        "name", "team", "pos", "gp", "g", "a", "p", "plus_minus", "pim", "p_gp",
                        "ppg", "ppp", "shg", "shp", "gwg", "otg", "s", "spercent", "toi_gp", "shifts_gp", "fowpercent"
                    };

                    return validColumns.Contains(columnName.ToLower());
                }

            }


        }
    }
}

