using MyPriorityQueue;

namespace MyQueue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Робота з ArrayQueue
            int n = GetValidInput("Введіть кількість елементів у черзі: ");
            int k = GetValidInput("Введіть кількість елементів для витягування: ");

            ArrayQueue queue = CreateQueue(n);
            DisplayQueue(queue);

            Console.WriteLine("\nВитягуємо перші {0} {1} черги:", k, GetElementWord(k));
            DisplayQueue(queue, k);

            DisplayQueue(queue); // виводимо залишок черги

            queue.Clear(); // очищаємо чергу
            DisplayQueue(queue); // перевіряємо чи черга порожня


            // Робота з PriorityQueue
            Console.WriteLine("\n\nТест PriorityQueue.");

            var priorityQueue = new PriorityQueue<string>();
            priorityQueue.Enqueue("Низький пріоритет", 5);
            priorityQueue.Enqueue("Високий пріоритет", 1);
            priorityQueue.Enqueue("Середній пріоритет", 3);

            Console.WriteLine("\nВміст PriorityQueue за пріоритетом:");
            while (!priorityQueue.IsEmpty())
            {
                Console.WriteLine(priorityQueue.Dequeue());
            }
        }

        public static ArrayQueue CreateQueue(int n)
        {
            ArrayQueue queue = new ArrayQueue(n);
            Random rand = new Random();

            for (int i = 0; i < n; i++)
            {
                queue.Enqueue(rand.Next(1, 101));
            }

            Console.Write("\nЧерга створена.");
            return queue;
        }

        public static void DisplayQueue(ArrayQueue queue, int? k = null)
        {
            if (queue.IsEmpty())
            {
                Console.WriteLine("\nЧерга порожня.");
                return;
            }

            if (k.HasValue)
            {
                for (int i = 0; i < k.Value; i++)
                {
                    var item = queue.Dequeue();
                    if (item.HasValue)
                        Console.WriteLine(item.Value);
                }
            }
            else
            {
                Console.WriteLine("Вміст черги:");
                foreach (var item in queue.GetItems())
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }
        }

        public static string GetElementWord(int count)
        {
            if (count % 10 == 1 && count % 100 != 11)
                return "елемент";
            else if ((count % 10 >= 2 && count % 10 <= 4) && (count % 100 < 10 || count % 100 >= 20))
                return "елементи";
            else
                return "елементів";
        }

        public static int GetValidInput(string prompt)
        {
            int input;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out input) || input <= 0)
            {
                Console.WriteLine("Будь ласка, введіть коректне число більше нуля.");
                Console.Write(prompt);
            }
            return input;
        }
    }
}


