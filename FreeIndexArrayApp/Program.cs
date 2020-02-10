using System;
using FreeIndexArrayLib.Classes;

namespace FreeIndexArrayApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FreeIndexArray<int> arr = new FreeIndexArray<int>(-4, 2);

            arr.Add(11);
            arr.Add(12);
            arr.Add(13);
            arr.Add(14);

            var test = arr[-4];
            test = arr[-3];
            test = arr[-2];
            test = arr[-1];

            arr[-4] = 14;
            arr[-3] = 13;
            arr[-2] = 12;
            arr[-1] = 11;

            foreach (var a in arr)
            {
                Console.WriteLine(a);
            }

            Console.ReadKey();

            Console.WriteLine(arr.Contains(14));
            Console.WriteLine(arr.Contains(11));
            Console.WriteLine(arr.Contains(15));

            Console.ReadKey();

            Console.WriteLine(arr.Remove(14));
            Console.WriteLine(arr.Remove(11));

            Console.ReadKey();

            foreach (var a in arr)
            {
                Console.WriteLine(a);
            }

            Console.ReadKey();

            var array = new int[20];

            arr.CopyTo(array, 3);

        }
    }
}
