using proj1oobjmain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace class1
{
    public static class Decode
    {
        public static string HTML(string input)
        {
            int startIndex = input.IndexOf("<") + 1;
            int endIndex = input.IndexOf(">");
            string outp = input.Substring(startIndex, endIndex - startIndex);
            return outp;
        }
    }
        public class AuthorAdapter3
    {
       
        public static Author Adapt(Author3 author3)
        {
            
            string[] parts = author3.Author.Split(new[] { "+", "$" }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 3 || parts.Length > 4)
            {
                throw new ArgumentException("Invalid author string format.");
            }
            //string name = parts[0];
            //string surname = parts[1];
            //int birthYear = int.Parse(parts[2]);
            //string nickname = parts[3];
            string name = Decode.HTML(parts[0]);
            string surname = Decode.HTML(parts[1]);
            string year = Decode.HTML(parts[2]);
            int birthYear = int.Parse(year);

            string nickname = null;
            if (parts.Length == 4)
            {
                nickname = Decode.HTML(parts[3]);
            }

            return new Author
            {
                Name = name,
                Surname = surname,
                BirthYear = birthYear,
                Nickname = nickname
            };

        }

    }
    public class BookAdapter3
    {
       
        public static Book Adapt(Book3 book3 , Dictionary<int, Author3> authors)
        {
           int i0 = book3.Title.IndexOf("<")+1;
           int i1 = book3.Title.IndexOf(">");
           string title  = book3.Title.Substring(i0, i1- i0);
           int j0 = book3.Title.IndexOf("<", i1) + 1;
           int j1 = book3.Title.IndexOf(">", j0);
           string yearString = book3.Title.Substring(j0, j1 - j0);
           int year = int.Parse(yearString);
            List<Author> authorRefs = new List<Author>();
            foreach (string authorIdString in book3.Authors.Split(','))
            {
                if (int.TryParse(authorIdString, out int authorId))
                {
                    if (authors.ContainsKey(authorId))
                    {
                        authorRefs.Add(AuthorAdapter3.Adapt(authors[authorId]));
                    }
                }
            }
            return new Book
            {
                Title = title,
                Authors = authorRefs,
                Year = year,
                PageCount = book3.PageCount
            };
        }

    }
    public class NewsPaperAdapter3
    {

        public static Newspaper Adapt(Newspaper3 newspaper3)
        {
            int i0 = newspaper3.Title.IndexOf("<") + 1;
            int i1 = newspaper3.Title.IndexOf(">");
            string title = newspaper3.Title.Substring(i0, i1 - i0);
            int j0 = newspaper3.Title.IndexOf("<", i1) + 1;
            int j1 = newspaper3.Title.IndexOf(">", j0);
            string yearString = newspaper3.Title.Substring(j0, j1 - j0);
            int year = int.Parse(yearString);
            return new Newspaper
            {
                Title = title,
                Year = year,
                PageCount = newspaper3.PageCount
            };
        }

    }
    public class BoardGameAdapter3
    {

        public static BoardGame Adapt(BoardGame3 boardGame3, Dictionary<int, Author3> authors)
        {
            List<Author> authorRefs = new List<Author>();
            foreach (string authorIdString in boardGame3.Authors.Split(','))
            {
                if (int.TryParse(authorIdString, out int authorId))
                {
                    if (authors.ContainsKey(authorId))
                    {
                        authorRefs.Add(AuthorAdapter3.Adapt(authors[authorId]));
                    }
                }
            }
            string[] parts = boardGame3.Players.Split(new[] {"/"}, StringSplitOptions.RemoveEmptyEntries);
            string Min = Decode.HTML(parts[0]);
            string Max = Decode.HTML(parts[1]);
            return new BoardGame
            {
                Title = boardGame3.Title,
                MinPlayers= int.Parse(Min),
                MaxPlayers = int.Parse(Max),
                Difficulty = boardGame3.Difficulty,
                Authors = authorRefs


            };
           
        }

    }
    public static class store3adapter
    {
        public static BajtpikBookstore ToStore(BajtpikBookstore3 store3)
        {
            BajtpikBookstore store = new BajtpikBookstore();
            foreach (var author in store3.Authors)
            {
                store.Authors.Add(AuthorAdapter3.Adapt(author.Value));
            }
            foreach (var book in store3.Books)
            {
                store.Books.Add(BookAdapter3.Adapt(book.Value, store3.Authors));
            }
            foreach (var newspaper in store3.Newspapers)
            {
                store.Newspapers.Add(NewsPaperAdapter3.Adapt(newspaper.Value));
            }
            foreach (var boardgame in store3.BoardGames)
            {
                store.BoardGames.Add(BoardGameAdapter3.Adapt(boardgame.Value, store3.Authors));
            }
            return store;
        }
    }

}
