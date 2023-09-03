using class1;
using proj1oobjmain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class query
{
    public static void printAfterYear1970(BajtpikBookstore B){
        Console.WriteLine("Books with authors born after 1970:");
        foreach (var book in B.Books)
        {
            if (book.Authors.Any(author => author.BirthYear > 1970))
            {
                Console.WriteLine("{0} ({1}), Authors: {2}", book.Title, book.Year, string.Join(", ", book.Authors.Select(a => $"{a.Name} {a.Surname}")));
            }
        }

        Console.WriteLine("\nBoard Games with authors born after 1970:");
        foreach (var boardGame in B.BoardGames)
        {
            if (boardGame.Authors.Any(author => author.BirthYear > 1970))
            {
                Console.WriteLine("{0}, Players: ({1}-{2}), Difficulty: {3}, Authors: {4}", boardGame.Title, boardGame.MinPlayers, boardGame.MaxPlayers, boardGame.Difficulty, string.Join(", ", boardGame.Authors.Select(a => $"{a.Name} {a.Surname}")));
            }
        }
    }
}
