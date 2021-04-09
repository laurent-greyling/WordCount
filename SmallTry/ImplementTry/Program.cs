using SomethingNew.Services;
using System;
using System.Diagnostics;

namespace ImplementTry
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Alpha
            Console.WriteLine("===================================================");
            Console.WriteLine("Key Value Store Basic");
            Console.WriteLine("===================================================");
            Console.WriteLine();
            var dictionary = new KeyValueStore<string, int>();

            var result = dictionary.Get("a");
            Console.WriteLine("Get a non existing key and check value");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Value for key A is { result }");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Set a keyvalue pair with Key==A and Value==10");
            Console.WriteLine();
            dictionary.Set("a", 10);

            result = dictionary.Get("a");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"The value for key A is { result }");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Set a keyvalue pair with Key==B and Value==10");
            Console.WriteLine();
            dictionary.Set("b", 10);

            var cnt = dictionary.Count(10);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Number of times the value 10 occured was { cnt }");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Delete Key C");
            dictionary.Delete("c");
            Console.WriteLine($"Delete Key B");
            dictionary.Delete("b");
            Console.WriteLine();

            cnt = dictionary.Count(10);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Number of times the value 10 occured was { cnt }");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Update key A value to 20");
            Console.WriteLine();
            dictionary.Set("a", 20);

            Console.ForegroundColor = ConsoleColor.Green;
            cnt = dictionary.Count(10);
            Console.WriteLine($"Number of times 10 occured was { cnt }");
            cnt = dictionary.Count(20);
            Console.WriteLine($"Number of times 20 occured was { cnt }");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(dictionary.End());

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.WriteLine("===================================================");
            Console.WriteLine("===================================================");
            Console.WriteLine();
            #endregion

            #region Beta
            //Console.WriteLine("===================================================");
            //Console.WriteLine("Key Value Store Transaction");
            //Console.WriteLine("===================================================");
            //Console.WriteLine();

            //var mainDictionary = new KeyValueStore<string, int>();

            //using (var scope = new TransactionService<string, int>(mainDictionary))
            //{
            //    Console.WriteLine("Begin TransAction");
            //    scope.Begin();

            //    Console.WriteLine("Set a keyvalue pair with Key==A and Value==10");
            //    scope.CurrentStore.Set("a", 10);

            //    Console.WriteLine("Commit Transaction");
            //    Console.WriteLine();
            //    scope.Commit();
            //}

            //var transActionResult = mainDictionary.Get("a");
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine($"The value for key A is { transActionResult }");
            //Console.WriteLine();

            //Console.ForegroundColor = ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.Black;
            //Console.WriteLine("===================================================");
            //Console.WriteLine("===================================================");
            //Console.WriteLine();
            #endregion

            #region Charlie
            //Console.WriteLine("===================================================");
            //Console.WriteLine("Key Value Store Transaction");
            //Console.WriteLine("===================================================");
            //Console.WriteLine();

            //var mainDictionary = new KeyValueStore<string, int>();

            //using (var scope = new TransactionService<string, int>(mainDictionary))
            //{
            //    Console.WriteLine("Begin TransAction");
            //    scope.Begin();

            //    Console.WriteLine("Set a keyvalue pair with Key==A and Value==20");
            //    scope.CurrentStore.Set("a", 20);

            //    Console.WriteLine("Rollback Transaction");
            //    Console.WriteLine();
            //    scope.RollBack(); //Can also just not do anything as using will invoke dispose at end and will auto rollback with no commit
            //}

            //var transActionResult = mainDictionary.Get("a");
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine($"The value for key A is { transActionResult }");
            //Console.WriteLine();

            //Console.ForegroundColor = ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.Black;
            //Console.WriteLine("===================================================");
            //Console.WriteLine("===================================================");
            //Console.WriteLine();
            #endregion

            #region Delta
            //Console.WriteLine("===================================================");
            //Console.WriteLine("Key Value Store Transaction");
            //Console.WriteLine("===================================================");
            //Console.WriteLine();

            //var mainDictionary = new KeyValueStore<string, int>();

            //using (var scope = new TransactionService<string, int>(mainDictionary))
            //{
            //    Console.WriteLine("Begin Transaction");
            //    scope.Begin();

            //    Console.ForegroundColor = ConsoleColor.White;
            //    Console.WriteLine("Set a keyvalue pair with Key==A and Value==20");
            //    Console.WriteLine();
            //    scope.CurrentStore.Set("a", 20);                

            //    using (var transaction = new TransactionService<string, int>(scope.MainStore))
            //    {
            //        Console.WriteLine("Begin 2nd Transaction");
            //        transaction.Begin();

            //        Console.WriteLine("Set a keyvalue pair with Key==A and Value==50");
            //        Console.WriteLine();
            //        transaction.CurrentStore.Set("a", 50);

            //        var value = transaction.CurrentStore.Get("a");                    
            //        Console.ForegroundColor = ConsoleColor.Green;
            //        Console.WriteLine($"The value for key A is { value }");
            //        Console.WriteLine();

            //        Console.ForegroundColor = ConsoleColor.White;
            //        Console.WriteLine($"Rollback Transaction 2");
            //        transaction.RollBack(); //will auto dispose with no commit at end of using
            //    }

            //    Console.ForegroundColor = ConsoleColor.Green;
            //    var val = scope.CurrentStore.Get("a");
            //    Console.WriteLine($"The value for key A is { val }");
            //    Console.WriteLine();

            //    Console.ForegroundColor = ConsoleColor.White;
            //    Console.WriteLine($"Commit");
            //    scope.Commit();
            //}

            //var transActionResult = mainDictionary.Get("a");
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine($"The value for key A is { transActionResult }");
            //Console.WriteLine();

            //Console.ForegroundColor = ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.Black;
            //Console.WriteLine("===================================================");
            //Console.WriteLine("===================================================");
            //Console.WriteLine();
            #endregion

            Console.ReadKey();
        }
    }
}
