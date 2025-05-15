namespace Stack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Введіть рядок (літери і числа):");
            string input = Console.ReadLine();
            Task1(input);

            Console.WriteLine("\nВведіть рядок для поділу на голосні/приголосні:");
            string input2 = Console.ReadLine();
            Task2(input2);

            Console.WriteLine("\nВведіть масив чисел через пробіл:");
            string[] inputArr = Console.ReadLine().Split(' ');
            int[] numbers = Array.ConvertAll(inputArr, int.Parse);
            SortArrayWithStack(numbers);
        }

        public static void Task1(string input)
        {
            Stack<char> charStack = new Stack<char>(); //стек для символів
            Stack<int> numberStack = new Stack<int>(); //стек для чисел

            foreach (char ch in input) //проходиося по кожному символу в input
            {
                if (char.IsDigit(ch)) //опрацьовуємо випадок, коли символ - число і додаємо
                    numberStack.Push(int.Parse(ch.ToString()));
                else if (char.IsLetter(ch)) //якщо це символ, то додаємо одразу до стеку
                    charStack.Push(ch);
            }

            Console.WriteLine("Символи у стеку:");
            foreach (var c in charStack)
                Console.Write(c + " ");
            Console.WriteLine("\nЧисла у стеку:");
            foreach (var n in numberStack)
                Console.Write(n + " ");
            Console.WriteLine();
        }

        public static void Task2(string input)
        {
            Stack<char> vowels = new Stack<char>(); //стек для голосних
            Stack<char> consonants = new Stack<char>(); //стек для приголосних
            string vowelLetters = "aeiouAEIOU";

            foreach (char ch in input) 
            {
                if (char.IsLetter(ch))
                {
                    if (vowelLetters.Contains(ch)) //якщо звук голосний, то додаємо в стек vowels
                        vowels.Push(ch);
                    else
                        consonants.Push(ch); //якщо приголосний, то додаємо в стек consonants
                }
            }

            Console.WriteLine("Голосні:");
            foreach (var c in vowels)
                Console.Write(c + " ");
            Console.WriteLine("\nПриголосні:");
            foreach (var c in consonants)
                Console.Write(c + " ");
            Console.WriteLine();
        }

        public static void SortArrayWithStack(int[] array) //алгоритм сорутвання insert sort, але за допомогою стеку
        {
            Stack<int> stack = new Stack<int>(); //головний стек для сортування
            Stack<int> tempStack = new Stack<int>(); //стек для тичасового зберігання елементів

            foreach (int number in array)
            {
                while (stack.Count > 0 && stack.Peek() < number)
                    tempStack.Push(stack.Pop());

                stack.Push(number);

                while (tempStack.Count > 0)
                    stack.Push(tempStack.Pop());
            }

            Console.WriteLine("Відсортований масив:");
            foreach (var item in stack)
                Console.Write(item + " ");
            Console.WriteLine();
        }
  
    }
}
