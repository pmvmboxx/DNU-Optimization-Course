using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public class HashTableChaining : HashTableBase
    {
        private List<(string key, int value)>[] table;

        public HashTableChaining(int size, IHashFunction func) : base(size, func)
        {
            table = new List<(string key, int value)>[size];
            for (int i = 0; i < size; i++)
                table[i] = new List<(string, int)>();
        }

        public override void Insert(string key, int value)
        {
            int idx = hashFunc.Hash(key, size);
            foreach (var item in table[idx])
            {
                if (item.key == key)
                    return; // або можна оновити value, якщо потрібно
            }
            if (table[idx].Count > 0) Collisions++;
            table[idx].Add((key, value));
        }

        public override bool Search(string key)
        {
            int idx = hashFunc.Hash(key, size);
            return table[idx].Any(item => item.key == key);
        }

        public override bool Delete(string key)
        {
            int idx = hashFunc.Hash(key, size);
            var list = table[idx];
            int index = list.FindIndex(item => item.key == key);
            if (index != -1)
            {
                list.RemoveAt(index);
                return true;
            }
            return false;
        }
    }
}
