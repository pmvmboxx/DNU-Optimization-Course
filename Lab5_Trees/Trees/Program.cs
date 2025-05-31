using System;
using System.Collections.Generic;

namespace Trees
{
    public class TreeNode // клас для вузла бінарного дерева
    {
        public int val; // значення вузла
        public TreeNode left; // лівий нащадок
        public TreeNode right; // правий нащадок
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null) // констурктор
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (true)
            {
                Console.WriteLine("\n=== МЕНЮ ===");
                Console.WriteLine("1 - Inorder обхід дерева");
                Console.WriteLine("2 - Правобічний вигляд дерева");
                Console.WriteLine("0 - Вихід");
                Console.Write("Введіть номер задачі: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("\n[Inorder обхід]");
                        Task1();
                        break;
                    case "2":
                        Console.WriteLine("\n[Правобічний вигляд]");
                        Task2();
                        break;
                    case "0":
                        Console.WriteLine("Завершення програми...");
                        return;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            }
        }

        public static void Task1()
        {
            // створюємо дерево
            TreeNode root = new TreeNode(1,
            new TreeNode(2,
                new TreeNode(4),
                new TreeNode(5,
                    new TreeNode(6),
                    new TreeNode(7)
                )
            ),
            new TreeNode(3,
                null,
                new TreeNode(8,
                    new TreeNode(9),
                    null
                )
            )
        );
            // список для резкльтату обходу
            var result = new List<int>();
            // обхід
            InorderTraversal(root, result);
            // виводио результат
            Console.WriteLine("Inorder: [" + string.Join(", ", result) + "]");
        }
        // метод для рекурсивного inorder обходу, приймає корінь і список, у який буде записувати значення
        public static void InorderTraversal(TreeNode root, List<int> result)
        {
            // перевіряємо чи не поорожній вузол
            if (root == null)
            {
                return;
            }

            // рекурсивно обходимо ліве піддерево
            InorderTraversal(root.left, result);

            // додаємо значення поточного вузла до результату
            result.Add(root.val);

            // рекурсивно обходимо праве піддерево
            InorderTraversal(root.right, result);
        }

        public static void Task2()
        {
            // створюємо дерево

            TreeNode root = new TreeNode(1,
                new TreeNode(2,
                    null,
                    new TreeNode(5)
                ),
                new TreeNode(3,
                    null,
                    new TreeNode(4)
                )
            );

            var result = RightSideView(root);
            // виводио результат
            Console.WriteLine("Right Side View: [" + string.Join(", ", result) + "]");
        }

        public static IList<int> RightSideView(TreeNode root)
        {
            var result = new List<int>();
            if (root == null)
            {
                return result;
            }

            var queue = new Queue<TreeNode>(); //  черга для  BFS обробки дерева
            queue.Enqueue(root);

            // поки черга непорожня - обробляємо всі вузли рівня
            while (queue.Count > 0)
            {
                int levelSize = queue.Count; // кількість вузлів на цьому рівні

                for (int i = 0; i < levelSize; i++)
                {
                    var node = queue.Dequeue();

                    if (i == levelSize - 1)
                    {
                        result.Add(node.val); // останній на цьому рівні
                    }

                    if (node.left != null)
                    {
                        queue.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        queue.Enqueue(node.right);
                    }
                }
            }

            return result;
        }
    }
}
