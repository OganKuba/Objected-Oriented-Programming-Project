using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class1
{
    public interface IBuilder<T>
    {
        T Build();
    }
    public abstract class AbstractBuilder : IBuilder<ICollectible>
    {
        protected Dictionary<string, Action<string>> FieldSetters = new Dictionary<string, Action<string>>();

        public abstract ICollectible Build();

        public bool UpdateField(string fieldName, string fieldValue)
        {
            if (FieldSetters.TryGetValue(fieldName.ToLower(), out Action<string> setter))
            {
                setter(fieldValue);
                return true;
            }
            return false;
        }
    }
    public class BuilderFactory
    {
        public static IBuilder<ICollectible> GetBuilder(string type)
        {
            switch (type)
            {
                case "book":
                    return new BookBuilder();
                case "newspaper":
                    return new NewspaperBuilder();
                case "boardgame":
                    return new BoardGameBuilder();
                case "author":
                    return new AuthorBuilder();
                default:
                    throw new ArgumentException($"Invalid type: {type}");
            }
        }
    }
    public class BookBuilder : AbstractBuilder
    {
        private string _title = "Untitled";
        private int _year = 2023;
        private int _pageCount = 0;

 
        public BookBuilder()
        {
            FieldSetters.Add("title", value => _title = value);
            FieldSetters.Add("year", value => _year = int.TryParse(value, out int year) ? year : _year);
            FieldSetters.Add("pagecount", value => _pageCount = int.TryParse(value, out int pageCount) ? pageCount : _pageCount);
        }

        public override ICollectible Build()
        {
            return (ICollectible) new Book
            {
                Title = _title,
                Year = _year,
                PageCount = _pageCount,
            };
        }
    }
    public class AuthorBuilder : AbstractBuilder
    {
        private string _name = "Unknown";
        private string _surname = "Unknown";
        private int _birthYear = 2023;
        private string _nickname = null;

        public AuthorBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public AuthorBuilder SetSurname(string surname)
        {
            _surname = surname;
            return this;
        }

        public AuthorBuilder SetBirthYear(int birthYear)
        {
            _birthYear = birthYear;
            return this;
        }

        public AuthorBuilder SetNickname(string nickname)
        {
            _nickname = nickname;
            return this;
        }
        public AuthorBuilder()
        {
            FieldSetters.Add("name", value => _name = value);
            FieldSetters.Add("surname", value => _surname = value);
            FieldSetters.Add("birthyear", value => _birthYear = int.TryParse(value, out int birthYear) ? birthYear : _birthYear);
            FieldSetters.Add("nickname", value => _nickname = value);
        }

        public override ICollectible Build()
        {
            return new Author
            {
                Name = _name,
                Surname = _surname,
                BirthYear = _birthYear,
                Nickname = _nickname
            };
        }
    }

    public class NewspaperBuilder : AbstractBuilder
    {
        private string _title = "Untitled";
        private int _year = 2023;
        private int _pageCount = 0;

        public NewspaperBuilder SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public NewspaperBuilder SetYear(int year)
        {
            _year = year;
            return this;
        }

        public NewspaperBuilder SetPageCount(int pageCount)
        {
            _pageCount = pageCount;
            return this;
        }

        public NewspaperBuilder()
        {
            FieldSetters.Add("title", value => _title = value);
            FieldSetters.Add("year", value => _year = int.TryParse(value, out int year) ? year : _year);
            FieldSetters.Add("pagecount", value => _pageCount = int.TryParse(value, out int pageCount) ? pageCount : _pageCount);
        }

        public override ICollectible Build()
        {
            return new Newspaper
            {
                Title = _title,
                Year = _year,
                PageCount = _pageCount
            };
        }
    }

    public class BoardGameBuilder : AbstractBuilder
    {
        private string _title = "Untitled";
        private int _minPlayers = 2;
        private int _maxPlayers = 4;
        private int _difficulty = 1;
        private List<Author> _authors = new List<Author>();

        public BoardGameBuilder SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public BoardGameBuilder SetMinPlayers(int minPlayers)
        {
            _minPlayers = minPlayers;
            return this;
        }

        public BoardGameBuilder SetMaxPlayers(int maxPlayers)
        {
            _maxPlayers = maxPlayers;
            return this;
        }

        public BoardGameBuilder SetDifficulty(int difficulty)
        {
            _difficulty = difficulty;
            return this;
        }

        public BoardGameBuilder AddAuthor(Author author)
        {
            _authors.Add(author);
            return this;
        }

        public BoardGameBuilder()
        {
            FieldSetters.Add("title", value => _title = value);
            FieldSetters.Add("minplayers", value => _minPlayers = int.TryParse(value, out int minPlayers) ? minPlayers : _minPlayers);
            FieldSetters.Add("maxplayers", value => _maxPlayers = int.TryParse(value, out int maxPlayers) ? maxPlayers : _maxPlayers);
            FieldSetters.Add("difficulty", value => _difficulty = int.TryParse(value, out int difficulty) ? difficulty : _difficulty);
        }

        public override ICollectible Build()
        {
            return new BoardGame
            {
                Title = _title,
                MinPlayers = _minPlayers,
                MaxPlayers = _maxPlayers,
                Difficulty = _difficulty,
                Authors = _authors
            };
        }
    }


}
