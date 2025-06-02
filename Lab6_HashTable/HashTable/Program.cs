using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HashTable
{
    public abstract class HashTableBase
    {
        protected int size;
        protected string[] keys;
        protected int[] values;
        protected bool[] used;
        protected IHashFunction hashFunc;
        public int Collisions { get; protected set; } = 0;

        public HashTableBase(int size, IHashFunction func)
        {
            this.size = size;
            keys = new string[size];
            values = new int[size];
            used = new bool[size];
            hashFunc = func;
        }

        // Властивість для доступу до хеш-функції
        public IHashFunction HashFunction => hashFunc;

        public abstract void Insert(string key, int value);
        public abstract bool Search(string key);
        public abstract bool Delete(string key);
    }

    internal class Program
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            int keyCount = 10000;
            int tableSize = keyCount * 2;

            var keys = GenerateTestKeys(keyCount);

            var hashFunctions = new List<IHashFunction>()
            {
                new SimpleModHash(),
                new MultiplicativeHash(),
                new BitwiseHash(),
                new CryptoMD5Hash()
            };

            PrintHeader();

            foreach (var func in hashFunctions)
            {
                var chainingTable = new HashTableChaining(tableSize, func);
                Benchmark("Chaining", chainingTable, keys);

                var linearTable = new HashTableLinear(tableSize, func);
                Benchmark("Linear Probing", linearTable, keys);

                var quadraticTable = new HashTableQuadratic(tableSize, func);
                Benchmark("Quadratic Probing", quadraticTable, keys);

                Console.WriteLine();
            }
        }

        static List<string> GenerateTestKeys(int count)
        {
            var keys = new List<string>();
            for (int i = 0; i < count; i++)
            {
                var str = new string(
                    Enumerable.Range(0, 8)
                    .Select(_ => (char)rnd.Next('A', 'Z' + 1))
                    .ToArray());
                keys.Add(str);
            }
            return keys;
        }

        static void PrintHeader()
        {
            Console.WriteLine(new string('-', 90));
            Console.WriteLine($"{"Table Type",-15} | {"Hash Function",-15} | {"Insert (ms)",10} | {"Search (ms)",10} | {"Delete (ms)",10} | {"Collisions",10}");
            Console.WriteLine(new string('-', 90));
        }

        static void Benchmark(string tableName, HashTableBase table, List<string> keys)
        {
            Stopwatch sw = Stopwatch.StartNew();

            foreach (var key in keys)
                table.Insert(key, 1);
            sw.Stop();
            long insertTime = sw.ElapsedMilliseconds;

            sw.Restart();
            foreach (var key in keys)
                table.Search(key);
            sw.Stop();
            long searchTime = sw.ElapsedMilliseconds;

            sw.Restart();
            foreach (var key in keys)
                table.Delete(key);
            sw.Stop();
            long deleteTime = sw.ElapsedMilliseconds;

            Console.WriteLine($"{tableName,-17} | {table.HashFunction.Name,-15} | {insertTime,10} | {searchTime,10} | {deleteTime,10} | {table.Collisions,10}");
        }
    }
}