using System.ComponentModel;
using System.Diagnostics;

namespace Fibonacijev_heap_CS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("Da li zelite da se prikaze heap nakon gotovog broja ciklusa (rezultat ne staje u konzolu)? y/n");

            //char p = (char)Console.Read();
            //bool print = (p == 'y');
            double[] times;
            Console.WriteLine("Vremena su prikazana u tikovima");
            for (int n = 10; n <= 100000; n = n * 10)
            {
                times = Cycle(n, false);
                Console.WriteLine($"Ciklus od: {n}: prosek dodavanja {times[0]}, prosek uklanjanja {times[1]}, prosek ekstrakcije {times[2]}");
            }
            
        }

        public static double[] Cycle(int n, bool print)
        {
            double[] results = new double[3] { 0, 0, 0 };//insert remove extract
            const int k = 100000;
            Heap heap = new Heap();
            Stopwatch insert = new Stopwatch();
            Stopwatch remove = new Stopwatch();
            Stopwatch extract = new Stopwatch();
            Random random= new Random();
            for (int i = 0; i < n; i++)
            {

                //Console.WriteLine($"Iteracija {i} ciklusa {n}");
                insert.Restart();
                heap.Insert(random.Next(k));
                heap.Insert(random.Next(k));
                heap.Insert(random.Next(k));
                insert.Stop();
                results[0] += (double)insert.ElapsedTicks;

                remove.Restart();
                heap.RemoveMostRecent();
                remove.Stop();
                results[1] += (double)remove.ElapsedTicks;

                extract.Restart();
                heap.ExtractMin();
                extract.Stop();
                results[2] += (double)extract.ElapsedTicks;


                insert.Restart();
                heap.Insert(random.Next(k));
                heap.Insert(random.Next(k));
                insert.Stop();
                results[0] += insert.ElapsedTicks;

                remove.Restart();
                heap.RemoveMostRecent();
                remove.Stop();
                results[1] += remove.ElapsedTicks;

                insert.Restart();
                heap.Insert(random.Next(k));
                heap.Insert(random.Next(k));
                insert.Stop();
                results[0] += insert.ElapsedTicks;

                extract.Restart();
                heap.ExtractMin();
                extract.Stop();
                results[2] += (double)extract.ElapsedTicks;

                insert.Restart();
                heap.Insert(random.Next(k));
                insert.Stop();
                results[0] += insert.ElapsedTicks;

            }

            //Console.WriteLine("Heap na kraju ciklusa: ");
            results[0] /= (n * 8);
            results[1] /= (n * 2);
            results[2] /= (n * 2);

            if (print == true)
            {
                Console.WriteLine($"Heap na kraju {n} ciklusa izgleda ovako: ");
                heap.PrintHeap();
                Console.WriteLine("\n\n\n\n\n");
            }
            return results;
        }
    }
}