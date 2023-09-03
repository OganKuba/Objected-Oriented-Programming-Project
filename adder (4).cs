using class1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj1oobjmain
{
    public class adder
    {
        public static void bookstore0(BajtpikBookstore bajtpikBookstore)
        {
            var douglasAdams = new Author { Name = "Douglas", Surname = "Adams", BirthYear = 1952 };
            var tomWolfe = new Author { Name = "Tom", Surname = "Wolfe", BirthYear = 1930 };
            var elmarEisemann = new Author { Name = "Elmar", Surname = "Eisemann", BirthYear = 1978 };
            var michaelSchwarz = new Author { Name = "Michael", Surname = "Schwarz", BirthYear = 1970 };
            var ulfAssarsson = new Author { Name = "Ulf", Surname = "Assarsson", BirthYear = 1975 };
            var michaelWimmer = new Author { Name = "Michael", Surname = "Wimmer", BirthYear = 1980 };
            var frankHerbert = new Author { Name = "Frank", Surname = "Herbert", BirthYear = 1920 };
            var terryPratchett = new Author { Name = "Terry", Surname = "Pratchett", BirthYear = 1948 };
            var neilGaiman = new Author { Name = "Neil", Surname = "Gaiman", BirthYear = 1960 };
            var jameyStegmaier = new Author { Name = "Jamey", Surname = "Stegmaier", BirthYear = 1975 };
            var jakubRozalski = new Author { Name = "Jakub", Surname = "Różalski", BirthYear = 1981, Nickname = "Mr. Werewolf" };
            var klausTeuber = new Author { Name = "Klaus", Surname = "Teuber", BirthYear = 1952 };
            var alfredButts = new Author { Name = "Alfred", Surname = "Butts", BirthYear = 1899 };
            var jamesBrunot = new Author { Name = "James", Surname = "Brunot", BirthYear = 1902 };
            var christianPetersen = new Author { Name = "Christian T.", Surname = "Petersen", BirthYear = 1970 };
            bajtpikBookstore.Authors.Add(new Author { Name = "Douglas", Surname = "Adams", BirthYear = 1952 });
            bajtpikBookstore.Authors.Add(new Author { Name = "Tom", Surname = "Wolfe", BirthYear = 1930 });
            bajtpikBookstore.Authors.Add(new Author { Name = "Elmar", Surname = "Eisemann", BirthYear = 1978 });
            bajtpikBookstore.Authors.Add(new Author { Name = "Michael", Surname = "Schwarz", BirthYear = 1970 });
            bajtpikBookstore.Authors.Add(new Author { Name = "Ulf", Surname = "Assarsson", BirthYear = 1975 });
            bajtpikBookstore.Authors.Add(new Author { Name = "Michael", Surname = "Wimmer", BirthYear = 1980 });
            bajtpikBookstore.Authors.Add(new Author { Name = "Frank", Surname = "Herbert", BirthYear = 1920 });
            bajtpikBookstore.Authors.Add(new Author { Name = "Terry", Surname = "Pratchett", BirthYear = 1948 });
            bajtpikBookstore.Authors.Add(new Author { Name = "Neil", Surname = "Gaiman", BirthYear = 1960 });
            bajtpikBookstore.Authors.Add(new Author { Name = "Jamey", Surname = "Stegmaier", BirthYear = 1975 });
            bajtpikBookstore.Authors.Add(new Author { Name = "Jakub", Surname = "Różalski", BirthYear = 1981, Nickname = "Mr. Werewolf" });
            bajtpikBookstore.Authors.Add(new Author { Name = "Klaus", Surname = "Teuber", BirthYear = 1952 });
            bajtpikBookstore.Authors.Add(new Author { Name = "Alfred", Surname = "Butts", BirthYear = 1899 });
            bajtpikBookstore.Authors.Add(new Author { Name = "James", Surname = "Brunot", BirthYear = 1902 });
            bajtpikBookstore.Authors.Add(new Author { Name = "Christian T.", Surname = "Petersen", BirthYear = 1970 });


            // create books and add
            var hitchhikersGuide = new Book { Title = "The Hitchhiker's Guide to the Galaxy", Year = 1987, PageCount = 320 };
            hitchhikersGuide.Authors = new List<Author> { douglasAdams };

            var rightStuff = new Book { Title = "The Right Stuff", Year = 1993, PageCount = 512 };
            rightStuff.Authors = new List<Author> { tomWolfe };

            var realTimeShadows = new Book { Title = "Real-Time Shadows", Year = 2011, PageCount = 383 };
            realTimeShadows.Authors = new List<Author> { elmarEisemann, michaelSchwarz, ulfAssarsson, michaelWimmer };

            var mesjaszDiuny = new Book { Title = "Mesjasz Diuny", Year = 1972, PageCount = 272 };
            mesjaszDiuny.Authors = new List<Author> { frankHerbert };

            var dobryOmen = new Book { Title = "Dobry Omen", Year = 1990, PageCount = 416 };
            dobryOmen.Authors = new List<Author> { terryPratchett, neilGaiman };
            bajtpikBookstore.Books.Add(hitchhikersGuide);
            bajtpikBookstore.Books.Add(rightStuff);
            bajtpikBookstore.Books.Add(realTimeShadows);
            bajtpikBookstore.Books.Add(mesjaszDiuny);
            bajtpikBookstore.Books.Add(dobryOmen);
            //create Newspaper and add
            bajtpikBookstore.Newspapers.Add(new Newspaper { Title = "International Journal of Human-Computer Studies", Year = int.MaxValue, PageCount = 300 });
            bajtpikBookstore.Newspapers.Add(new Newspaper { Title = "Nature", Year = 1869, PageCount = 200 });
            bajtpikBookstore.Newspapers.Add(new Newspaper { Title = "National Geographic", Year = 2001, PageCount = 106 });
            bajtpikBookstore.Newspapers.Add(new Newspaper { Title = "Pixel", Year = 2015, PageCount = 115 });
            //create BoardGame and add
            var scythe = new BoardGame
            {
                Title = "Scythe",
                MinPlayers = 1,
                MaxPlayers = 5,
                Difficulty = 7,
                Authors = new List<Author> { jameyStegmaier, jakubRozalski }
            };

            var catan = new BoardGame
            {
                Title = "Catan",
                MinPlayers = 3,
                MaxPlayers = 4,
                Difficulty = 6,
                Authors = new List<Author> { klausTeuber }
            };

            var scrabble = new BoardGame
            {
                Title = "Scrabble",
                MinPlayers = 2,
                MaxPlayers = 4,
                Difficulty = 5,
                Authors = new List<Author> { jamesBrunot, alfredButts }
            };
            var twilightImperium = new BoardGame
            {
                Title = "Twilight Imperium",
                MinPlayers = 3,
                MaxPlayers = 8,
                Difficulty = 9,
                Authors = new List<Author> { christianPetersen }
            };
            bajtpikBookstore.BoardGames.Add(scythe);
            bajtpikBookstore.BoardGames.Add(catan);
            bajtpikBookstore.BoardGames.Add(scrabble);
            bajtpikBookstore.BoardGames.Add(twilightImperium);
        }
        public static void bookstore1(BajtpikBookstore1 bookstore)
        {
            bookstore.AddAuthor("Douglas", "Adams", 1952);
            bookstore.AddAuthor("Tom", "Wolfe", 1930);
            bookstore.AddAuthor("Elmar", "Eisemann", 1978);
            bookstore.AddAuthor("Michael", "Schwarz", 1970);
            bookstore.AddAuthor("Ulf", "Assarsson", 1975);
            bookstore.AddAuthor("Michael", "Wimmer", 1980);
            bookstore.AddAuthor("Frank", "Herbert", 1920);
            bookstore.AddAuthor("Terry", "Pratchett", 1948);
            bookstore.AddAuthor("Neil", "Gaiman", 1960);
            bookstore.AddAuthor("Jamey", "Stegmaier", 1975);
            bookstore.AddAuthor("Jakub", "Różalski", 1981, "Mr. Werewolf");
            bookstore.AddAuthor("Klaus", "Teuber", 1952);
            bookstore.AddAuthor("Alfred", "Butts", 1899);
            bookstore.AddAuthor("James", "Brunot", 1902);
            bookstore.AddAuthor("Christian T.", "Petersen", 1970);
            //add books
            bookstore.AddBook("The Hitchhiker's Guide to the Galaxy", new List<int> { 1 }, 1987, 320);
            bookstore.AddBook("The Right Stuff", new List<int> { 2 }, 1993, 512);
            bookstore.AddBook("Real-Time Shadows", new List<int> { 3, 4, 5, 6 }, 2011, 383);
            bookstore.AddBook("Mesjasz Diuny", new List<int> { 7 }, 1972, 272);
            bookstore.AddBook("Dobry Omen", new List<int> { 8, 9 }, 1990, 416);
            /// add newspaper
            bookstore.AddNewspaper("International Journal of Human-Computer Studies", 2023, 300);
            bookstore.AddNewspaper("Nature", 1869, 200);
            bookstore.AddNewspaper("National Geographic", 2001, 106);
            bookstore.AddNewspaper("Pixel", 2015, 115);
            //add boardgame
            bookstore.AddBoardGame("Scythe", 1, 5, 7, new List<int> { 10, 11 });
            bookstore.AddBoardGame("Catan", 3, 4, 6, new List<int> { 12 });
            bookstore.AddBoardGame("Scrabble", 2, 4, 5, new List<int> { 13, 14 });
            bookstore.AddBoardGame("Twilight Imperium", 3, 8, 9, new List<int> { 15 });
        }
        public static void bookstore3(BajtpikBookstore3 store3)
        {
            ///adding elemnts to store3
            ////add authors
            store3.AddAuthor("<Douglas>+<Adams>+<1952>");
            store3.AddAuthor("<Tom>+<Wolfe>+<1930>");
            store3.AddAuthor("<Elmar>+<Eisemann>+<1978>");
            store3.AddAuthor("<Michael>+<Schwarz>+<1970>");
            store3.AddAuthor("<Ulf>+<Assarsson>+<1975>");
            store3.AddAuthor("<Michael>+<Wimmer>+<1980>");
            store3.AddAuthor("<Frank>+<Herbert>+<1920>");
            store3.AddAuthor("<Terry>+<Pratchett>+<1948>");
            store3.AddAuthor("<Neil>+<Gaiman>+<1960>");
            store3.AddAuthor("<Jamey>+<Stegmaier>+<1975>$<Mr. Werewolf>$");
            store3.AddAuthor("<Jakub>+<Różalski>+<1981>");
            store3.AddAuthor("<Klaus>+<Teuber>+<1952>");
            store3.AddAuthor("<Alfred>+<Butts>+<1899>");
            store3.AddAuthor("<James>+<Brunot>+<1902>");
            store3.AddAuthor("<Christian T.>+<Petersen>+<1970>");
            ///add books
            store3.AddBook("<The Hitchhiker's Guide to the Galaxy>(<1987>)", "(<1>)", 320);
            store3.AddBook("<The Right Stuff>(<1993>)", "(<2>)", 512);
            store3.AddBook("<Real-Time Shadows>(<2011>)", "(<3>),(<4>),(<5>),(<6>)", 383);
            store3.AddBook("<Mesjasz Diuny>(<1972>)", "(<7>)", 272);
            store3.AddBook("<Dobry Omen>(<1990>)", "(<8>),(<9>)", 416);
            //add newspapers
            store3.AddNewspaper("<International Journal of Human-Computer Studies>(<2023>)", 300);
            store3.AddNewspaper("<Nature>(<1869>)", 200);
            store3.AddNewspaper("<National Geographic>(<2001>)", 106);
            store3.AddNewspaper("<Pixel>(<2015>)", 115);
            //add boardgames
            //bookstore.AddBoardGame("Scythe", 1, 5, 7, new List<int> { 10, 11 });
            //bookstore.AddBoardGame("Catan", 3, 4, 6, new List<int> { 12 });
            //bookstore.AddBoardGame("Scrabble", 2, 4, 5, new List<int> { 13, 14 });
            //bookstore.AddBoardGame("Twilight Imperium", 3, 8, 9, new List<int> { 15 });
            store3.AddBoardGame("Scythe", "<1>/<5>", 7, "(<10>),(<11>)");
            store3.AddBoardGame("Catan", "<3>/<4>", 6, "(<12>)");
            store3.AddBoardGame("Scrabble", "<2>/<4>", 5, "(<13>),(<14>)");
            store3.AddBoardGame("Twilight Imperium", "<3>/<8>", 9, "(<15>)");

        }
        public static void forestadder(Forest forest)
        {
            var douglasAdams = new Author { Name = "Douglas", Surname = "Adams", BirthYear = 1952 };
            var tomWolfe = new Author { Name = "Tom", Surname = "Wolfe", BirthYear = 1930 };
            var elmarEisemann = new Author { Name = "Elmar", Surname = "Eisemann", BirthYear = 1978 };
            var michaelSchwarz = new Author { Name = "Michael", Surname = "Schwarz", BirthYear = 1970 };
            var ulfAssarsson = new Author { Name = "Ulf", Surname = "Assarsson", BirthYear = 1975 };
            var michaelWimmer = new Author { Name = "Michael", Surname = "Wimmer", BirthYear = 1980 };
            var frankHerbert = new Author { Name = "Frank", Surname = "Herbert", BirthYear = 1920 };
            var terryPratchett = new Author { Name = "Terry", Surname = "Pratchett", BirthYear = 1948 };
            var neilGaiman = new Author { Name = "Neil", Surname = "Gaiman", BirthYear = 1960 };
            var jameyStegmaier = new Author { Name = "Jamey", Surname = "Stegmaier", BirthYear = 1975 };
            var jakubRozalski = new Author { Name = "Jakub", Surname = "Różalski", BirthYear = 1981, Nickname = "Mr. Werewolf" };
            var klausTeuber = new Author { Name = "Klaus", Surname = "Teuber", BirthYear = 1952 };
            var alfredButts = new Author { Name = "Alfred", Surname = "Butts", BirthYear = 1899 };
            var jamesBrunot = new Author { Name = "James", Surname = "Brunot", BirthYear = 1902 };
            var christianPetersen = new Author { Name = "Christian T.", Surname = "Petersen", BirthYear = 1970 };
            forest.AuthorTree.Insert(new Author { Name = "Douglas", Surname = "Adams", BirthYear = 1952 });
            forest.AuthorTree.Insert(new Author { Name = "Tom", Surname = "Wolfe", BirthYear = 1930 });
            forest.AuthorTree.Insert(new Author { Name = "Elmar", Surname = "Eisemann", BirthYear = 1978 });
            forest.AuthorTree.Insert(new Author { Name = "Michael", Surname = "Schwarz", BirthYear = 1970 });
            forest.AuthorTree.Insert(new Author { Name = "Ulf", Surname = "Assarsson", BirthYear = 1975 });
            forest.AuthorTree.Insert(new Author { Name = "Michael", Surname = "Wimmer", BirthYear = 1980 });
            forest.AuthorTree.Insert(new Author { Name = "Frank", Surname = "Herbert", BirthYear = 1920 });
            forest.AuthorTree.Insert(new Author { Name = "Terry", Surname = "Pratchett", BirthYear = 1948 });
            forest.AuthorTree.Insert(new Author { Name = "Neil", Surname = "Gaiman", BirthYear = 1960 });
            forest.AuthorTree.Insert(new Author { Name = "Jamey", Surname = "Stegmaier", BirthYear = 1975 });
            forest.AuthorTree.Insert(new Author { Name = "Jakub", Surname = "Różalski", BirthYear = 1981, Nickname = "Mr. Werewolf" });
            forest.AuthorTree.Insert(new Author { Name = "Klaus", Surname = "Teuber", BirthYear = 1952 });
            forest.AuthorTree.Insert(new Author { Name = "Alfred", Surname = "Butts", BirthYear = 1899 });
            forest.AuthorTree.Insert(new Author { Name = "James", Surname = "Brunot", BirthYear = 1902 });
            forest.AuthorTree.Insert(new Author { Name = "Christian T.", Surname = "Petersen", BirthYear = 1970 });


            // create books and add
            var hitchhikersGuide = new Book { Title = "The Hitchhiker's Guide to the Galaxy", Year = 1987, PageCount = 320 };
            hitchhikersGuide.Authors = new List<Author> { douglasAdams };

            var rightStuff = new Book { Title = "The Right Stuff", Year = 1993, PageCount = 512 };
            rightStuff.Authors = new List<Author> { tomWolfe };

            var realTimeShadows = new Book { Title = "Real-Time Shadows", Year = 2011, PageCount = 383 };
            realTimeShadows.Authors = new List<Author> { elmarEisemann, michaelSchwarz, ulfAssarsson, michaelWimmer };

            var mesjaszDiuny = new Book { Title = "Mesjasz Diuny", Year = 1972, PageCount = 272 };
            mesjaszDiuny.Authors = new List<Author> { frankHerbert };

            var dobryOmen = new Book { Title = "Dobry Omen", Year = 1990, PageCount = 416 };
            dobryOmen.Authors = new List<Author> { terryPratchett, neilGaiman };
            forest.BookTree.Insert(hitchhikersGuide);
            forest.BookTree.Insert(rightStuff);
            forest.BookTree.Insert(realTimeShadows);
            forest.BookTree.Insert(mesjaszDiuny);
            forest.BookTree.Insert(dobryOmen);
            //create Newspaper and add
            forest.NewspaperTree.Insert(new Newspaper { Title = "International Journal of Human-Computer Studies", Year = int.MaxValue, PageCount = 300 });
            forest.NewspaperTree.Insert(new Newspaper { Title = "Nature", Year = 1869, PageCount = 200 });
            forest.NewspaperTree.Insert(new Newspaper { Title = "National Geographic", Year = 2001, PageCount = 106 });
            forest.NewspaperTree.Insert(new Newspaper { Title = "Pixel", Year = 2015, PageCount = 115 });
            //create BoardGame and add
            var scythe = new BoardGame
            {
                Title = "Scythe",
                MinPlayers = 1,
                MaxPlayers = 5,
                Difficulty = 7,
                Authors = new List<Author> { jameyStegmaier, jakubRozalski }
            };

            var catan = new BoardGame
            {
                Title = "Catan",
                MinPlayers = 3,
                MaxPlayers = 4,
                Difficulty = 6,
                Authors = new List<Author> { klausTeuber }
            };

            var scrabble = new BoardGame
            {
                Title = "Scrabble",
                MinPlayers = 2,
                MaxPlayers = 4,
                Difficulty = 5,
                Authors = new List<Author> { jamesBrunot, alfredButts }
            };
            var twilightImperium = new BoardGame
            {
                Title = "Twilight Imperium",
                MinPlayers = 3,
                MaxPlayers = 8,
                Difficulty = 9,
                Authors = new List<Author> { christianPetersen }
            };
            
            forest.BoardGameTree.Insert(scythe);
            forest.BoardGameTree.Insert(catan);
            forest.BoardGameTree.Insert(scrabble);
            forest.BoardGameTree.Insert(twilightImperium);
        }
    }
}
