using System;

namespace LinqTutorials
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1: " + string.Join(", ", LinqTasks.Task1()));
            Console.WriteLine("Task 2: " + string.Join(", ", LinqTasks.Task2()));
            Console.WriteLine("Task 3: " + string.Join(", ", LinqTasks.Task3()));
            Console.WriteLine("Task 4: " + string.Join(", ", LinqTasks.Task4()));
            Console.WriteLine("Task 5: " + string.Join(", ", LinqTasks.Task5()));
            Console.WriteLine("Task 6: " + string.Join(", ", LinqTasks.Task6()));
            Console.WriteLine("Task 7: " + string.Join(", ", LinqTasks.Task7()));
            Console.WriteLine("Task 8: " + LinqTasks.Task8());
            Console.WriteLine("Task 9: " + LinqTasks.Task9());
            Console.WriteLine("Task 10: " + string.Join(", ", LinqTasks.Task10()));
            Console.WriteLine("Task 11: " + string.Join(", ", LinqTasks.Task11()));
            Console.WriteLine("Task 12: " + string.Join(", ", LinqTasks.Task12()));
            Console.WriteLine("Task 13: " + LinqTasks.Task13([1,1,1,1,1,1,10,1,1,1,1]));
            Console.WriteLine("Task 14: " + string.Join(", ", LinqTasks.Task14()));
            Console.WriteLine("Task 15: " + string.Join(", ", LinqTasks.Task15()));
            Console.WriteLine("Task 16: " + string.Join(", ", LinqTasks.Task16()));

        }
    }
}