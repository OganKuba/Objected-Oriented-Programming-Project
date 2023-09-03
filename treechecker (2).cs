using proj1oobjmain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class1
{
    public class treechecker
    {
        public static void check<T>(BinaryTree<T> binaryTree ) where T : IComparable<T>, IComparable
        {
           /// Create an instance of the BinaryTree class
          

          

            // Traverse the tree using InOrderIterator
            Console.WriteLine("In-order traversal:");
            IIterator<T> inOrderIterator = binaryTree.GetInOrderIterator();
            while (inOrderIterator.MoveNext())
            {
                Console.WriteLine(inOrderIterator.Current);
            }

            // Traverse the tree using ReverseInOrderIterator
            Console.WriteLine("\nReverse in-order traversal:");
                    IIterator<T> reverseInOrderIterator = binaryTree.GetReverseInOrderIterator();
                    while (reverseInOrderIterator.MoveNext())
                    {
                        Console.WriteLine(reverseInOrderIterator.Current);
                    }

            // Remove a value from the tree
            //int valueToRemove = 5;

            //if (binaryTree.Remove(valueToRemove))
            //{
            //    Console.WriteLine($"\nValue {valueToRemove} removed successfully.");
            //}
            //else
            //{
            //    Console.WriteLine($"\nValue {valueToRemove} not found in the tree.");
            //}
            //Console.WriteLine("algorithms");
            //int searchValue = 15;
           // int? foundElement = Find(binaryTree.GetInOrderIterator(), value => value == searchValue);
           // Console.WriteLine(foundElement.HasValue ? $"Found element: {foundElement.Value}" : "Element not found.");


           // Console.WriteLine("ForEach: doubling elements:");
          //  ForEach(binaryTree.GetInOrderIterator(), value => Console.Write($"{value * 2} "));

            //int minValue = 10;
           // int count = CountIf(binaryTree.GetInOrderIterator(), value => value >= minValue);
           // Console.WriteLine($"\nNumber of elements greater than or equal to {minValue}: {count}");
         }
        static int CountIf<T>(IIterator<T> iterator, Func<T, bool> predicate)
        {
            int count = 0;
            while (iterator.MoveNext())
            {
                if (predicate(iterator.Current))
                {
                    count++;
                }
            }
            return count;
        }
        static T? Find<T>(IIterator<T> iterator, Func<T, bool> predicate) where T : struct
        {
            while (iterator.MoveNext())
            {
                if (predicate(iterator.Current))
                {
                    return iterator.Current;
                }
            }
            return null;
        }

        static void ForEach<T>(IIterator<T> iterator, Action<T> action)
        {
            while (iterator.MoveNext())
            {
                action(iterator.Current);
            }
        }

    }
}
