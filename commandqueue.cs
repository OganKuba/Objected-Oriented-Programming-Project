using class1;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace proj1oobjmain
{
    public interface ICommand
    {
        void Execute();
        void UnExecute();
    }
    public interface ICommandDeserializer
    {
        IEnumerable<ICommand> Deserialize(string filePath);
    }
    public class PlainTextCommandDeserializer : ICommandDeserializer
    {
        private CommandParser commandParser;
        private static readonly string[] Keywords = { "exit", "add", "delete", "find", "list", "edit" };

        public PlainTextCommandDeserializer(Forest forest)
        {
           
            Printer myPrinter = new Printer();
            commandParser = new CommandParser(forest, myPrinter);

        }

        public IEnumerable<ICommand> Deserialize(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                StringBuilder commandBuilder = new StringBuilder();
                while ((line = reader.ReadLine()) != null)
                {
                    string trimmedLine = line.Trim();
                    if (Keywords.Any(keyword => trimmedLine.StartsWith(keyword, StringComparison.OrdinalIgnoreCase)))
                    {
                        if (commandBuilder.Length > 0) // We have a command to yield
                        {
                            yield return commandParser.Parse(commandBuilder.ToString());
                            commandBuilder.Clear();
                        }
                    }

                    commandBuilder.AppendLine(line);
                }

                if (commandBuilder.Length > 0) // Yield the final command, if any
                {
                    yield return commandParser.Parse(commandBuilder.ToString());
                }
            }
        }
    }
    public class XmlCommandDeserializer : ICommandDeserializer
    {
        private CommandParser commandParser;
        private static readonly string[] Keywords = { "exit", "add", "delete", "find", "list", "edit" };

        public XmlCommandDeserializer(Forest forest)
        {
            
            Printer myPrinter = new Printer();
            commandParser = new CommandParser(forest, myPrinter);

        }

        public IEnumerable<ICommand> Deserialize(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            StringBuilder commandBuilder = new StringBuilder();
            foreach (XElement commandElement in doc.Root.Elements())
            {
                string commandLine = commandElement.Value;
                string trimmedLine = commandLine.Trim();
                if (Keywords.Any(keyword => trimmedLine.StartsWith(keyword, StringComparison.OrdinalIgnoreCase)))
                {
                    if (commandBuilder.Length > 0) // We have a command to yield
                    {
                        yield return commandParser.Parse(commandBuilder.ToString());
                        commandBuilder.Clear();
                    }
                }

                commandBuilder.AppendLine(commandLine);
            }

            if (commandBuilder.Length > 0) // Yield the final command, if any
            {
                yield return commandParser.Parse(commandBuilder.ToString());
            }
        }
    }







    public class CommandQueue
    {
        private readonly Queue<ICommand> _queue = new Queue<ICommand>();
        public Queue<ICommand> GetQueueCopy()
        {
            return new Queue<ICommand>(_queue);
        }

        public void AddCommand(ICommand command)
        {
            _queue.Enqueue(command);
        }

        public void ExecuteCommands()
        {
            while (_queue.Count > 0)
            {
                ICommand command = _queue.Dequeue();
                command.Execute();
            }
        }
        public void Clear()
        {
            _queue.Clear();
        }
     
        public void PrintCommands()
        {
            if (_queue.Count == 0)
            {
                Console.WriteLine("No commands in the queue.");
                return;
            }

            Console.WriteLine("Commands in the queue:");
            foreach (var command in _queue)
            {
                Console.WriteLine(command.ToString());
            }
        }
        public void Load(string filename , ICommandDeserializer commandDeserializer)
        {
            IEnumerable<ICommand> commands = commandDeserializer.Deserialize(filename);
            foreach (ICommand command in commands)
            {
                _queue.Enqueue(command);
            }
        }

    }
    public class ExportCommand : ICommand
    {
        private string filename;
        private string format;
        private Queue<ICommand> commandQueue;

        public ExportCommand(string filename, string format, Queue<ICommand> commandQueue)
        {
            this.filename = filename;
            this.format = format.ToLower();
            this.commandQueue = commandQueue;
        }
        public void UnExecute()
        {

        }
        public void Execute()
        {
            string filenamefull= $"{filename}.{format}";
            if (format == "xml")
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
                List<string> commandStrings = commandQueue.Select(command => command.ToString()).ToList();
                using (StreamWriter writer = new StreamWriter(filenamefull))
                {
                    
                    serializer.Serialize(writer, commandStrings);
                }
            }
            else 
            {
                using (StreamWriter writer = new StreamWriter(filenamefull))
                {
                    foreach (ICommand command in commandQueue)
                    {
                        writer.WriteLine(command.ToString());
                    }
                }
            }
        }

        public override string ToString()
        {
            return $"ExportCommand: exporting to {filename} in {format} format";
        }
    }
    public class DismissCommand : ICommand
    {
        private Queue<ICommand> commandQueue;

        public DismissCommand(Queue<ICommand> commandQueue)
        {
            this.commandQueue = commandQueue;
        }
        public void UnExecute()
        {

        }

        public void Execute()
        {
            commandQueue.Clear();
        }

        public override string ToString()
        {
            return $"DismissCommand: cleared command queue";
        }
    }

    
    public class CommandParser
    {
        private Forest forest;
        private Printer printer;

        public CommandParser(Forest forest, Printer printer)
        {
            this.forest = forest;
            this.printer = printer;
        }

        public ICommand Parse(string commandString)
        {
            // First, split the commandString by spaces to get the individual parts.
            string[] parts = commandString.Split(' ');
            string input = commandString.TrimEnd('\r', '\n');
            // The command type should be the first word in the commandString.
            string className = parts[1].ToLower().TrimEnd('\r', '\n');
            string commandType = parts[0].ToLower();
            // The rest of the commandString, if there is any, will be the parameters.
            

            switch (commandType)
            {
                case "add":
                    return new AddCommand(className, forest, input);
                case "delete":
                    return new DeleteCommand(className, forest, input);
                case "find":
                    return new FindCommand(className, forest, input, printer);
                case "list":
                    return new ListCommand(className, forest, input, printer);
                case "exit":
                    return new ExitCommand();
                case "edit":
                    return new EditCommand(className, forest , input);
                default:
                    throw new Exception($"Invalid command type: {commandType}");
            }
        }
    }




}
