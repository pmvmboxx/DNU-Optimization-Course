using System;

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
                        int[] a = { 45158, 12345, 65535, 0, 11111, 22222, 33333, 44444, 55555, 60000 };
                        int[] b = { 7125, 54321, 0, 65535, 11111, 12345, 33333, 9999, 8888, 60000 };
                        Task3_BiWiseComparison(a, b);
                        break;
                    case 0:
                        Console.WriteLine("Завершення програми.");
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
            bool found = false;

            for (int i = 0; i < arr.Length; i++)
            {
                int sum = 0;
                for (int j = i; j < arr.Length; j++)
                {
                    sum += arr[j];
                    if (sum == 0)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                    break;
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

                Console.WriteLine($"\nРядки {minRow} (мін={min}) та {maxRow} (макс={max}) поміняно місцями.");
            }
            else
            {
                Console.WriteLine("\nМаксимум і мінімум у одному рядку. Заміна не потрібна.");
            }

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
        public static void Task3_BiWiseComparison(int[] arr1, int[] arr2)
        {

            int[] resultArr = new int[10];

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("\na = ");
            Console.WriteLine("[" + string.Join(", ", arr1) + "]");

            Console.Write("b = ");
            Console.WriteLine("[" + string.Join(", ", arr2) + "]");
            Console.ResetColor();

            Console.WriteLine("\nРезультати побітового порівняння:");
            for (int i = 0; i < 10; i++)
            {
                int a = arr1[i];
                int b = arr2[i];
                int result = ~(a ^ b) & 0xFFFF;  // побітове XNOR для 16 біт
                resultArr[i] = result;
   
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\narr1[{i}]={a} ");
                Console.Write($"arr2[{i}]={b}");
                Console.ResetColor();

                Console.WriteLine("\n\nBinary:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(Convert.ToString(a, 2).PadLeft(16, '0'));
                Console.WriteLine(Convert.ToString(b, 2).PadLeft(16, '0'));
                Console.ResetColor();
                Console.WriteLine(new string('─', 16));
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(Convert.ToString(result, 2).PadLeft(16, '0'));
                Console.ResetColor();

                Console.WriteLine($"Decimal: {result}");
                Console.WriteLine($"Hex: {result:X4}");
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nРезультуючий масив:");
            Console.WriteLine("[" + string.Join(", ", resultArr) + "]");
            Console.ResetColor();
        }
    }
}
