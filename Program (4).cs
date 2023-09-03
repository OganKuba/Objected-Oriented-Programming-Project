using proj1oobjmain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static System.Formats.Asn1.AsnWriter;


namespace class1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //adding authors
            BajtpikBookstore store = new BajtpikBookstore();
            //BajtpikBookstore1 store1 = new BajtpikBookstore1();
            //BajtpikBookstore3 store3 = new BajtpikBookstore3();
            adder.bookstore0(store);
            //adder.bookstore1(store1);
            //adder.bookstore3(store3);
            /////print version 0
            Console.WriteLine("printing version 0");
            store.Print();
            store.PrintAuthors();
            /////print version 1
            //Console.WriteLine("printing version 1");
            //store1.Print();
            //store1.PrintAllAuthors();
            /////using adapter from 1 to 0
            //BajtpikBookstore Adapted1 = StoreAdapter.ToStore(store1);
            //Console.WriteLine("printing version 1 after adapter usage");
            //Adapted1.Print();
            //Adapted1.PrintAuthors();
            ////using adpater from 3 to 0
            //BajtpikBookstore Adapted3 = store3adapter.ToStore(store3);
            //Console.WriteLine("printing version 3 after adapter usage");
            //Adapted3.Print();
            //Adapted3.PrintAuthors();
            //Console.WriteLine("ver0---------------------------");
            //query.printAfterYear1970(store);
            //Console.WriteLine("ver1-----------------------");
            //query.printAfterYear1970(StoreAdapter.ToStore(store1));
            //Console.WriteLine("ver3-----------------------");
            //query.printAfterYear1970(store3adapter.ToStore(store3));

            //Console.WriteLine("tree-----------------------");

            Forest forest = new Forest();
            adder.forestadder(forest);
            //treechecker.check(forest.AuthorTree);

            //Console.WriteLine("In-order traversal:");
            //IIterator<Author> inOrderIterator = forest.AuthorTree.GetInOrderIterator();
            //while (inOrderIterator.MoveNext())
            //{
            //    Console.WriteLine(inOrderIterator.Current);
            //}

            Printer printer = new Printer();
            Console.WriteLine();
            Console.WriteLine("------------------------------------------\n");
            CommandQueue commandQueue = new CommandQueue();
            var commandHistory = new CommandHistory();
            while (true)
            {
                Console.WriteLine("Enter command:");
                string input = Console.ReadLine().Trim();
                if (input.StartsWith("add"))
                {
                    string[] parts = input.Split(' ');
                    string className = parts[1];
                    string baseOrSecondary = parts[2];
                    if (baseOrSecondary == "base")
                    {
                        Console.WriteLine($"[Available fields: {Parser.GetAvailableFields(className, "base")}]");
                    }
                    else if (baseOrSecondary == "secondary")
                    {
                        Console.WriteLine($"[Available fields: {Parser.GetAvailableFields(className, "secondary")}]");
                    }
                    else
                    {
                        return;
                    }
                    while (true)
                           {
                                string line = Console.ReadLine();
                                input = input + Environment.NewLine + line;
                                if (line.Equals("DONE", StringComparison.OrdinalIgnoreCase))
                                {
                                    break;
                               }
                            if (line.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                            {

                                break;
                            }
                          }
                        ICommand command = new AddCommand(className , forest, input);
                   // command.Execute();
                    commandHistory.Execute(command);
                    //commandQueue.AddCommand(command);
                }
                else if (input == "exit")
                {

                    ICommand command = new ExitCommand();
                    // command.Execute();
                    //commandHistory.Execute(command);
                    command.Execute();
                    //commandQueue.AddCommand(command);
                }
                else if (input.StartsWith("find"))
                {
                    string[] parts = input.Split(' ');
                    string className = parts[1];

                    ICommand command = new FindCommand(className, forest, input , printer);
                   // commandHistory.Execute(command);
                    command.Execute();
                    //commandQueue.AddCommand(command);
                }
                else if (input.StartsWith("delete"))
                {
                    string[] parts = input.Split(' ');
                    string className = parts[1];
                    string objectId = parts[2];

                    ICommand command = new DeleteCommand(className , forest, input);
                    commandHistory.Execute(command);
                    //commandQueue.AddCommand(command);
                }
                else if (input.StartsWith("list"))  
                {
                    string[] parts = input.Split(' ');
                    string className = parts[1];

                    ICommand command = new ListCommand(className, forest, input , printer);
                    command.Execute();
                    //commandQueue.AddCommand(command);
                }
                else if (input.StartsWith("edit"))
                {
                    string[] parts = input.Split(' ');
                    string className = parts[1];
                    string baseOrSecondary = parts[2];
                    //string filter=input;
                    while (true)
                    {
                        string line = Console.ReadLine();
                        input = input + Environment.NewLine + line;
                        if (line.Equals("DONE", StringComparison.OrdinalIgnoreCase))
                        {
                            break;
                        }
                        if (line.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                        {

                            break;
                        }
                    }
                    ICommand command = new EditCommand(className, forest, input);
                    commandHistory.Execute(command);

                    //commandQueue.AddCommand(command);
                }
                //else if(input.StartsWith("queue print"))
                //{
                //    commandQueue.PrintCommands();
                //}
                //else if (input.StartsWith("queue dismiss"))
                //{
                //    commandQueue.Clear();
                //}
                else if (input.StartsWith("export"))
                {
                    string[] parts = input.Split(' ');
                    string filename = parts[1];
                    string format = "xml";
                    if (parts.Length > 3)
                    {
                       format = parts[2];
                    }
                    
                    ExportCommand exportCommand = new ExportCommand(filename, format, commandQueue.GetQueueCopy());
                    exportCommand.Execute();
                    //commandHistory.Execute(exportCommand);
                    string directory = Directory.GetCurrentDirectory();  // Get the current working directory
                    string filePath = Path.Combine(directory, $"{filename}.{format}");  // Combine the directory and filename
                    if (File.Exists($"{filename}.{format}"))
                    {
                        Console.WriteLine("File was created " + filePath);
                    }
                    else
                    {
                        Console.WriteLine("File does not exist.");
                    }

                }
                //else if (input.StartsWith("queue commit"))
                //{
                //    commandQueue.ExecuteCommands();
                //}
                else if (input.StartsWith("load"))
                {
                    string[] parts = input.Split(' ');
                    string filename = parts[1];
                    ICommandDeserializer commandDeserializer = null;
                    string fileExtension = Path.GetExtension(filename);
                    if (fileExtension == ".txt")
                    {
                        commandDeserializer = new PlainTextCommandDeserializer(forest);
                    }
                    else if (fileExtension == ".xml")
                    {
                        commandDeserializer = new XmlCommandDeserializer(forest);
                    }
                    //commandQueue.Load(filename , commandDeserializer);
                    LoadCommand loadCommand = new LoadCommand(filename, input ,  commandDeserializer , commandHistory);
                }
                else if (input.StartsWith("undo"))
                {
                    commandHistory.Undo();
                }
                else if (input.StartsWith("redo"))
                {
                    commandHistory.Redo();
                }
                else if (input.StartsWith("history"))
                {
                    commandHistory.PrintHistory();
                }
                else
                {
                    Console.WriteLine("Unknown command: ");
                }
            }
        }
        public static Dictionary<string, (string comparison, string value)> ParseCommandLine(string commandLine)
        {
            var filters = new Dictionary<string, (string comparison, string value)>();
            List<string> parts = TokenizeInput(commandLine);

            if (parts.Count % 3 != 0)
            {
                Console.WriteLine("Invalid command.");
                return null;
            }

            for (int i = 0; i < parts.Count; i += 3)
            {
                string key = parts[i];
                string comparisonValue = parts[i + 1];
                string value;
                if (parts[i + 2].StartsWith("\"") && parts[i + 2].EndsWith("\""))
                {
                    value = parts[i+2].Substring(1, parts[i+2].Length - 2);
                }
                else
                {
                    value = parts[i + 2];
                }
                filters[key] = (comparisonValue, value);
             }

            return filters;
        }

        private static List<string> TokenizeInput(string input)
        {
            var parts = new List<string>();
            var buffer = new StringBuilder();
            var inQuote = false;

            // Split the input based on white spaces and not considering those within quotes
            foreach (char c in input)
            {
                if (c == '\"')
                {
                    inQuote = !inQuote;
                }

                if (c == ' ' && !inQuote)
                {
                    if (buffer.Length > 0)
                    {
                        parts.Add(buffer.ToString());
                        buffer.Clear();
                    }
                }
                else
                {
                    buffer.Append(c);
                }
            }

            if (buffer.Length > 0)
            {
                parts.Add(buffer.ToString());
            }
            var tokens = new List<string>();
            for (int i = 2; i < parts.Count(); i++)
            {
                int indexOfEquals = parts[i].IndexOf('=');
                int indexOfGreaterThan = parts[i].IndexOf('>');
                int indexOfLessThan = parts[i].IndexOf('<');
                string[] words = parts[i].Split(new[] { '>', '<', '=' });
                tokens.Add(words[0]);
                bool was = false;
                if (indexOfEquals >= 0)
                {
                    tokens.Add("=");
                    was = true;
                }
                if (indexOfGreaterThan >= 0)
                {
                    tokens.Add(">");
                    was = true;
                }
                if (indexOfLessThan >= 0)
                {
                    tokens.Add("<");
                    was = true;
                }
                if (was)
                {
                    tokens.Add(words[1]);
                }
            }




            return tokens;
        }
        private static string GetAvailableFields(string className, string representation)
        {
            if (representation == "base")
            {
                switch (className.ToLower())
                {
                    case "book":
                        return "title, year, pageCount";
                    case "newspaper":
                        return "title, year, pageCount";
                    case "boardgame":
                        return "title, minPlayers, maxPlayers, difficulty";
                    case "author":
                        return "name, surname, birthYear, nickname";
                }
            }
            else if (representation == "secondary")
            {
                switch (className.ToLower())
                {
                    case "book":
                        return "id , title, year, pageCount";
                    case "newspaper":
                        return "id , title, year, pageCount";
                    case "boardgame":
                        return "id , title, minPlayers, maxPlayers, difficulty";
                    case "author":
                        return "id , name, surname, birthYear, nickname";
                }
            }
            else if (representation == "third")
            {
                switch (className.ToLower())
                {
                    case "book":
                        return "title, year, pageCount";
                    case "newspaper":
                        return "id , title, year, pageCount";
                    case "boardgame":
                        return "id , title, minPlayers, maxPlayers, difficulty";
                    case "author":
                        return "id , name, surname, birthYear, nickname";
                }
            }

            return "Invalid class name or representation.";
        }

    }
}


