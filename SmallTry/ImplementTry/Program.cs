using SomethingNew.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ImplementTry
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Alpha
            //Console.WriteLine("===================================================");
            //Console.WriteLine("Key Value Store Basic");
            //Console.WriteLine("===================================================");
            //Console.WriteLine();
            //var dictionary = new KeyValueStore<string, int>();

            //Console.WriteLine("Get a non existing key and check value");
            //var result = dictionary.Get("a");
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine($"Value for key A is { ConvertToString(result) }");
            //Console.WriteLine();

            //Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine("Set a keyvalue pair with Key==A and Value==10");
            //Console.WriteLine();
            //dictionary.Set("a", 10);

            //result = dictionary.Get("a");
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine($"The value for key A is { ConvertToString(result) }");
            //Console.WriteLine();

            //Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine("Set a keyvalue pair with Key==B and Value==10");
            //Console.WriteLine();
            //dictionary.Set("b", 10);

            //var cnt = dictionary.Count(10);
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine($"Number of times the value 10 occured was { cnt }");
            //Console.WriteLine();

            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine($"Delete Key C");
            //dictionary.Delete("c");
            //Console.WriteLine($"Delete Key B");
            //dictionary.Delete("b");
            //Console.WriteLine();

            //cnt = dictionary.Count(10);
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine($"Number of times the value 10 occured was { cnt }");
            //Console.WriteLine();

            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine("Update key A value to 20");
            //Console.WriteLine();
            //dictionary.Set("a", 20);

            //Console.ForegroundColor = ConsoleColor.Green;
            //cnt = dictionary.Count(10);
            //Console.WriteLine($"Number of times 10 occured was { cnt }");
            //cnt = dictionary.Count(20);
            //Console.WriteLine($"Number of times 20 occured was { cnt }");
            //Console.WriteLine();

            //Console.ForegroundColor = ConsoleColor.Black;
            //Console.BackgroundColor = ConsoleColor.White;
            //Console.WriteLine(dictionary.End());

            //Console.ForegroundColor = ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.Black;

            //Console.WriteLine("===================================================");
            //Console.WriteLine("===================================================");
            //Console.WriteLine();
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

            //    Console.WriteLine("Try Commit Transaction");
            //    Console.WriteLine();
            //    IsTransactionSuccesful(scope.Committed());
            //}

            //var transActionResult = mainDictionary.Get("a");
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine($"The value for key A is { ConvertToString(transActionResult) }");
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

            //    Console.WriteLine("Try Rollback Transaction");
            //    Console.WriteLine();
            //    IsTransactionSuccesful(scope.RollBack()); //Can also just not do anything as using will invoke dispose at end and will auto rollback with no commit
            //}

            //var transActionResult = mainDictionary.Get("a");
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine($"The value for key A is { ConvertToString(transActionResult) }");
            //Console.WriteLine();

            //Console.ForegroundColor = ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.Black;
            //Console.WriteLine("===================================================");
            //Console.WriteLine("===================================================");
            //Console.WriteLine();
            #endregion

            #region Delta
            Console.WriteLine("===================================================");
            Console.WriteLine("Key Value Store Transaction");
            Console.WriteLine("===================================================");
            Console.WriteLine();

            var mainDictionary = new KeyValueStore<string, int>();

            using (var scope = new TransactionService<string, int>(mainDictionary))
            {
                Console.WriteLine("Begin Transaction");
                scope.Begin();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Set a keyvalue pair with Key==A and Value==20");
                Console.WriteLine();
                scope.CurrentStore.Set("a", 20);

                using (var transaction = new TransactionService<string, int>(scope.MainStore))
                {
                    Console.WriteLine("Begin 2nd Transaction");
                    transaction.Begin();

                    Console.WriteLine("Set a keyvalue pair with Key==A and Value==50");
                    Console.WriteLine();
                    transaction.CurrentStore.Set("a", 50);

                    var value = transaction.CurrentStore.Get("a");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"The value for key A is { ConvertToString(value) }");
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Try Rollback Transaction 2");
                    IsTransactionSuccesful(transaction.RollBack()); //will auto dispose with no commit at end of using
                }

                Console.ForegroundColor = ConsoleColor.Green;
                var val = scope.CurrentStore.Get("a");
                Console.WriteLine($"The value for key A is { ConvertToString(val) }");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Try Commit Transaction");
                IsTransactionSuccesful(scope.Committed());
            }

            var transActionResult = mainDictionary.Get("a");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"The value for key A is { ConvertToString(transActionResult) }");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("===================================================");
            Console.WriteLine("===================================================");
            Console.WriteLine();
            #endregion

            #region November
            //Console.WriteLine("===================================================");
            //Console.WriteLine("Key Value Store Transaction");
            //Console.WriteLine("===================================================");
            //Console.WriteLine();

            //var mainDictionary = new KeyValueStore<string, int>();

            //using (var scope = new TransactionService<string, int>(mainDictionary))
            //{
            //    Console.ForegroundColor = ConsoleColor.White;
            //    Console.WriteLine("Set a keyvalue pair with Key==A and Value==20");
            //    Console.WriteLine();
            //    scope.CurrentStore?.Set("a", 20);

            //    Console.ForegroundColor = ConsoleColor.White;
            //    Console.WriteLine($"Try Commit Transaction");

            //    IsTransactionSuccesful(scope.Committed());
            //}

            #endregion

            Console.ReadKey();
        }

        /// <summary>
        /// Console.Writeline will not write null to console window, this is to check if value is null
        /// If value is null set string null so console.writeline can print it on screen else send back actual value as string value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Value"></param>
        /// <returns></returns>
        private static string ConvertToString<T>(T Value) => Value == null ? "null" : Value.ToString();

        /// <summary>
        /// Will check if transaction was in error or not. If in error assume that no transaction was started
        /// A started transaction will initialise a new temp store, so without a begin, we will get a null reference, which can safely be seen as
        /// no transaction started
        /// </summary>
        /// <param name="action"></param>
        private static void IsTransactionSuccesful(bool action) 
        {
            if (action)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Transaction Succesfull");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR, no transaction");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
