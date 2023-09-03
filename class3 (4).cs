using class1;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace proj1oobjmain
{
    public class Author3
    {
        public int ID { get; set; }
        public string Author { get; set; }
 
    }

    public class Book3
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public int PageCount { get; set; }
    }

    

    public class Newspaper3
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
    }

    public class BoardGame3
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string Players { get; set; }

        public int Difficulty { get; set; }

        public string Authors { get; set; }

      
    }
    public class BajtpikBookstore3
    {
        public Dictionary<int, Book3> Books { get; set; }
        public Dictionary<int, Newspaper3> Newspapers { get; set; }
        public Dictionary<int, BoardGame3> BoardGames { get; set; }
        public Dictionary<int, Author3> Authors { get; set; }

        private int bookIndex;
        private int newspaperIndex;
        private int boardGameIndex;
        private int authorIndex;

        public BajtpikBookstore3()
        {
            Books = new Dictionary<int, Book3>();
            Newspapers = new Dictionary<int, Newspaper3>();
            BoardGames = new Dictionary<int, BoardGame3>();
            Authors = new Dictionary<int, Author3>();

            bookIndex = 1;
            newspaperIndex = 1;
            boardGameIndex = 1;
            authorIndex = 1;
        }

        public int AddBook(string title, string authors , int pageCount )
        {
            int id = bookIndex++;
            Books.Add(id, new Book3 { ID = id, Title = title, Authors = authors,  PageCount = pageCount });
            return id;
        }

        public int AddNewspaper(string title,int pageCount)
        {
            int id = newspaperIndex++;
            Newspapers.Add(id, new Newspaper3 { ID = id, Title = title,  PageCount = pageCount });
            return id;
        }

        public int AddBoardGame(string title, string players, int difficulty, string authors )
        {
            int id = boardGameIndex++;
            BoardGames.Add(id, new BoardGame3 { ID = id, Title = title, Players = players,  Difficulty = difficulty, Authors = authors });
            return id;
        }

        public int AddAuthor(string author)
        {
            int id = authorIndex++;
            Authors.Add(id, new Author3 { ID = id, Author = author });
            return id;
        }
        public Book3 GetBook(int bookID)
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

        public Newspaper3 GetNewspaper(int newspaperID)
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

        public BoardGame3 GetBoardGame(int boardGameID)
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

        public Author3 GetAuthor(int authorID)
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
        public void PrintAuthors()
        {
            foreach (var author in Authors)
            {
                Console.WriteLine(AuthorAdapter3.Adapt(author.Value));
            }
        }
        public void PrintBooks()
        {
            foreach(var book in Books)
            {
                Console.WriteLine(BookAdapter3.Adapt(book.Value , Authors));
            }
        }
        public void PrintBoardGames()
        {
            foreach (var boardgame in BoardGames)
            {
                Console.WriteLine(BoardGameAdapter3.Adapt(boardgame.Value , Authors));
            }
        }
        public void PrintNewsPapers()
        {
            foreach(var newspaper in Newspapers)
            {
                Console.WriteLine(NewsPaperAdapter3.Adapt(newspaper.Value));
            }
        }

    }
}
