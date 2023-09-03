using proj1oobjmain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace class1
{
    public class Forest
    {
        public BinaryTree<Author> AuthorTree { get; set; }
        public BinaryTree<Book> BookTree { get; set; }
        public BinaryTree<Newspaper> NewspaperTree { get; set; }
        public BinaryTree<BoardGame> BoardGameTree { get; set; }

        public Forest()
        {
            AuthorTree = new BinaryTree<Author>();
            BookTree = new BinaryTree<Book>();
            NewspaperTree = new BinaryTree<Newspaper>();
            BoardGameTree = new BinaryTree<BoardGame>();
        }
        public IIterator<T> Find<T>(BinaryTree<T> tree, Dictionary<string, (string comparison, string value)> filters) where T : class, IFilterable, IComparable<T>
        {
            var inOrderIterator = tree.GetInOrderIterator();
            List<T> filteredItems = new List<T>();

            while (inOrderIterator.MoveNext())
            {
                T item = inOrderIterator.Current;

                if (item.MatchesFilters(filters))
                {
                    filteredItems.Add(item);
                }
            }

            return new ListIterator<T>(filteredItems);
        }

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
        public class ListIterator<T> : IIterator<T>
        {
            private readonly List<T> _items;
            private int _currentIndex;

            public ListIterator(List<T> items)
            {
                _items = items;
                _currentIndex = -1;
            }

            public T Current => _items[_currentIndex];

            public bool MoveNext()
            {
                if (_currentIndex + 1 >= _items.Count)
                {
                    return false;
                }

                _currentIndex++;
                return true;
            }

            public bool MovePrev()
            {
                if (_currentIndex - 1 < 0)
                {
                    return false;
                }

                _currentIndex--;
                return true;
            }

            public void Reset()
            {
                _currentIndex = -1;
            }
        }


    }

}
