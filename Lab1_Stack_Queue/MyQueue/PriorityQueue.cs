using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPriorityQueue
{
    internal class PriorityQueue<T>
    {
        // вузол черги з пріоритетом
        private class Node 
        {
            public T Value;
            public int Priority;

            public Node(T value, int priority)
            {
                Value = value;
                Priority = priority;
            }
        }

        private List<Node> nodes = new List<Node>();

        // додати елемент з пріоритетом
        public void Enqueue(T item, int priority)
        {
            nodes.Add(new Node(item, priority));
            // сортуємо, щоб елементи з найвищим пріоритетом були в початку списку
            nodes.Sort((a, b) => a.Priority.CompareTo(b.Priority));
        }

        // вилучити елемент з найвищим пріоритетом (з найменшим числом пріоритету)
        public T Dequeue()
        {
            if (nodes.Count == 0)
                throw new InvalidOperationException("Черга порожня.");

            var item = nodes[0].Value;
            nodes.RemoveAt(0);
            return item;
        }

        // перевірити, чи порожня черга
        public bool IsEmpty()
        {
            return nodes.Count == 0;
        }

        // поточна кількість елементів
        public int Count()
        {
            return nodes.Count;
        }
    }
}
