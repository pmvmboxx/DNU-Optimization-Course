using System.Collections;
using System.Diagnostics;

namespace Lists
{
    internal class Program
    {
        const int ListSize = 100000;
        const int InsertCount = 1000;
        static Random random = new Random();

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Швидкість ArrayList та LinkedList<int> ===");

            // 1. Заповнення списку даними
            Console.WriteLine("\n1. Заповнення списку даними:");
            var arrayList = new ArrayList();
            var linkedList = new LinkedList<int>();

            MeasureTime("ArrayList - Заповнення", () =>
            {
                for (int i = 0; i < ListSize; i++)
                    arrayList.Add(random.Next(ListSize));
            });

            MeasureTime("LinkedList - Заповнення", () =>
            {
                for (int i = 0; i < ListSize; i++)
                    linkedList.AddLast(random.Next(ListSize));
            });

            // 2. Random Access (ArrayList підтримує доступ за індексом, LinkedList — ні)
            Console.WriteLine("\n2. Доступ за індексом (Random Access):");
            MeasureTime("ArrayList - Random Access", () =>
            {
                for (int i = 0; i < 10000; i++)
                    _ = arrayList[random.Next(ListSize)];
            });

            MeasureTime("LinkedList - Random Access", () =>
            {
                for (int i = 0; i < 1000; i++) // повільніше, тому менше ітерацій
                {
                    int index = random.Next(ListSize);
                    var node = linkedList.First;
                    for (int j = 0; j < index; j++)
                        node = node.Next;
                    _ = node.Value;
                }
            });

            // 3. Доступ за ітератором (Sequential Access)
            Console.WriteLine("\n3. Послідовний доступ (Sequential Access):");
            MeasureTime("ArrayList - Sequential Access", () =>
            {
                foreach (var item in arrayList)
                    _ = item;
            });

            MeasureTime("LinkedList - Sequential Access", () =>
            {
                foreach (var item in linkedList)
                    _ = item;
            });

            // 4. Вставка елементів на початок
            Console.WriteLine("\n4. Вставка на початок:");
            MeasureTime("ArrayList - Вставка на початок", () =>
            {
                for (int i = 0; i < InsertCount; i++)
                    arrayList.Insert(0, random.Next());
            });

            MeasureTime("LinkedList - Вставка на початок", () =>
            {
                for (int i = 0; i < InsertCount; i++)
                    linkedList.AddFirst(random.Next());
            });

            // 5. Вставка в кінець
            Console.WriteLine("\n5. Вставка в кінець:");
            MeasureTime("ArrayList - Вставка в кінець", () =>
            {
                for (int i = 0; i < InsertCount; i++)
                    arrayList.Add(random.Next());
            });

            MeasureTime("LinkedList - Вставка в кінець", () =>
            {
                for (int i = 0; i < InsertCount; i++)
                    linkedList.AddLast(random.Next());
            });

            // 6. Вставка в середину
            Console.WriteLine("\n6. Вставка в середину:");
            MeasureTime("ArrayList - Вставка в середину", () =>
            {
                int middle = arrayList.Count / 2;
                for (int i = 0; i < InsertCount; i++)
                    arrayList.Insert(middle, random.Next());
            });

            MeasureTime("LinkedList - Вставка в середину", () =>
            {
                int middle = linkedList.Count / 2;
                for (int i = 0; i < InsertCount; i++)
                {
                    var node = linkedList.First;
                    for (int j = 0; j < middle; j++)
                        node = node.Next;
                    linkedList.AddBefore(node, random.Next());
                }
            });
        }

        static void MeasureTime(string title, Action action)
        {
            var stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            Console.WriteLine($"{title}: {stopwatch.ElapsedMilliseconds} мс");
        }
    }
}
