using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ConcurrentTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var enterCollection = new BlockingCollection<int>(5);

            Task.Run(() =>
            {
                for (int i = 0; i < 11; i++)
                {
                    enterCollection.Add(i);
                    Console.WriteLine($"Element {i} added sucessfully.");
                }

                enterCollection.CompleteAdding();
            });

            Console.ReadLine();
            Console.WriteLine("Reading collection..");


            Task.Run(() =>
           {
               while (!enterCollection.IsCompleted)
               {
                   try
                   {
                       enterCollection.TryTake(out int k);
                       Console.WriteLine($"Element {k} was read.");
                   }
                   catch (Exception ex)
                   {
                       Console.WriteLine(ex.Message);                       
                   }                   
               }
           });

            Console.ReadLine();
        }
    }
}
