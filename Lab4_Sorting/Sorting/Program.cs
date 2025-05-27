using System;
using System.Diagnostics;

class Program
{
    static readonly Random rnd = new Random();
    static readonly int[] sizes = { 10, 1000, 10000, 100000, 200000 };
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string[] algorithms = { "Bubble Sort", "Insertion Sort", "Quick Sort", "Merge Sort", "Count Sort", "Radix Sort" };

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

            Console.WriteLine("{0,-15} | {1,10} | {2,10} | {3,10} | {4,10} | {5,10}", "Algorithm", "10", "1K", "10K", "100K", "200K |");
            Console.WriteLine(new string('-', 81));

            foreach (string algo in algorithms)
            {
                Console.Write("{0,-15} |", algo);
                foreach (int size in sizes)
                {
                    int[] array = arrayType.Value(size);
                    //int[] array = GenerateRandomArray(size, 1, 1000000); // великий інтервал

                    Stopwatch sw = new Stopwatch();
                    int[] copy = (int[])array.Clone(); // Копіюємо масив, щоб не зіпсувати оригінал
                    sw.Start();

                    switch (algo)
                    {
                        case "Bubble Sort":
                            BubbleSort(copy);
                            break;
                        case "Insertion Sort":
                            InsertionSort(copy);
                            break;
                        case "Quick Sort":
                            QuickSort(copy, 0, copy.Length - 1);
                            break;
                        case "Merge Sort":
                            copy = MergeSort(copy);
                            break;
                        case "Count Sort":
                            copy = CountSort(copy);
                            break;
                        case "Radix Sort":
                            RadixSort(copy);
                            break;
                    }

                    sw.Stop();
                    Console.Write(" {0,10} |", sw.ElapsedMilliseconds + "ms");
                }
                Console.WriteLine();
            }
        }
    }

    static int[] GenerateRandomArray(int size, int min, int max)
    {
        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
            arr[i] = rnd.Next(min, max);
        return arr;
    }

    // Алгоритми сортування

    static void BubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
            for (int j = 0; j < n - i - 1; j++)
                if (arr[j] > arr[j + 1])
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
    }

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
        // середній із першого, середнього і останнього
        int mid = low + (high - low) / 2;
        int pivot = MedianOfThree(arr[low], arr[mid], arr[high]);

        // Знаходимо індекс півота в масиві
        int pivotIndex = Array.IndexOf(arr, pivot, low, high - low + 1);
        (arr[pivotIndex], arr[high]) = (arr[high], arr[pivotIndex]); // ставимо півот в кінець

        int i = low - 1;
        for (int j = low; j < high; j++)
        {
            if (arr[j] <= pivot)
                (arr[++i], arr[j]) = (arr[j], arr[i]);
        }
        (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
        return i + 1;
    }

    static int MedianOfThree(int a, int b, int c)
    {
        if ((a > b) != (a > c)) return a;
        else if ((b > a) != (b > c)) return b;
        else return c;
    }

    static int[] MergeSort(int[] arr)
    {
        if (arr.Length <= 1)
            return arr;
        int mid = arr.Length / 2;
        int[] left = new int[mid];
        int[] right = new int[arr.Length - mid];
        Array.Copy(arr, 0, left, 0, mid);
        Array.Copy(arr, mid, right, 0, arr.Length - mid);

        return Merge(MergeSort(left), MergeSort(right));
    }

    static int[] Merge(int[] left, int[] right)
    {
        int[] result = new int[left.Length + right.Length];
        int l = 0, r = 0, k = 0;
        while (l < left.Length && r < right.Length)
            result[k++] = (left[l] < right[r]) ? left[l++] : right[r++];
        while (l < left.Length) result[k++] = left[l++];
        while (r < right.Length) result[k++] = right[r++];
        return result;
    }

    static int[] CountSort(int[] arr)
    {
        int max = int.MinValue, min = int.MaxValue;
        foreach (int num in arr)
        {
            if (num > max) max = num;
            if (num < min) min = num;
        }

        int range = max - min + 1;
        int[] count = new int[range];
        foreach (int num in arr)
            count[num - min]++;

        int index = 0;
        for (int i = 0; i < range; i++)
            while (count[i]-- > 0)
                arr[index++] = i + min;

        return arr;
    }

    static void RadixSort(int[] arr)
    {
        int max = GetMax(arr);
        for (int exp = 1; max / exp > 0; exp *= 10)
            CountSortByDigit(arr, exp);
    }

    static void CountSortByDigit(int[] arr, int exp)
    {
        int n = arr.Length;
        int[] output = new int[n];
        int[] count = new int[10];
        for (int i = 0; i < n; i++)
            count[(arr[i] / exp) % 10]++;
        for (int i = 1; i < 10; i++)
            count[i] += count[i - 1];
        for (int i = n - 1; i >= 0; i--)
        {
            int digit = (arr[i] / exp) % 10;
            output[count[digit] - 1] = arr[i];
            count[digit]--;
        }
        for (int i = 0; i < n; i++)
            arr[i] = output[i];
    }

    static int GetMax(int[] arr)
    {
        int max = arr[0];
        foreach (int num in arr)
            if (num > max)
                max = num;
        return max;
    }
}
