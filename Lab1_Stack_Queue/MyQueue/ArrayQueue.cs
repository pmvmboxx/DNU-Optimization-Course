using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQueue
{
    public class ArrayQueue
    {
            private int[] array; //тут зберігаються елементи черги
            private int head = 0; //вказує на початок черги
            private int rear = -1; //вказує на останній елемен черги
            private int capacity; //розмір масиву

            public ArrayQueue(int size) //конструктор 
            {
                capacity = size;
                array = new int[capacity];
            }

            public void Enqueue(int item) //додавання елементу до черги 
        {
                if (rear == capacity - 1)
                {
                    Console.WriteLine("Черга переповнена.");
                    return;
                }

                rear++;
                array[rear] = item;
            }

            public int? Dequeue() //видаляє і повертає перший елемент черги 
            {
                if (IsEmpty())
                    return null;

                int item = array[head];
                head++;

                if (head > rear)
                {
                    head = 0;
                    rear = -1;
                }

                return item;
            }

            public void Clear() // очищення черги
            {
                head = 0;
                rear = -1;
            }

            public bool IsEmpty() // перевіряємо чи черга порожня
        {
                return rear < head;
            }

            public IEnumerable<int> GetItems() // повертає всі елементи черги як послідовність 
        {
                for (int i = head; i <= rear; i++)
                {
                    yield return array[i];
                }
            }

            public int Count() // повертає кількість елементів у черзі
            {
                return IsEmpty() ? 0 : rear - head + 1;
            }
        
    }
}
