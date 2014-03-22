using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRU
{
    class Program
    {
        static void Main(string[] args)
        {
            Storage<int> cache = new Storage<int>(3);

            // 3 - 2 - 1
            // 4 - 3 - 2
            // 2 - 4 - 3
            // 4 - 2 - 3
            // 3 - 4 - 2
            // 6 - 5 - 3

            cache.Put("gappu", 1);
            cache.Put("Tecy", 3);
            cache.Put("Amma", 5);
            cache.Put("gappu", 6);
            cache.Put("Tecy", 4);
            cache.Put("Andy", 2);

            // Andy -> Tecy -> gappu

            Test(cache, "gappu", 6);
            Test(cache, "Tecy", 4);
            Test(cache, "Andy", 2);
        }

        static void Test(Storage<int> storage, string key, int expectedValue)
        {
            Console.WriteLine("Expected: {0}, Actual: {1}", expectedValue, storage.Get(key));
        }
    }
}
