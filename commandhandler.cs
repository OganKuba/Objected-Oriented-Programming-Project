using proj1oobjmain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace class1
{
    public class ListCommand : ICommand
    {
        private string className;
        private Forest forest;
        private string input;
        private Printer printer;
        public ListCommand(string className , Forest forest, string input, Printer printer)
        {
            this.className = className;
            this.forest = forest;
            this.input = input;
            this.printer = printer;
        }
        public override string ToString()
        {
            return input;
        }
        public void UnExecute()
        {

        }
        public void Execute()
        {
            switch (className.ToLower())
            {
                case "book":
                    IIterator<Book> bookIterator = forest.BookTree.GetInOrderIterator();
                    while (bookIterator.MoveNext())
                    {
                        Book book = bookIterator.Current;
                        printer.PrintBookV(book);
                    }
                    break;

                case "newspaper":
                    IIterator<Newspaper> newspaperIterator = forest.NewspaperTree.GetInOrderIterator();
                    while (newspaperIterator.MoveNext())
                    {
                        Newspaper newspaper = newspaperIterator.Current;
                        printer.PrintNewspaperV(newspaper);
                    }
                    break;

                case "author":
                    IIterator<Author> authorIterator = forest.AuthorTree.GetInOrderIterator();
                    while (authorIterator.MoveNext())
                    {
                        Author author = authorIterator.Current;
                        printer.PrintAuthorV(author);
                    }
                    break;

                case "boardgame":
                    IIterator<BoardGame> boardGameIterator = forest.BoardGameTree.GetInOrderIterator();
                    while (boardGameIterator.MoveNext())
                    {
                        BoardGame boardGame = boardGameIterator.Current;
                        printer.PrintBoardGameV(boardGame);
                    }
                    break;
                default:
                    Console.WriteLine("Unknown class: " + className);
                    break;
            }
        }
    }
    public class FindCommand : ICommand
    {
        private string className;
        private Forest forest;
        private string input;
        private Printer printer;
        public FindCommand(string className, Forest forest, string input, Printer printer)
        {
            this.className = className;
            this.forest = forest;
            this.input = input;
            this.printer = printer;
        }
        public override string ToString()
        {
            return input;
        }
        public void UnExecute()
        {

        }
        public void Execute()
        {
            Dictionary<string, (string comparison, string value)> filters = Parser.ParseCommandLine(input);
            switch (className.ToLower())
            {
                case "book":
                    IIterator<Book> bookIterator = forest.Find(forest.BookTree, filters);
                    while (bookIterator.MoveNext())
                    {
                        Book book = bookIterator.Current;
                        printer.PrintBookV(book);
                    }
                    break;

                case "newspaper":
                    IIterator<Newspaper> newspaperIterator = forest.Find(forest.NewspaperTree, filters);
                    while (newspaperIterator.MoveNext())
                    {
                        Newspaper newspaper = newspaperIterator.Current;
                        printer.PrintNewspaperV(newspaper);
                    }
                    break;

                case "author":
                    IIterator<Author> authorIterator = forest.Find(forest.AuthorTree, filters);
                    while (authorIterator.MoveNext())
                    {
                        Author author = authorIterator.Current;
                        printer.PrintAuthorV(author);
                    }
                    break;

                case "boardgame":
                    IIterator<BoardGame> boardGameIterator = forest.Find(forest.BoardGameTree, filters);
                    while (boardGameIterator.MoveNext())
                    {
                        BoardGame boardGame = boardGameIterator.Current;
                        printer.PrintBoardGameV(boardGame);
                    }
                    break;
                default:
                    Console.WriteLine("Unknown class: " + className);
                    break;
            }
        }
    }
    public class ExitCommand : ICommand
    {
    
        // Other necessary fields

        public void Execute()
        {
            Environment.Exit(0);
        }
        public override string ToString()
        {
            return "exit";
        }
        public void UnExecute()
        {

        }
    }
    public class AddCommand : ICommand
    {
        private string className;
        //private string baseOrSecondary;
        private Forest forest;
        private string input;
        private dynamic recentlyAddedObject;
        public AddCommand(string className, Forest forest, string input)
        {
            this.className = className;
            //this.baseOrSecondary = baseOrSecondary;
            this.forest = forest;
            this.input = input;
        }
        public override string ToString()
        {
            return input;
        }
        public void UnExecute()
        {
            switch (className.ToLower())
            {
                case "book":
                    forest.BookTree.Remove((Book)recentlyAddedObject);
                    break;
                case "newspaper":
                    forest.NewspaperTree.Remove((Newspaper)recentlyAddedObject);
                    break;
                case "boardgame":
                    forest.BoardGameTree.Remove((BoardGame)recentlyAddedObject);
                    break;
                case "author":
                    forest.AuthorTree.Remove((Author)recentlyAddedObject);
                    break;
            }
        }
        public void Execute()
        {
            AbstractBuilder builder;
            switch (className.ToLower())
            {
                case "book":
                    builder = new BookBuilder();
                    break;
                case "newspaper":
                    builder = new NewspaperBuilder();
                    break;
                case "boardgame":
                    builder = new BoardGameBuilder();
                    break;
                case "author":
                    builder = new AuthorBuilder();
                    break;
                default:
                    Console.WriteLine("Invalid class name.");
                    return;
            }
            string[] parts = input.Split(' ' , '\r' , '\n');
            string baseOrSecondary = parts[2];
           

            bool creationAborted = false;
            string[] inputLines = input.Split("\r" , '\n');
            int index = 1;
            while(index<inputLines.Length)
            {
                string line = inputLines[index++];
                if (line.Equals("DONE", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                if (line.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                {
                    creationAborted = true;
                    break;
                }

                string[] fieldParts = line.Split('=');
                if (fieldParts.Length == 2)
                {
                    string fieldName = fieldParts[0].Trim();
                    string fieldValue = fieldParts[1].Trim();
                    if (baseOrSecondary == "secondary" && fieldName == "id") continue;
                    if (baseOrSecondary == "base")
                    {
                        if (!builder.UpdateField(fieldName, fieldValue))
                        {
                            Console.WriteLine($"Error setting field '{fieldName}'.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid field input format.");
                }
            }

            if (!creationAborted)
            {
                object createdObject = builder.Build();
                recentlyAddedObject = createdObject;
                switch (className.ToLower())
                {
                    case "book":
                        forest.BookTree.Insert((Book)createdObject);
                        break;
                    case "newspaper":
                        forest.NewspaperTree.Insert((Newspaper)createdObject);
                        break;
                    case "boardgame":
                        forest.BoardGameTree.Insert((BoardGame)createdObject);
                        break;
                    case "author":
                        forest.AuthorTree.Insert((Author)createdObject);
                        break;
                }   
                Console.WriteLine("[Object created]");
            }
            else
            {
                Console.WriteLine("[Object creation abandoned]");
            }
        }
    }
    public class DeleteCommand : ICommand
    {
        private string className;
        private Forest forest;
        private string input;
        private dynamic recentlydeletedObject;
        public DeleteCommand(string className, Forest forest, string input)
        {
            this.className = className;
            this.forest = forest;
            this.input = input;
            
        }
        public override string ToString()
        {
            return input;
        }
        public void UnExecute()
        {
            switch (className.ToLower())
            {
                case "book":
                    forest.BookTree.Insert((Book)recentlydeletedObject);
                    break;
                case "newspaper":
                    forest.NewspaperTree.Insert((Newspaper)recentlydeletedObject);
                    break;
                case "boardgame":
                    forest.BoardGameTree.Insert((BoardGame)recentlydeletedObject);
                    break;
                case "author":
                    forest.AuthorTree.Insert((Author)recentlydeletedObject);
                    break;
            }
        }
        public void Execute()
        {
            Dictionary<string, (string comparison, string value)> filters = Parser.ParseCommandLine(input);

            switch (className.ToLower())
            {
                case "book":
                    IIterator<Book> iteratorB = forest.Find(forest.BookTree, filters);
                    while (iteratorB.MoveNext())
                    {
                        recentlydeletedObject= iteratorB.Current;
                        forest.BookTree.Remove(iteratorB.Current);
                        break;
                    }
                    break;
                case "newspaper":
                    IIterator<Newspaper> iteratorN = forest.Find(forest.NewspaperTree, filters);
                    while (iteratorN.MoveNext())
                    {
                        recentlydeletedObject = iteratorN.Current;
                        forest.NewspaperTree.Remove(iteratorN.Current);
                        break;
                    }
                    break;
                case "boardgame":
                    IIterator<BoardGame> iteratorG = forest.Find(forest.BoardGameTree, filters);
                    while (iteratorG.MoveNext())
                    {
                        recentlydeletedObject = iteratorG.Current;
                        forest.BoardGameTree.Remove(iteratorG.Current);
                        break;
                    }
                    break;
                case "author":
                    IIterator<Author> iteratorA = forest.Find(forest.AuthorTree, filters);
                    while (iteratorA.MoveNext())
                    {
                        recentlydeletedObject = iteratorA.Current;
                        forest.AuthorTree.Remove(iteratorA.Current);
                        break;
                    }
                    break;
            }
            
        }
    }
    public class EditCommand : ICommand
    {
        private string input;
        private Forest forest;
        private string className;
        private dynamic recentlyEditedObject1;
        private dynamic recentlyEditedObject2;
        public EditCommand(string classname , Forest forest , string input)
        {
            this.input = input;
            this.forest = forest;
            this.className = classname;
        }
        public void UnExecute()
        {
            switch (className.ToLower())
            {
                case "book":
                    forest.BookTree.Remove((Book)recentlyEditedObject1);
                    forest.BookTree.Insert((Book)recentlyEditedObject2);
                    break;
                case "newspaper":
                    forest.NewspaperTree.Remove((Newspaper)recentlyEditedObject1);
                    forest.NewspaperTree.Insert((Newspaper)recentlyEditedObject2);
                    break;
                case "boardgame":
                    forest.BoardGameTree.Remove((BoardGame)recentlyEditedObject1);
                    forest.BoardGameTree.Insert((BoardGame)recentlyEditedObject2);
                    break;
                case "author":
                    forest.AuthorTree.Remove((Author)recentlyEditedObject1);
                    forest.AuthorTree.Insert((Author)recentlyEditedObject2);
                    break;
            }
        }

        public void Execute()
        {
            string infil;
            using (StringReader reader = new StringReader(input))
            {
                infil = reader.ReadLine();
            }
            
            Dictionary<string, (string comparison, string value)> filters = Parser.ParseCommandLine(infil);
            Book foundB = new Book();
            Newspaper foundN = new Newspaper();
            BoardGame foundG = new BoardGame();
            Author foundA = new Author();
            Book foundBook = new Book();
            Newspaper foundNewspaper = new Newspaper();
            BoardGame foundBoardGame = new BoardGame();
            Author foundAuthor = new Author();

            switch (className.ToLower())
            {
                case "book":
                    IIterator<Book> iteratorB = forest.Find(forest.BookTree, filters);
                    while (iteratorB.MoveNext())
                    {
                        foundB = iteratorB.Current;
                        foundBook = iteratorB.Current.DeepClone();
                        break;

                    }
                    break;
                case "newspaper":
                    IIterator<Newspaper> iteratorN = forest.Find(forest.NewspaperTree, filters);
                    while (iteratorN.MoveNext())
                    {
                        foundN = iteratorN.Current;
                        foundNewspaper = iteratorN.Current.DeepClone();
                        break;
                    }
                    break;
                case "boardgame":
                    IIterator<BoardGame> iteratorG = forest.Find(forest.BoardGameTree, filters);
                    while (iteratorG.MoveNext())
                    {
                        foundG = iteratorG.Current;
                        foundBoardGame = iteratorG.Current.DeepClone();
                        break;

                    }
                    break;
                case "author":
                    IIterator<Author> iteratorA = forest.Find(forest.AuthorTree, filters);
                    while (iteratorA.MoveNext())
                    {
                        foundA = iteratorA.Current;
                        foundAuthor = iteratorA.Current.DeepClone();
                        break;

                    }
                    break;
            }
            bool creationAborted = false;
            string[] lines = input.Split('\n' );
            int index = 1;
            while (index<lines.Length)
            {
                string line = lines[index++];
                
                if (line.Equals("DONE", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                if (line.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                {
                    creationAborted = true;
                    break;
                }
                string[] fieldParts = line.Split('=');
                if (fieldParts.Length == 2)
                {

                    string fieldName = fieldParts[0].Trim();
                    string fieldValue = fieldParts[1].Trim();
                    switch (className.ToLower())
                    {
                        case "book":
                            foundB.UpdateField(fieldName, fieldValue);
                            break;
                        case "newspaper":
                            foundN.UpdateField(fieldName, fieldValue);
                            break;
                        case "boardgame":
                            foundG.UpdateField(fieldName, fieldValue);
                            break;
                        case "author":
                            foundA.UpdateField(fieldName, fieldValue);
                            break;
                    }

                }
                else
                {
                    Console.WriteLine("Invalid field input format.");
                }
            }

            if (!creationAborted)
            {
                switch (className.ToLower())
                {
                    case "book":
                        recentlyEditedObject1 = (Book)foundB;
                        recentlyEditedObject2 = (Book)foundBook;
                        forest.BookTree.UpdateValue(foundBook, foundB);
                        break;
                    case "newspaper":
                        recentlyEditedObject1 = (Newspaper)foundN;
                        recentlyEditedObject2 = (Newspaper)foundNewspaper;
                        forest.NewspaperTree.UpdateValue(foundNewspaper, foundN);
                        break;
                    case "boardgame":
                        recentlyEditedObject1 = (BoardGame)foundG;
                        recentlyEditedObject2 = (BoardGame)foundBoardGame;
                        forest.BoardGameTree.UpdateValue(foundBoardGame, foundG);
                        break;
                    case "author":
                        recentlyEditedObject1 = (Author)foundA;
                        recentlyEditedObject2 = (Author)foundAuthor;
                        forest.AuthorTree.UpdateValue(foundAuthor, foundA);
                        break;
                }
                Console.WriteLine("[Object created]");
            }
            else
            {
                Console.WriteLine("[Object creation abandoned]");
            }

        }

        public override string ToString()
        {
            return input;
        }
    }
    public class LoadCommand : ICommand
    {
        private string fileName;
        private string input;
        private ICommandDeserializer commandDeserializer;
        private CommandHistory history;
        public LoadCommand(string fileName, string input, ICommandDeserializer commandDeserializer , CommandHistory history)

        {
            this.commandDeserializer = commandDeserializer;
            this.input = input;
            this.fileName = fileName;
            this.history = history;
        }
        public override string ToString()
        {
            return input;
        }
        public void UnExecute()
        {

        }
        public void Execute()
        {
            IEnumerable<ICommand> commands = commandDeserializer.Deserialize(fileName);
            foreach (ICommand command in commands)
            {
                history.Add(command);
            }
        }
    }




        public class Parser
    {
       public static string GetAvailableFields(string className, string representation)
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
                    value = parts[i + 2].Substring(1, parts[i + 2].Length - 2);
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
    }



}
