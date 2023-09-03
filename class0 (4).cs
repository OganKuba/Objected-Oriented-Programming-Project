using proj1oobjmain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace class1
{
    public interface ICommandProcessor
    {
        List<T> Find<T>(List<T> items, Dictionary<string, (string comparison, string value)> filters) where T : class;
    }
    public interface IUpdatable
    {
        bool UpdateField(string fieldName, string fieldValue);
    }

    public interface IFilterable
    {
        bool MatchesFilters(Dictionary<string, (string comparison, string value)> filters);
    }
    public interface ICollectible
    {
        // Common properties or methods for Book, Newspaper, BoardGame, and Author
    }
    public class Author : IFilterable, ICollectible , IComparable<Author> , IUpdatable
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BirthYear { get; set; }
        public string Nickname { get; set; }
        public Author DeepClone()
        {
            return new Author
            {
                Name = this.Name,
                Surname = this.Surname,
                BirthYear = this.BirthYear,
                Nickname = this.Nickname,
            };
        }
        public int CompareTo(Author other)
        {
            return string.Compare(this.Name, other.Name);
        }
        public override string ToString()
        {
            string nickname = Nickname == null ? "" : $"({Nickname})";
            return $"{Name} {Surname} - Birth Year: {BirthYear} {nickname}";
            
        }
        public bool MatchesFilters(Dictionary<string, (string comparison, string value)> filters)
        {
            foreach (var filter in filters)
            {
                string propertyName = filter.Key;
                string comparison = filter.Value.comparison;
                string value = filter.Value.value;

                // Example filter: filtering by name

                if (propertyName.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    if (!Forest.Compare(Name, value, comparison)) return false;
                }
                else if (propertyName.Equals("Surname", StringComparison.OrdinalIgnoreCase))
                {
                    if (!Forest.Compare(Surname, value, comparison)) return false;
                }
                else if (propertyName.Equals("BirthYear", StringComparison.OrdinalIgnoreCase))
                {
                    int intValue;
                    if (!int.TryParse(value, out intValue))
                    {
                        Console.WriteLine("Unable to parse value.");
                        return false;
                    }
                    if (!Forest.Compare(BirthYear, intValue, comparison)) return false;
                }
                else if (propertyName.Equals("NickName", StringComparison.OrdinalIgnoreCase))
                {
                    if (!Forest.Compare(Nickname, value, comparison)) return false;
                }
                else
                {
                    Console.WriteLine($"Invalid property name '{propertyName}'");
                    return false;
                }
                // Add other property filter checks here
            }
            return true;
        }
        public bool UpdateField(string fieldName, string fieldValue)
        {
            switch (fieldName.ToLower())
            {
                case "name":
                    Name = fieldValue;
                    break;
                case "surname":
                    Surname = fieldValue;
                    break;
                case "year":
                    if (int.TryParse(fieldValue, out int year))
                    {
                        BirthYear= year;
                        break;
                    }
                    return false;
                default:
                    return false;
            }
            return true;
        }

    }

    public class Book : IFilterable, ICollectible , IComparable<Book> , IUpdatable
    {
        public string Title { get; set; }
        public List<Author> Authors { get; set; }
        public int Year { get; set; }
        public int PageCount { get; set; }

        public int CompareTo(Book other)
        {
            return string.Compare(this.Title, other.Title);
        }
        public Book DeepClone()
        {
            return new Book
            {
                Title = this.Title,
                Year = this.Year,
                PageCount = this.PageCount,
                Authors = this.Authors
            };
        }
        public override string ToString()
        {
            string authorNames = string.Join(", ", Authors.Select(author => $"{author.Name} {author.Surname}"));
            return $"{Title} ({Year}), Authors: {authorNames} , PageCount: {PageCount}";

        }
        public bool MatchesFilters(Dictionary<string, (string comparison, string value)> filters)
        {
            foreach (var filter in filters)
            {
                string propertyName = filter.Key;
                string comparison = filter.Value.comparison;
                string value = filter.Value.value;
                bool validproperty = true;
                if (propertyName.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    if (!Forest.Compare(Title, value, comparison)) return false;
                }
                else if (propertyName.Equals("Year", StringComparison.OrdinalIgnoreCase))
                {
                    int intValue;
                    if (!int.TryParse(value, out intValue))
                    {
                        Console.WriteLine("Unable to parse value.");
                        return false;
                    }
                    if (!Forest.Compare(Year, intValue, comparison)) return false;
                }
                else if (propertyName.Equals("PageCount", StringComparison.OrdinalIgnoreCase))
                {
                    int intValue;
                    if (!int.TryParse(value, out intValue))
                    {
                        Console.WriteLine("Unable to parse value.");
                        return false;
                    }
                    if (!Forest.Compare(PageCount, intValue, comparison)) return false;
                }
                else
                {
                    Console.WriteLine($"Invalid property name '{propertyName}'");
                    return false;
                }
                // Add other property filter checks here
            }
            return true;
        }
        public bool UpdateField(string fieldName, string fieldValue)
        {
            switch (fieldName.ToLower())
            {
                case "title":
                    Title = fieldValue;
                    break;
                case "year":
                    if (int.TryParse(fieldValue, out int year))
                    {
                        Year = year;
                        break;
                    }
                    return false;
                case "pagecount":
                    if (int.TryParse(fieldValue, out int pagecount))
                    {
                        PageCount = pagecount;
                        break;
                    }
                    return false;
                default:
                    return false;
            }
            return true;
        }


    }

    public class Newspaper : IFilterable, ICollectible, IComparable<Newspaper> , IUpdatable
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public int PageCount { get; set; }

        public int CompareTo(Newspaper other)
        {
            return string.Compare(this.Title, other.Title);
        }
        public Newspaper DeepClone()
        {
            return new Newspaper
            {
                Title = this.Title,
                Year = this.Year,
                PageCount = this.PageCount
            };
        }
        public override string ToString()
        {
            return $"{Title} ({Year}), Pages: {PageCount}";
        }
        public bool MatchesFilters(Dictionary<string, (string comparison, string value)> filters)
        {
            foreach (var filter in filters)
            {
                string propertyName = filter.Key;
                string comparison = filter.Value.comparison;
                string value = filter.Value.value;

                if (propertyName.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    if (!Forest.Compare(Title, value, comparison)) return false;
                }
                
                else if (propertyName.Equals("Year", StringComparison.OrdinalIgnoreCase))
                {
                    int intValue;
                    if (!int.TryParse(value, out intValue))
                    {
                        Console.WriteLine("Unable to parse value.");
                        return false;
                    }
                    if (!Forest.Compare(Year, intValue, comparison)) return false;
                }
                else if (propertyName.Equals("PageCount", StringComparison.OrdinalIgnoreCase))
                {
                    int intValue;
                    if (!int.TryParse(value, out intValue))
                    {
                        Console.WriteLine("Unable to parse value.");
                        return false;
                    }
                    if (!Forest.Compare(PageCount, intValue, comparison)) return false;
                }
                else
                {
                    Console.WriteLine($"Invalid property name '{propertyName}'");
                    return false;
                }
                // Add other property filter checks here
            }
            return true;
        }
        public bool UpdateField(string fieldName, string fieldValue)
        {
            switch (fieldName.ToLower())
            {
                case "title":
                    Title = fieldValue;
                    break;
                case "year":
                    if (int.TryParse(fieldValue, out int year))
                    {
                        Year = year;
                        break;
                    }
                    return false;
                case "pagecount":
                    if (int.TryParse(fieldValue, out int pagecount))
                    {
                        PageCount = pagecount;
                        break;
                    }
                    return false;
                default:
                    return false;
            }
            return true;
        }


    }

    public class BoardGame : IFilterable, ICollectible, IComparable<BoardGame>
    {
        public string Title { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int Difficulty { get; set; }
        public List<Author> Authors { get; set; }

        public int CompareTo(BoardGame other)
        {
            return string.Compare(this.Title, other.Title);
        }
        public BoardGame DeepClone()
        {
            return new BoardGame
            {
                Title = this.Title,
                MinPlayers = this.MinPlayers,
                MaxPlayers = this.MaxPlayers,
                Difficulty = this.Difficulty,
                Authors = this.Authors
            };
        }
        public override string ToString()
        {
            string authorNames = string.Join(", ", Authors.Select(author => $"{author.Name} {author.Surname}"));
            return $"{Title} ({MinPlayers}-{MaxPlayers}), Difficulty: {Difficulty}, Authors: {authorNames}";
        }
        public bool MatchesFilters(Dictionary<string, (string comparison, string value)> filters)
        {
            foreach (var filter in filters)
            {
                string propertyName = filter.Key;
                string comparison = filter.Value.comparison;
                string value = filter.Value.value;

                if (propertyName.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    if (!Forest.Compare(Title, value, comparison)) return false;
                }
                else if (propertyName.Equals("Difficulty", StringComparison.OrdinalIgnoreCase))
                {
                    int intValue;
                    if (!int.TryParse(value, out intValue))
                    {
                        Console.WriteLine("Unable to parse value.");
                        return false;
                    }
                    if (!Forest.Compare(Difficulty, intValue, comparison)) return false;
                }
                else if (propertyName.Equals("MinPlayers", StringComparison.OrdinalIgnoreCase))
                {
                    int intValue;
                    if (!int.TryParse(value, out intValue))
                    {
                        Console.WriteLine("Unable to parse value.");
                        return false;
                    }
                    if (!Forest.Compare(MinPlayers, intValue, comparison)) return false;
                }
                else if (propertyName.Equals("MaxPlayers", StringComparison.OrdinalIgnoreCase))
                {
                    int intValue;
                    if (!int.TryParse(value, out intValue))
                    {
                        Console.WriteLine("Unable to parse value.");
                        return false;
                    }
                    if (!Forest.Compare(MaxPlayers, intValue, comparison)) return false;
                }
                else
                {
                    Console.WriteLine($"Invalid property name '{propertyName}'");
                    return false;
                }
                // Add other property filter checks here
            }
            return true;
        }
        public bool UpdateField(string fieldName, string fieldValue)
        {
            switch (fieldName.ToLower())
            {
                case "title":
                    Title = fieldValue;
                    break;
                case "difficulty":
                    if (int.TryParse(fieldValue, out int dif))
                    {
                        Difficulty = dif;
                        break;
                    }
                    return false;
                case "minplayers":
                    if (int.TryParse(fieldValue, out int mini))
                    {
                        MinPlayers = mini;
                        break;
                    }
                    return false;
                case "maxplayers":
                    if (int.TryParse(fieldValue, out int maxi))
                    {
                        MaxPlayers = maxi;
                        break;
                    }
                    return false;
                default:
                    return false;
            }
            return true;
        }

    }


    public class BajtpikBookstore : ICommandProcessor
    {
       

        public static bool Compare<T>(T left, T right, string comparison) where T : IComparable
        {
            switch (comparison)
            {
                case "=":
                    return left.CompareTo(right) == 0;
                case "<":
                    return left.CompareTo(right) < 0;
                case ">":
                    return left.CompareTo(right) > 0;
                default:
                    Console.WriteLine("Invalid comparison operator.");
                    return false;
            }
        }

        public  List<T> Find<T>(List<T> items, Dictionary<string, (string comparison, string value)> filters) where T : class
        {
            List<T> result = new List<T>();

            foreach (T item in items)
            {
                bool isMatch = true;

                foreach (var filter in filters)
                {
                    string propertyName = filter.Key;
                    string comparison = filter.Value.comparison;
                    string value = filter.Value.value;

                    PropertyInfo propertyInfo = typeof(T).GetProperty(propertyName);
                    if (propertyInfo == null)
                    {
                        Console.WriteLine("Invalid property name.");
                        return null;
                    }

                    object propertyValue = propertyInfo.GetValue(item);

                    if (propertyValue is IComparable comparable)
                    {
                        Type propertyType = propertyValue.GetType();
                        object convertedValue;

                        try
                        {
                            convertedValue = Convert.ChangeType(value, propertyType);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Unable to parse value.");
                            return null;
                        }

                        if (!Compare(comparable, (IComparable)convertedValue, comparison)) isMatch = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid property type.");
                        return null;
                    }

                    if (!isMatch) break;
                }

                if (isMatch) result.Add(item);
            }

            return result;
        }
       










        public List<Book> Books { get; set; }
        public List<Newspaper> Newspapers { get; set; }
        public List<BoardGame> BoardGames { get; set; }
        public List<Author> Authors { get; set; }

        public BajtpikBookstore()
        {
            Books = new List<Book>();
            Newspapers = new List<Newspaper>();
            BoardGames = new List<BoardGame>();
            Authors = new List<Author>();
        }
        public void PrintBooks()
        {
            Console.WriteLine("Books:");
            foreach (var book in Books)
            {
                //Console.WriteLine("{0} ({1}), Authors{2} , PrageCount: {3}", book.Title, book.Year, string.Join(", ", book.Authors.), book.PageCount);
                PrintBook(book);
            }
        }
        public void PrintNewspapers()
        {
            Console.WriteLine("Newspapers:");
            foreach (var newspaper in Newspapers)
            {
                PrintNewspaper(newspaper);
            }
        }
        public void PrintBoardGames()
        {
            Console.WriteLine("Board Games:");
            foreach (var boardGame in BoardGames)
            {
                PrintBoardGame(boardGame);
                // Console.WriteLine("{0} ({1}-{2}), Difficulty: {3}, Authors: {4}", boardGame.Title, boardGame.MinPlayers, boardGame.MaxPlayers, boardGame.Difficulty, string.Join(", ", boardGame.Authors));
            }
        }
        public void PrintAuthors()
        {
            Console.WriteLine("Authors:");
            foreach (var author in Authors)
            {
                //string nickname = author.Nickname == null ? "" : $"({author.Nickname})";
               // Console.WriteLine($"{author.Name} {author.Surname} - Birth Year: {author.BirthYear} {nickname}");
               PrintAuthor(author);
            }
        }
        public void Print()
        {
            PrintBooks();
            PrintNewspapers();
            PrintBoardGames();
        }
        public void PrintBook(Book book)

        {
            string authorNames = "No authors";
            if (book.Authors != null)
            {
                authorNames = string.Join(", ", book.Authors.Select(author => $"{author.Name} {author.Surname}"));
            }

            Console.WriteLine("{0} ({1}), Authors: {2} , PageCount: {3}", book.Title, book.Year, authorNames, book.PageCount);
        }
        public void PrintNewspaper(Newspaper newspaper)
        {
            Console.WriteLine("{0} ({1}), Pages: {2}", newspaper.Title, newspaper.Year, newspaper.PageCount);
        }
        public void PrintBoardGame(BoardGame boardGame)
        {
            string authorNames = "No authors";
            if (boardGame.Authors != null)
            {
                authorNames = string.Join(", ", boardGame.Authors.Select(author => $"{author.Name} {author.Surname}"));
            }
            //string authorNames = string.Join(", ", boardGame.Authors.Select(author => $"{author.Name} {author.Surname}"));
            Console.WriteLine("{0} ({1}-{2}), Difficulty: {3}, Authors: {4}", boardGame.Title, boardGame.MinPlayers, boardGame.MaxPlayers, boardGame.Difficulty, string.Join(", ", authorNames));
            
        }
        public void PrintAuthor(Author author)
        {
            
            string nickname = author.Nickname == null ? "" : $"({author.Nickname})";
            Console.WriteLine($"{author.Name} {author.Surname} - Birth Year: {author.BirthYear} {nickname}");
            
        }
    }
}
