using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

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

        //public void ConverttoTime( string minutesColonSeconds)
        //{
        //    //minutesColonSeconds.IndexOf(":");
        //    string minutes = minutesColonSeconds.Substring(0, minutesColonSeconds.IndexOf(":"));
        //    string seconds = minutesColonSeconds.Substring(minutesColonSeconds.IndexOf(":") + 1);
        //    double convertedMins = double.Parse(minutes);
        //    double convertedSeconds = double.Parse(seconds);
        //    double result = convertedMins + convertedSeconds;

        //}

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

        static List<Players> PerformFilter(List<Players> players, string filter) //Filter function
        {
            //double value;
            Method method = new Method();
            //var filterComponents = filter.Split(new char[] { ' ' }, 3);  //splitting the filter into three components


            //////splittin ginto the three diff parts
            //string statColumn = filterComponents[0].ToLower(); // column
            //string filterOperator = filterComponents[1].ToLower(); // conditional operator
            //filterOperator = Regex.Replace(filterOperator, @"\s+", " ").Trim();
            //string filterValue = filterComponents[2].ToLower(); // value
            //filterValue = Regex.Replace(filterValue, @"\s+", " ").Trim();

            //if (isNumericColumn && filterComponents.Length != 3)
            //{
            //    Console.WriteLine();
            //    Console.WriteLine("Invalid Filter Format");
            //    return new List<Players>();
            //}



            ///REFFERENCING OUR CODE///////
            //We used AI to get this code thjat helpS us for easier separation of the filter string ,
            //before we used the code above but the updated one seems to help for more effective solution

            // Regular expression to capture the column, operator (with spaces), and value
            var regex = new Regex(@"^([\w+/%-]+)\s+(ends\swith|starts\swith|contains|==|!=|>|<|>=|<=|=)\s+(.+)$", RegexOptions.IgnoreCase);


            // Check if the filter matches the pattern
            Match match = regex.Match(filter);
            if (!match.Success)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid filter format.");
                Console.WriteLine("Please ensure components are correct and well separated with spaces");
                return new List<Players>();  // Return empty list if filter format is incorrect
            }

            // Extract the components from the regex match
            string statColumn = match.Groups[1].Value.ToLower();  // Column name
            string filterOperator = match.Groups[2].Value.ToLower();  // Operator
            string filterValue = match.Groups[3].Value.ToLower();  // Filter value


            filterOperator = Regex.Replace(filterOperator, @"\s+", " ").Trim();  // Clean up the operator and value (remove extra spaces if needed)
            filterValue = Regex.Replace(filterValue, @"\s+", " ").Trim();  // Clean up the value and value (remove extra spaces if needed)

            bool containsColon = filterValue.Contains(":");



            if ((statColumn == "toi/gp") && containsColon) // if its the column  of time type
            {
                filterValue = method.ConverttoTime(filterValue).ToString();
            }
            if ((statColumn == "toi/gp") && !containsColon) // if its the column  of time type
            {
                Console.WriteLine();
                Console.WriteLine("The TOI/GP Column is to be compared with values in mm:ss (e.g 12:10)");
                return new List<Players>();

            }

            double value = 0;
            bool isNumericColumn = statColumn != "name" && statColumn != "pos" && statColumn != "team" && !containsColon;


            if (isNumericColumn && !double.TryParse(filterValue, out value)) //for columns that are not number values, we dont need to parse it
            {
                Console.WriteLine();
                Console.WriteLine("Invalid value for comparison.");
                Console.WriteLine("Make sure there's no space between operators if using symbols and use correct values.");
                return new List<Players>();
            }

            // manipulating input string for columns that were named differently 
            if (statColumn == "+/-") { statColumn = "plus_minus"; }
            if (statColumn == "p/gp") { statColumn = "p_gp"; }
            if (statColumn == "s%") { statColumn = "spercent"; }
            if (statColumn == "toi/gp") { statColumn = "toi_gp"; }
            if (statColumn == "shifts/gp") { statColumn = "shifts_gp"; }
            if (statColumn == "fow%") { statColumn = "fowpercent"; }
            if (filterOperator == "starts with") { filterOperator = "startsWith"; }
            if (filterOperator == "contains") { filterOperator = "contains"; }
            if (filterOperator == "ends with") { filterOperator = "endsWith"; }

            var column = typeof(Players).GetFields(BindingFlags.Public | BindingFlags.Instance)
                                            .FirstOrDefault(f => f.Name.ToLower() == statColumn); // COnfirming column is available (also case insensitive too)
            if (column == null)
            {
                Console.WriteLine("Invalid Column Name");
            }
            IEnumerable<Players> filteredQuery = players; // Applying the filter based on operator
            switch (filterOperator)
            {
                case "==":
                case "=":
                    if (statColumn == "toi_gp")
                    {
                        filteredQuery = players.Where(q => method.ConverttoTime(q.TOI_GP) == value);

                    }
                    else if (statColumn == "name")
                    {
                        filteredQuery = players.Where(q => q.Name.ToLower() == filterValue);
                    }
                    else if (statColumn == "team")
                    {
                        filteredQuery = players.Where(q => q.Team.ToLower() == filterValue);
                    }
                    else if (statColumn == "pos")
                    {
                        filteredQuery = players.Where(q => q.Pos.ToLower() == filterValue);
                    }
                    //else if(statColumn == "name" || statColumn == "team" || statColumn == "pos")
                    //{

                    //    filteredQuery = players.Where(q => {

                    //        var fieldValue = column.GetValue(q);

                    //        // Ensure the field value is a string, and perform the comparison case-insensitively
                    //        if (fieldValue is string fieldStr)
                    //        {
                    //            return fieldStr.ToLower() == filterValue.ToLower();
                    //        }
                    //        // return new List<Players>();
                    //        return false;
                    //    } );
                    //}
                    else
                    {
                        filteredQuery = players.Where(q => (double)column.GetValue(q) == value);
                    }
                    break;
                case "startsWith":
                    if (statColumn == "name")
                    {
                        filteredQuery = players.Where(q => q.Name.StartsWith(filterValue, StringComparison.OrdinalIgnoreCase));
                    }
                    else if (statColumn == "team")
                    {
                        filteredQuery = players.Where(q => q.Team.StartsWith(filterValue, StringComparison.OrdinalIgnoreCase));
                    }
                    else if (statColumn == "pos")
                    {
                        filteredQuery = players.Where(q => q.Pos.StartsWith(filterValue, StringComparison.OrdinalIgnoreCase));
                    }
                    break;
                case "endsWith":
                    if (statColumn == "name")
                    {
                        filteredQuery = players.Where(q => q.Name.EndsWith(filterValue, StringComparison.OrdinalIgnoreCase));
                    }
                    else if (statColumn == "team")
                    {
                        filteredQuery = players.Where(q => q.Team.EndsWith(filterValue, StringComparison.OrdinalIgnoreCase));
                    }
                    else if (statColumn == "pos")
                    {
                        filteredQuery = players.Where(q => q.Pos.EndsWith(filterValue, StringComparison.OrdinalIgnoreCase));
                    }
                    break;
                case "contains":
                    if (statColumn == "name")
                    {
                        filteredQuery = players.Where(q => q.Name.IndexOf(filterValue, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    else if (statColumn == "team")
                    {
                        filteredQuery = players.Where(q => q.Team.IndexOf(filterValue, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    else if (statColumn == "pos")
                    {
                        filteredQuery = players.Where(q => q.Pos.IndexOf(filterValue, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    break;
                case ">":
                    if (statColumn == "toi_gp")
                    {
                        filteredQuery = players.Where(q => method.ConverttoTime(q.TOI_GP) > value);
                    }
                    else
                    {
                        filteredQuery = players.Where(q => (double)column.GetValue(q) > value);
                    }
                    break;
                case "<":
                    if (statColumn == "toi_gp")
                    {
                        filteredQuery = players.Where(q => method.ConverttoTime(q.TOI_GP) < value);
                    }
                    else
                    {
                        filteredQuery = players.Where(q => (double)column.GetValue(q) < value);
                    }
                    break;
                case ">=":
                    if (statColumn == "toi_gp")
                    {
                        filteredQuery = players.Where(q => method.ConverttoTime(q.TOI_GP) >= value);
                    }
                    else
                    {
                        filteredQuery = players.Where(q => (double)column.GetValue(q) >= value);
                    }
                    break;
                case "<=":
                    if (statColumn == "toi_gp")
                    {
                        filteredQuery = players.Where(q => method.ConverttoTime(q.TOI_GP) <= value);
                    }
                    else
                    {
                        filteredQuery = players.Where(q => (double)column.GetValue(q) <= value);
                    }
                    break;
                case "!=":
                    if (statColumn == "toi_gp")
                    {
                        filteredQuery = players.Where(q => method.ConverttoTime(q.TOI_GP) != value);
                    }
                    else
                    {
                        filteredQuery = players.Where(q => (double)column.GetValue(q) != value);
                    }
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Invalid filter operator.");
                    return new List<Players>();
                    //return players; // Return original list if the operator is invalid
            }

            return filteredQuery.ToList(); // Return the filtered list
        }


        static List<Players> PerformSort(List<Players> players, string sortColumn, string sortOrder) //Sort function
        {
            var column = typeof(Players).GetFields(BindingFlags.Public | BindingFlags.Instance)
                                            .FirstOrDefault(f => f.Name.ToLower() == sortColumn
                                         ); // COnfirming column is available (also case insensitive too)

            if (column == null)
            {
                Console.WriteLine("Invalid Column Name");
            }
            IEnumerable<Players> filteredQuery = players;

            if (sortOrder == "asc")
            { filteredQuery = filteredQuery.OrderBy(player => column.GetValue(player)); }
            else if (sortOrder == "desc")
            { filteredQuery = filteredQuery.OrderByDescending(player => column.GetValue(player)); }


            return filteredQuery.ToList();
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Your Table with player stats(Name, Team, Pos, GP, G, A, P, +/-, PIM, P/GP, PPG, PPP, SHG, SHP, GWG, OTG, S, S%, TOI/GP, Shifts/GP, FOW%)...\n");

            BuildDBFromFile(); //building my dtata from csv file

            List<Players> filteredPlayersList = new List<Players>(Players); // This holds the filtered players list
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose an Option (1, 2, 3 or 4)");
                Console.WriteLine("1. Filter" + "\n" + "2. Sort" + "\n" + "3. Display Table of Player Stats" + "\n" + "4. Exit");
                string option = Console.ReadLine().Trim();
                //option = Regex.Replace(option, @"\s+", " ").Trim();
                while (string.IsNullOrEmpty(option) || (option != "1" && option != "2" && option != "3" && option != "4"))
                {
                    Console.WriteLine("Invalid input. Please choose a valid option (1, 2, 3 or 4):");
                    option = Console.ReadLine().Trim();
                }
                if (option == "1") //if user wants to filter
                {
                    bool isValidFilter = false; // field to check if filter is valid

                    while (!isValidFilter)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Available columns for filtering with numbers : GP, G, A, P, +/-, PIM, P/GP, PPG, PPP, SHG, SHP, GWG, OTG, S, S%, TOI/GP, Shifts/GP, FOW%");
                        Console.WriteLine("(e.g., 'G >= 50')");
                        Console.WriteLine();
                        Console.WriteLine("Available columns for filtering with words / letters : Name, Team, Pos");
                        Console.WriteLine("(e.g., 'Name == Mark Alt' or 'Name Starts With Mark' or 'Name Ends With Alt' or 'Name Contains Mark')");
                        Console.WriteLine();
                        Console.WriteLine("Please Enter A Filter Expression:");


                        string Filter = Console.ReadLine().Trim();
                        bool containsAnd = Filter.Contains("&&"); // check if filter contains && symbol                    
                        bool containsComma = Filter.Contains(","); // check if filter cointains so we know if user wants multiple filtering;
                        bool containsOr = Filter.Contains("||"); // check if filter contains || symbol

                        if (string.IsNullOrWhiteSpace(Filter))
                        {
                            Console.WriteLine("Filter cannot be empty. Please enter a valid filter.");
                            continue; // Re-ask if the filter is empty
                        }
                        try
                        {
                            if (containsOr)
                            {
                                var orComponents = Regex.Split(Filter, @"\s*\|\|\s*"); // splitting components with ||
                                var combinedList = new List<Players>();

                                foreach (var orComponent in orComponents)
                                {
                                    string mainOrComponent = Regex.Replace(orComponent, @"\s+", " ").Trim();
                                    var andComponents = Regex.Split(mainOrComponent, @"\s*\&\&\s*"); // splitting components with , or &&
                                    var filteredPlayersForAnd = Players;
                                    foreach (var andComponent in andComponents)
                                    {
                                        string mainAndComponent = Regex.Replace(andComponent, @"\s+", " ").Trim();
                                        filteredPlayersForAnd = PerformFilter(filteredPlayersForAnd, mainAndComponent);
                                    }
                                    combinedList.AddRange(filteredPlayersForAnd);
                                    filteredPlayersList = combinedList;
                                }
                                combinedList = combinedList.Distinct().ToList(); // Remove duplicates and display the combined list
                                DisplayAllPlayersInTabularFormat(combinedList);
                            }
                            else if (containsComma || containsAnd)
                            {

                                var filterComponents = Regex.Split(Filter, @"[,\&]{1,2}"); // splitting components with , or &&
                                var filteredPlayers = Players;
                                foreach (var component in filterComponents)
                                {
                                    string mainComponent = Regex.Replace(component, @"\s+", " ").Trim(); // replacing unnecessary too much space btw three components with a space , removing all trailing and leading space.    
                                    filteredPlayers = PerformFilter(filteredPlayers, mainComponent);
                                    filteredPlayersList = filteredPlayers;
                                }
                                DisplayAllPlayersInTabularFormat(filteredPlayers);
                            }
                            else
                            {
                                Filter = Regex.Replace(Filter, @"\s+", " ").Trim(); // replacing unnecessary too much space btw three components with a space , removing all trailing and leading space.
                                var filteredPlayers = PerformFilter(Players, Filter);
                                filteredPlayersList = filteredPlayers;
                                DisplayAllPlayersInTabularFormat(filteredPlayers);
                            }

                            isValidFilter = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine($"Invalid filter format. Please try again and make sure evrything is spaced out good and written with the correct operator.");
                        }
                    }
                    if (filteredPlayersList.Count() == 0)
                    {

                        Console.WriteLine("Sorry, there is nothing to show.....");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Do you want to sort? \n1.Yes \n2.No");
                        string ans = Console.ReadLine().Trim();
                        while (string.IsNullOrEmpty(ans) || (ans != "1" && ans != "2"))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Please Enter a Valid Option (1 or 2)");
                            Console.WriteLine();
                            ans = Console.ReadLine().Trim();
                        }

                        if (ans == "1")
                        {
                            Console.WriteLine("Enter the column to sort by (e.g., 'G', 'P', 'GP'.....):");
                            string sortColumn = Console.ReadLine().ToLower().Trim();

                            while ((string.IsNullOrEmpty(sortColumn) ||
                                (sortColumn != "name" && sortColumn != "team" && sortColumn != "pos" && sortColumn != "gp" &&
                                sortColumn != "g" && sortColumn != "a" && sortColumn != "p" && sortColumn != "+/-" &&
                                sortColumn != "pim" && sortColumn != "p/gp" && sortColumn != "ppg" && sortColumn != "ppp" &&
                                sortColumn != "shg" && sortColumn != "shp" && sortColumn != "gwg" && sortColumn != "otg" &&
                                sortColumn != "s" && sortColumn != "s%" && sortColumn != "toi/gp" && sortColumn != "shifts/gp" &&
                                sortColumn != "fow%")))
                            {
                                Console.WriteLine();
                                Console.WriteLine("Please enter a valid column name \n" +
                                    "(Name, Team, Pos, GP, G, A, P, +/-, PIM, P/GP, PPG, PPP, SHG, SHP, GWG, OTG, S, S%, TOI/GP, Shifts/GP, FOW%)");
                                Console.WriteLine();
                                sortColumn = Console.ReadLine().ToLower().Trim();

                            }

                            // sortColumn = sortColumn.Trim(); //removing leading and trailing space incase user makes mistake.
                            Console.WriteLine("Enter sort order ('asc' for ascending, 'desc' for descending):");
                            string sortOrder = Console.ReadLine().ToLower().Trim();
                            while ((string.IsNullOrEmpty(sortOrder) || (sortOrder != "asc" && sortOrder != "desc")))
                            {
                                Console.WriteLine("Please enter a valid order (asc or desc)");
                                sortOrder = Console.ReadLine().ToLower().Trim();
                            }
                            // manipulating input string for columns that were named differently
                            if (sortColumn == "+/-") { sortColumn = "plus_minus"; }
                            if (sortColumn == "p/gp") { sortColumn = "p_gp"; }
                            if (sortColumn == "s%") { sortColumn = "spercent"; }
                            if (sortColumn == "toi/gp") { sortColumn = "toi_gp"; }
                            if (sortColumn == "shifts/gp") { sortColumn = "shifts_gp"; }
                            if (sortColumn == "fow%") { sortColumn = "fowpercent"; }

                            var sortedPlayers = PerformSort(filteredPlayersList, sortColumn, sortOrder);
                            DisplayAllPlayersInTabularFormat(sortedPlayers);
                        }
                        else if (ans == "2")
                        {
                            Console.WriteLine("Returning to menu...");
                        }

                    }
                }
                else if (option == "2")
                {

                    Console.WriteLine("Enter the column to sort by (e.g., 'G', 'P', 'GP'.....):");
                    string sortColumn = Console.ReadLine().ToLower().Trim();

                    while ((string.IsNullOrEmpty(sortColumn) ||
                        (sortColumn != "name" && sortColumn != "team" && sortColumn != "pos" && sortColumn != "gp" &&
                        sortColumn != "g" && sortColumn != "a" && sortColumn != "p" && sortColumn != "+/-" &&
                        sortColumn != "pim" && sortColumn != "p/gp" && sortColumn != "ppg" && sortColumn != "ppp" &&
                        sortColumn != "shg" && sortColumn != "shp" && sortColumn != "gwg" && sortColumn != "otg" &&
                        sortColumn != "s" && sortColumn != "s%" && sortColumn != "toi/gp" && sortColumn != "shifts/gp" &&
                        sortColumn != "fow%")))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please enter a valid column name \n" +
                            "(Name, Team, Pos, GP, G, A, P, +/-, PIM, P/GP, PPG, PPP, SHG, SHP, GWG, OTG, S, S%, TOI/GP, Shifts/GP, FOW%)");
                        Console.WriteLine();
                        sortColumn = Console.ReadLine().ToLower().Trim();

                    }

                    // sortColumn = sortColumn.Trim(); //removing leading and trailing space incase user makes mistake.
                    Console.WriteLine("Enter sort order ('asc' for ascending, 'desc' for descending):");
                    string sortOrder = Console.ReadLine().ToLower().Trim();
                    while ((string.IsNullOrEmpty(sortOrder) || (sortOrder != "asc" && sortOrder != "desc")))
                    {
                        Console.WriteLine("Please enter a valid order (asc or desc)");
                        sortOrder = Console.ReadLine().ToLower().Trim();
                    }
                    // manipulating input string for columns that were named differently
                    if (sortColumn == "+/-") { sortColumn = "plus_minus"; }
                    if (sortColumn == "p/gp") { sortColumn = "p_gp"; }
                    if (sortColumn == "s%") { sortColumn = "spercent"; }
                    if (sortColumn == "toi/gp") { sortColumn = "toi_gp"; }
                    if (sortColumn == "shifts/gp") { sortColumn = "shifts_gp"; }
                    if (sortColumn == "fow%") { sortColumn = "fowpercent"; }

                    var sortedPlayers = PerformSort(Players, sortColumn, sortOrder);
                    DisplayAllPlayersInTabularFormat(sortedPlayers);
                }

                else if (option == "3")
                {
                    Console.WriteLine();
                    //Thread.Sleep(1000);
                    DisplayAllPlayersInTabularFormat(Players);
                    //break ;
                }
                else if (option == "4")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Invalid option, please try again.");
                }
            }
        }
    }
}

