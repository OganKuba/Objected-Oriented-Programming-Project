using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class1
{
    public class Author1
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BirthYear { get; set; }
        public string Nickname { get; set; }
    }

    public class Book1
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public List<int> AuthorIDs { get; set; }
        public int Year { get; set; }
        public int PageCount { get; set; }
        //public string Genre { get; set; }
    }

    public class Newspaper1
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int PageCount { get; set; }
    }

    public class BoardGame1
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int Difficulty { get; set; }
        public List<int> AuthorIDs { get; set; }
    }

    public class BajtpikBookstore1 
    {
        public Dictionary<int, Book1> Books { get; set; }
        public Dictionary<int, Newspaper1> Newspapers { get; set; }
        public Dictionary<int, BoardGame1> BoardGames { get; set; }
        public Dictionary<int, Author1> Authors { get; set; }

        private int bookIndex;
        private int newspaperIndex;
        private int boardGameIndex;
        private int authorIndex;

        public BajtpikBookstore1()
        {
            Books = new Dictionary<int, Book1>();
            Newspapers = new Dictionary<int, Newspaper1>();
            BoardGames = new Dictionary<int, BoardGame1>();
            Authors = new Dictionary<int, Author1>();

            bookIndex = 1;
            newspaperIndex = 1;
            boardGameIndex = 1;
            authorIndex = 1;
        }

        public int AddBook(string title, List<int> authorIDs, int year, int pageCount)
        {
            int id = bookIndex++;
            Books.Add(id, new Book1 { ID = id, Title = title, AuthorIDs = authorIDs, Year = year, PageCount = pageCount });
            return id;
        }

        public int AddNewspaper(string title, int year, int pageCount)
        {
            int id = newspaperIndex++;
            Newspapers.Add(id, new Newspaper1 { ID = id, Title = title, Year = year, PageCount = pageCount });
            return id;
        }

        public int AddBoardGame(string title, int minPlayers, int maxPlayers, int difficulty, List<int> authorIDs)
        {
            int id = boardGameIndex++;
            BoardGames.Add(id, new BoardGame1 { ID = id, Title = title, MinPlayers = minPlayers, MaxPlayers = maxPlayers, Difficulty = difficulty, AuthorIDs = authorIDs });
            return id;
        }

        public int AddAuthor(string name, string surname, int birthYear, string nickname = null)
        {
            int id = authorIndex++;
            Authors.Add(id, new Author1 { ID = id, Name = name, Surname = surname, BirthYear = birthYear, Nickname = nickname });
            return id;
        }
        public Book1 GetBook(int bookID)
        {
            if (Books.ContainsKey(bookID))
            {
                return Books[bookID];
            }
            else
            {
                return null;
            }
        }

        public Newspaper1 GetNewspaper(int newspaperID)
        {
            if (Newspapers.ContainsKey(newspaperID))
            {
                return Newspapers[newspaperID];
            }
            else
            {
                return null;
            }
        }

        public BoardGame1 GetBoardGame(int boardGameID)
        {
            if (BoardGames.ContainsKey(boardGameID))
            {
                return BoardGames[boardGameID];
            }
            else
            {
                return null;
            }
        }

        public Author1 GetAuthor(int authorID)
        {
            if (Authors.ContainsKey(authorID))
            {
                return Authors[authorID];
            }
            else
            {
                return null;
            }
        }
        public void PrintAllBooks()
        {
            foreach (var book in Books.Values)
            {
                string S = "";
                foreach (var authorId in book.AuthorIDs)
                {
                    S = string.Join(",", S, GetAuthor(authorId).Name, GetAuthor(authorId).Surname, GetAuthor(authorId).Nickname);
                }
                Console.WriteLine("{0} ({1}), Authors{2} , PrageCount: {3}", book.Title, book.Year, S, book.PageCount);
            }
        }

        public void PrintAllNewspapers()
        {
            foreach (var newspaper in Newspapers.Values)
            {

                Console.WriteLine("{0} ({1}), Pages: {2}", newspaper.Title, newspaper.Year, newspaper.PageCount);
            }
        }

        public void PrintAllBoardGames()
        {
            foreach (var boardGame in BoardGames.Values)
            {

                string S = "";
                foreach (var authorId in boardGame.AuthorIDs)
                {
                    string nickname = GetAuthor(authorId).Nickname == null ? "" : $"({GetAuthor(authorId).Nickname})";
                    S = string.Join(",", S, GetAuthor(authorId).Name, GetAuthor(authorId).Surname, GetAuthor(authorId).Nickname == null ? "" : $"({GetAuthor(authorId).Nickname})");
                }
                Console.WriteLine("{0} ({1}-{2}), Difficulty: {3}, Authors: {4}", boardGame.Title, boardGame.MinPlayers, boardGame.MaxPlayers, boardGame.Difficulty, S);

            }
        }

        public void PrintAllAuthors()
        {
            foreach (var author in Authors.Values)
            {
                string nickname = author.Nickname == null ? "" : $"({author.Nickname})";
                Console.WriteLine($"{author.Name} {author.Surname} - Birth Year: {author.BirthYear} {nickname}");
            }
        }
        public void Print()
        {
            PrintAllBooks();
            PrintAllNewspapers();
            PrintAllBoardGames();
        }
    }
}
