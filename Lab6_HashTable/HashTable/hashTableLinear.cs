using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    // реалізація хеш-таблиці з лінійним зондуванням
    public class HashTableLinear : HashTableBase
    {
        private bool[] deleted;

        public HashTableLinear(int size, IHashFunction func) : base(size, func)
        {
            deleted = new bool[size];
        }

        public override void Insert(string key, int value)
        {
            int idx = hashFunc.Hash(key, size);
            int originalIdx = idx;
            int firstDeleted = -1;

            while (used[idx])
            {
                if (keys[idx] == key)
                {
                    values[idx] += value;
                    return;
                }
                if (deleted[idx] && firstDeleted == -1)
                    firstDeleted = idx;

                Collisions++;
                idx = (idx + 1) % size;
                if (idx == originalIdx)
                    break;
            }

            if (firstDeleted != -1)
                idx = firstDeleted;

            used[idx] = true;
            deleted[idx] = false;
            keys[idx] = key;
            values[idx] = value;
        }

        public override bool Search(string key)
        {
            int idx = hashFunc.Hash(key, size);
            int originalIdx = idx;

            while (used[idx] || deleted[idx])
            {
                if (used[idx] && keys[idx] == key)
                    return true;

                idx = (idx + 1) % size;
                if (idx == originalIdx)
                    break;
            }
            return false;
        }

        public override bool Delete(string key)
        {
            int idx = hashFunc.Hash(key, size);
            int originalIdx = idx;

            while (used[idx] || deleted[idx])
            {
                if (used[idx] && keys[idx] == key)
                {
                    used[idx] = false;
                    deleted[idx] = true;
                    keys[idx] = null;
                    values[idx] = 0;
                    return true;
                }
                idx = (idx + 1) % size;
                if (idx == originalIdx)
                    break;
            }
            return false;
        }
    }
}
