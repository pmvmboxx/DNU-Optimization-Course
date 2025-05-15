namespace Queue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int n = GetValidInput("Введіть кількість елементів у черзі: ");
            int k = GetValidInput("Введіть кількість елементів для витягування: ");


            Queue<int> queue = CreateQueue(n); 
            DisplayQueue(queue);


            Console.WriteLine("\nВитягуємо перші {0} {1} черги:", k, GetElementWord(k)); 
            DisplayQueue(queue, k);

            DisplayQueue(queue); // виводимо залишок черги

            ClearQueue(ref queue); // очищаємо чергу
            DisplayQueue(queue); // перевіряємо чи черга порожня
        }

        public static Queue<int> CreateQueue(int n)
        {
            Queue<int> queue = new Queue<int>();
            Random rand = new Random();

            // додаємо n випадкових елементів у чергу
            for (int i = 0; i < n; i++)
            {
                queue.Enqueue(rand.Next(1, 101)); // заповнюємо випадковими числами від 1 до 100
            }

            Console.Write("\nЧерга створена.");
            return queue;
        }

        public static void DisplayQueue(Queue<int> queue, int? k = null)
        {
            if (queue.Count == 0)
            {
                Console.WriteLine("\nЧерга порожня.");
                return;
            }

            if (k.HasValue)
            {
                for (int i = 0; i < k.Value && queue.Count > 0; i++) //за допомогою count визначаємо кількість елементів у черзі
                {
                    Console.WriteLine(queue.Dequeue()); // видаляємо з черги
                }
            }
            else
            {
                Console.WriteLine("Вміст черги:");
                foreach (var item in queue)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }
        }

        public static void ClearQueue(ref Queue<int> queue)
        {
            queue.Clear(); // Очищаємо чергу
            Console.WriteLine("\nЧерга очищена.");
        }

        public static string GetElementWord(int count)
        {
            if (count % 10 == 1 && count % 100 != 11)
                return "елемент"; // однина
            else if ((count % 10 >= 2 && count % 10 <= 4) && (count % 100 < 10 || count % 100 >= 20))
                return "елементи"; // множина
            else
                return "елементів"; // форма родового відмінка
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
