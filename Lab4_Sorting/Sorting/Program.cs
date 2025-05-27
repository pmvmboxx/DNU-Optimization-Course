using System.Diagnostics;

namespace Sorting
{
    internal class Program
    {
        static readonly int[] Sizes = { 10, 1000, 10_000, 100_000 }; // 100_000_000 excluded for performance
        static readonly Random rnd = new Random();

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Порівняння алгоритмів сортування ===\n");

            var sorters = new Dictionary<string, Action<int[]>>
            {
                ["Insertion Sort"] = InsertionSort,
                ["Bubble Sort"] = BubbleSort,
                ["Quick Sort"] = QuickSort,
                ["Merge Sort"] = MergeSort,
                ["Count Sort"] = CountSort,
                ["Radix Sort"] = RadixSort
            };

            var arrayTypes = new Dictionary<string, Func<int, int[]>>
            {
                ["Великий інтервал"] = size => Enumerable.Range(0, size).Select(_ => rnd.Next()).ToArray(),
                ["Малий інтервал"] = size => Enumerable.Range(0, size).Select(_ => rnd.Next(0, 100)).ToArray(),
                ["Спадання (великий)"] = size => Enumerable.Range(0, size).Select(_ => rnd.Next()).OrderByDescending(x => x).ToArray(),
                ["Спадання (малий)"] = size => Enumerable.Range(0, size).Select(_ => rnd.Next(0, 100)).OrderByDescending(x => x).ToArray(),
            };

            foreach (var arrayType in arrayTypes)
            {
                Console.WriteLine($"\nТип масиву: {arrayType.Key}\n");
                PrintHeader();

                foreach (var sorter in sorters)
                {
                    Console.Write($"{sorter.Key,-18}");

                    foreach (var size in Sizes)
                    {
                        var arr = arrayType.Value(size);
                        var copy = new int[arr.Length];
                        arr.CopyTo(copy, 0);

                        try
                        {
                            var sw = Stopwatch.StartNew();
                            sorter.Value(copy);
                            sw.Stop();
                            Console.Write($"{sw.ElapsedMilliseconds,10} мс");
                        }
                        catch
                        {
                            Console.Write($"{"N/A",10}");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }

        static void PrintHeader()
        {
            Console.Write($"{"Алгоритм",-18}");
            foreach (var size in Sizes)
                Console.Write($"{size,10}");
            Console.WriteLine();
        }

        // ========== СОРТУВАННЯ ==========

        static void InsertionSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int key = arr[i], j = i - 1;
                while (j >= 0 && arr[j] > key)
                    arr[j + 1] = arr[j--];
                arr[j + 1] = key;
            }
        }

        static void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - i - 1; j++)
                    if (arr[j] > arr[j + 1])
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
        }

        static void QuickSort(int[] arr) => QuickSort(arr, 0, arr.Length - 1);
        static void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(arr, low, high);
                QuickSort(arr, low, pi - 1);
                QuickSort(arr, pi + 1, high);
            }
        }
        static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high], i = low - 1;
            for (int j = low; j < high; j++)
                if (arr[j] < pivot)
                    (arr[++i], arr[j]) = (arr[j], arr[i]);
            (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
            return i + 1;
        }

        static void MergeSort(int[] arr)
        {
            if (arr.Length <= 1) return;
            int mid = arr.Length / 2;
            int[] left = arr.Take(mid).ToArray();
            int[] right = arr.Skip(mid).ToArray();
            MergeSort(left);
            MergeSort(right);
            Merge(arr, left, right);
        }
        static void Merge(int[] arr, int[] left, int[] right)
        {
            int i = 0, j = 0, k = 0;
            while (i < left.Length && j < right.Length)
                arr[k++] = left[i] < right[j] ? left[i++] : right[j++];
            while (i < left.Length) arr[k++] = left[i++];
            while (j < right.Length) arr[k++] = right[j++];
        }

        static void CountSort(int[] arr)
        {
            int max = arr.Max(), min = arr.Min();
            int[] count = new int[max - min + 1];
            foreach (var num in arr) count[num - min]++;

            int index = 0;
            for (int i = 0; i < count.Length; i++)
                while (count[i]-- > 0)
                    arr[index++] = i + min;
        }

        static void RadixSort(int[] arr)
        {
            int max = arr.Max();
            for (int exp = 1; max / exp > 0; exp *= 10)
                CountingSortByDigit(arr, exp);
        }
        static void CountingSortByDigit(int[] arr, int exp)
        {
            int[] output = new int[arr.Length];
            int[] count = new int[10];

            foreach (var num in arr)
                count[(num / exp) % 10]++;

            for (int i = 1; i < 10; i++)
                count[i] += count[i - 1];

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                int digit = (arr[i] / exp) % 10;
                output[--count[digit]] = arr[i];
            }
            Array.Copy(output, arr, arr.Length);
        }
    }
}
