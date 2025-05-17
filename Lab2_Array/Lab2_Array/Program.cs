namespace Lab2_Array
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
 
            int choice;
            do
            {
                Console.WriteLine("\nОберіть завдання:");
                Console.WriteLine("1 - Підмасив із сумою 0");
                Console.WriteLine("2 - Обмін рядків у матриці (мін/макс)");
                Console.WriteLine("3 - Побітове порівняння масивів");
                Console.WriteLine("0 - Вихід");
                Console.Write("Ваш вибір: ");
                choice = int.Parse(Console.ReadLine()); 

                switch (choice)
                {
                    case 1:
                        int[] arr = { 3, 4, -7, 1, 2, -6, 3 };
                        Task1(arr);
                        break;
                    case 2:
                        int[,] matrix = {
                        { 3,  2,  5 },
                        { 8, -5,  0 },
                        { 7, 10, -1 }
                        };
                        Task2(matrix);
                        break;
                    case 3:
                        int[] a = { 5, 255, 0, 170, 85, 128, 15, 240, 34, 221 };
                        int[] b = { 1, 255, 255, 85, 170, 0, 15, 0, 17, 221 };
                        Task3_BiWiseCoparison(a, b);
                        break;
                    case 0:
                        Console.WriteLine("До побачення!");
                        break;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
            while (choice != 0);
        }

        // Завдання 1 
        public static void Task1(int[] arr)
        {
            HashSet<int> sums = new HashSet<int>();
            int sum = 0;
            bool found = false;

            foreach (int num in arr)
            {
                sum += num;
                if (sum == 0 || sums.Contains(sum))
                {
                    found = true;
                    break;
                }
                sums.Add(sum);
            }

            Console.WriteLine("\nМасив: " + string.Join(", ", arr));
            Console.WriteLine(found
                ? "Існує підмасив із сумою 0."
                : "Підмасиву із сумою 0 не знайдено.");
        }

        // Завдання 2 
        public static void Task2(int[,] matrix)
        {

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int min = matrix[0, 0], max = matrix[0, 0];
            int minRow = 0, maxRow = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] < min)
                    {
                        min = matrix[i, j];
                        minRow = i;
                    }
                    if (matrix[i, j] > max)
                    {
                        max = matrix[i, j];
                        maxRow = i;
                    }
                }
            }

            Console.WriteLine("\nПочаткова матриця:");
            PrintMatrix(matrix);

            if (minRow != maxRow)
            {
                for (int j = 0; j < cols; j++)
                {
                    int temp = matrix[minRow, j];
                    matrix[minRow, j] = matrix[maxRow, j];
                    matrix[maxRow, j] = temp;
                }
            }

            Console.WriteLine($"\nРядки {minRow} (мін={min}) та {maxRow} (макс={max}) поміняно місцями.");
            Console.WriteLine("Матриця після заміни:");
            PrintMatrix(matrix);
        }

        public static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{matrix[i, j],4}");
                }
                Console.WriteLine();
            }
        }

        // Завдання 3 
        public static void Task3_BiWiseCoparison(int[] a, int[] b)
        {
            Console.Write("\na = ");
            PrintMatrix(a);
            Console.Write("b = ");
            PrintMatrix(b);
            
            int[] result = new int[10];

            for (int i = 0; i < 10; i++)
            {
                result[i] = Xnor(a[i], b[i]);
            }

            Console.WriteLine("\nІндекс |   A   |   B   | Результат (Dec) | Результат (Hex)");
            Console.WriteLine("----------------------------------------------------------");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{i,5} | {a[i],5} | {b[i],5} | {result[i],15} | {result[i]:X2}");
            }
        }

        public static int Xnor(int a, int b)
        {
            return ~(a ^ b) & 0xFF; // 8 біт
        }

        public static void PrintMatrix(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i],4}");
            }
            Console.WriteLine();
        }
    }
}
