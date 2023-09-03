using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class1
{
    public class Printer
    {
        public void PrintBookV(Book book)

        {

            Console.WriteLine("{0} ({1}),PageCount: {2}", book.Title, book.Year, book.PageCount);
        }
        public void PrintNewspaperV(Newspaper newspaper)
        {
            Console.WriteLine("{0} ({1}), Pages: {2}", newspaper.Title, newspaper.Year, newspaper.PageCount);
        }
        public void PrintBoardGameV(BoardGame boardGame)
        {
            Console.WriteLine("{0} ({1}-{2}), Difficulty: {3}", boardGame.Title, boardGame.MinPlayers, boardGame.MaxPlayers, boardGame.Difficulty);

        }
        public void PrintAuthorV(Author author)
        {

            string nickname = author.Nickname == null ? "" : $"({author.Nickname})";
            Console.WriteLine($"{author.Name} {author.Surname} - Birth Year: {author.BirthYear} {nickname}");

        }
    }
}
