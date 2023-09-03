using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class1
{
    public class AuthorAdapter
    {
        public static Author Adapt(Author1 author1)
        {
            return new Author
            {
                Name = author1.Name,
                Surname = author1.Surname,
                BirthYear = author1.BirthYear,
                Nickname = author1.Nickname
            };
        }

    }
    public static class BookAdapter
    {

        public static Book ToBook(Book1 book1, Dictionary<int, Author1> authors)
        {
            List<Author> bookAuthors = new List<Author>();
            foreach (var authorID in book1.AuthorIDs)
            {
                if (authors.ContainsKey(authorID))
                {
                    bookAuthors.Add(AuthorAdapter.Adapt(authors[authorID]));
                }
            }

            return new Book
            {
                Title = book1.Title,
                Authors = bookAuthors,
                Year = book1.Year,
                PageCount = book1.PageCount
            };
        }
    }

        public static class NewsPaperAdapter
        {
            public static Newspaper ToNewspaper(Newspaper1 newspaper1)
            {
                return new Newspaper
                {
                    Title = newspaper1.Title,
                    Year = newspaper1.Year,
                    PageCount = newspaper1.PageCount
                };
            }
        }
    public static class BoardgameAdapter
    {
        public static BoardGame ToBoardGame(BoardGame1 boardgame1, Dictionary<int, Author1> authors)
        {
            List<Author> boardgameAuthors = new List<Author>();
            foreach (var authorID in boardgame1.AuthorIDs)
            {
                if (authors.ContainsKey(authorID))
                {
                    boardgameAuthors.Add(AuthorAdapter.Adapt(authors[authorID]));
                }
            }

            return new BoardGame
            {
                Title = boardgame1.Title,
                Authors = boardgameAuthors,
                MinPlayers = boardgame1.MinPlayers,
                MaxPlayers = boardgame1.MaxPlayers,
                Difficulty = boardgame1.Difficulty
            };
        }
    }
    public static class StoreAdapter
    {
        public static BajtpikBookstore ToStore(BajtpikBookstore1 store1)
        {
            BajtpikBookstore store = new BajtpikBookstore();
            foreach(var author in store1.Authors)
            {
                store.Authors.Add(AuthorAdapter.Adapt(author.Value));
            }
            foreach(var book in store1.Books)
            {
                store.Books.Add(BookAdapter.ToBook(book.Value , store1.Authors));
            }
            foreach(var newspaper in store1.Newspapers)
            {
                store.Newspapers.Add(NewsPaperAdapter.ToNewspaper(newspaper.Value));
            }
            foreach (var boardgame in store1.BoardGames)
            {
                store.BoardGames.Add(BoardgameAdapter.ToBoardGame(boardgame.Value , store1.Authors));
            }
            return store;
        }
    }







}
