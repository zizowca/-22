
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Задание_22
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Write("Введите количество элементов массива:\t");
            int n = Convert.ToInt32(Console.ReadLine());


            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1,n);

            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(Summa);
            Task task2 = task1.ContinueWith<int>(func2);


            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(Max);
            Task<int> task3 = task1.ContinueWith<int>(func3);

            task1.Start();
            Console.ReadKey();
        }
        
        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
            }
            for (int i = 0; i < array.Count(); i++)
            {
                Console.Write($"{array[i]} ");
            }
            return array;
        }
        static int Summa(Task <int []> task)
        {
            int[] array = task.Result ;
            Console.WriteLine("\nSumma начал работу\n");
            int s = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                s += array[i];
            }
            Console.WriteLine(s);
            Console.WriteLine("\nSumma окончил работу\n");
            return s;
        }
        static int Max(Task<int[]> task)
        {
            int[] array = task.Result;
            Console.WriteLine("\nMax начал работу\n");
            int max = 0;
            foreach (int b in array)
            {
                if (b > max)
                {
                    max = b;
                }
            }
            Console.WriteLine(max);
            Console.WriteLine("\nMax окончил работу\n");
            return max;

        }
    }
}
