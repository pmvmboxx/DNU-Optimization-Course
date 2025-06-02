using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    // інтерфейс для хеш функції
    public interface IHashFunction
    {
        int Hash(string key, int tableSize);
        string Name { get; }
    }

    // реалізує просте хешування
    public class SimpleModHash : IHashFunction
    {
        public string Name => "SimpleMod";
        public int Hash(string key, int tableSize)
        {
            int sum = 0;
            foreach (char c in key)
                sum += c;
            return sum % tableSize;
        }
    }

    // реалізує хешування на основі добутку
    public class MultiplicativeHash : IHashFunction
    {
        public string Name => "Multiplicative";
        public int Hash(string key, int tableSize)
        {
            const double A = 0.6180339887;
            int k = Encoding.ASCII.GetBytes(key).Sum(b => b);
            double frac = k * A % 1;
            return (int)(tableSize * frac);
        }
    }

    // реалізує бітове хешування
    public class BitwiseHash : IHashFunction
    {
        public string Name => "Bitwise";
        public int Hash(string key, int tableSize)
        {
            int hash = 0;
            foreach (char c in key)
                hash = (hash << 5) ^ (hash >> 27) ^ c;
            return Math.Abs(hash % tableSize);
        }
    }

    // реалізує хешування на основі MD5 (крисптографічна хеш-функція)
    public class CryptoMD5Hash : IHashFunction
    {
        public string Name => "MD5";
        public int Hash(string key, int tableSize)
        {
            using var md5 = MD5.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(key);
            byte[] hash = md5.ComputeHash(bytes);
            int value = BitConverter.ToInt32(hash, 0);
            return Math.Abs(value % tableSize);
        }
    }
}
