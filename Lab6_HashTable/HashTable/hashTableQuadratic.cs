using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    // реалізація хеш-таблиці з квадратичним зондуванням
    public class HashTableQuadratic : HashTableBase
    {
        private bool[] deleted;

        public HashTableQuadratic(int size, IHashFunction func) : base(size, func)
        {
            deleted = new bool[size];
        }

        public override void Insert(string key, int value)
        {
            int idx = hashFunc.Hash(key, size);
            int i = 0;
            int firstDeleted = -1;

            while (used[(idx + i * i) % size])
            {
                int pos = (idx + i * i) % size;

                if (keys[pos] == key)
                {
                    values[pos] += value;
                    return;
                }

                if (deleted[pos] && firstDeleted == -1)
                    firstDeleted = pos;

                Collisions++;
                i++;
                if (i >= size)
                    return;  // Таблиця заповнена
            }

            int insertPos = firstDeleted != -1 ? firstDeleted : (idx + i * i) % size;
            used[insertPos] = true;
            deleted[insertPos] = false;
            keys[insertPos] = key;
            values[insertPos] = value;
        }

        public override bool Search(string key)
        {
            int idx = hashFunc.Hash(key, size);
            int i = 0;

            while (used[(idx + i * i) % size] || deleted[(idx + i * i) % size])
            {
                int pos = (idx + i * i) % size;

                if (used[pos] && keys[pos] == key)
                    return true;

                i++;
                if (i >= size)
                    break;
            }
            return false;
        }

        public override bool Delete(string key)
        {
            int idx = hashFunc.Hash(key, size);
            int i = 0;

            while (used[(idx + i * i) % size] || deleted[(idx + i * i) % size])
            {
                int pos = (idx + i * i) % size;

                if (used[pos] && keys[pos] == key)
                {
                    used[pos] = false;
                    deleted[pos] = true;
                    keys[pos] = null;
                    values[pos] = 0;
                    return true;
                }

                i++;
                if (i >= size)
                    break;
            }
            return false;
        }
    }
}
