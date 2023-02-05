using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;

namespace Sortiranje
{
    public class Program
    {
        public static long  memStart, memCheck=0, memMax=0;
        public static void Main()
        {
            var proc = System.Diagnostics.Process.GetCurrentProcess();

            Random rand = new Random();

            int[,][] nizovi = new int[6,3][];
            double stepen;
            for (int grupa = 0; grupa < 6; grupa++)
            {
                stepen = Math.Pow(10, grupa + 2);
                for (int init = 0; init < 3; init++)
                    nizovi[grupa, init] = new int[(int)stepen];
                for (int j = 0; j < stepen; j++)
                { 
                    nizovi[grupa,0][j] = nizovi[grupa,1][j] = nizovi[grupa,2][j] = rand.Next(10000);
                }
            }

            
            
            for (int i = 0; i < 6; i++)
            {
                stepen = Math.Pow(10, i + 2);
                Console.WriteLine($"Sortiranje {stepen} elemenata");
                Console.WriteLine("Selection sort:");
                SelectionSort(nizovi[i, 0]);
                Console.WriteLine("Heap sort:");
                HeapSort(nizovi[i, 1]);
                Console.WriteLine("Counting sort:");
                int[] sorted = new int[(int)stepen];
                sorted = CountSort(nizovi[i, 2]);
                Console.WriteLine();
                Console.WriteLine();
            }
            
            
        }
        

        public static void SelectionSort(int[] input, bool descending = true)
        {
            {
                memStart = getMemory();
                memMax = 0;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int index;
            int tmp;
            for (int i = 0; i < input.Length - 1; i++)
            {
                index = i;
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (descending)
                    {
                        if (input[j] < input[index])
                        {
                            index = j;
                        }
                    }
                    else
                    {
                        if (input[j] > input[index])
                        {
                            index = j;
                        }
                    }
                }

                tmp = input[i];
                input[i] = input[index];
                input[index] = tmp;
            }

            sw.Stop();
            Console.WriteLine(System.Diagnostics.Process.GetCurrentProcess().PeakWorkingSet-memStart);
            Console.WriteLine(sw.Elapsed);
        }

        public static int[] CountSort(int[] input)
        {
            {
                memStart = getMemory();
                memMax = 0;
            }


            Stopwatch sw = new Stopwatch();
            sw.Start();
            int n = input.Length;
            int[] output = new int[n];
            int k = input.Max() + 1;
            int[] temp = new int[k];



            for (int i = 0; i < k; i++)
            {
                temp[i] = 0;
            }
            for (int i = 0; i < n; i++)
            {
                temp[input[i]]++;
            }
            
            for (int i = 1; i < k; i++)
            {
                temp[i] = temp[i] + temp[i - 1];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                output[temp[input[i]]-1] = input[i];
                temp[input[i]]--;
            }


            sw.Stop();
            Console.WriteLine(System.Diagnostics.Process.GetCurrentProcess().PeakWorkingSet - memStart);
            Console.WriteLine(sw.Elapsed);


            return output;
        }




        public static void HeapSort(int[] input, bool maxHeap = false)
        {
            {
                memStart = getMemory();
                memMax = 0;
            }


            Stopwatch sw = new Stopwatch();
            sw.Start();
            BuildHeap(input, maxHeap);
            int inputSize = input.Length;
            int tmp;
            for (int i = input.Length-1; i>=1; i--)
            {
                tmp = input[i];
                input[i] = input[0];
                input[0] = tmp;
                inputSize--;
                Heapify(input, inputSize, 0, maxHeap);
            }


            sw.Stop();
            Console.WriteLine(System.Diagnostics.Process.GetCurrentProcess().PeakWorkingSet - memStart);
            Console.WriteLine(sw.Elapsed);
        }


        public static void BuildHeap(int[] list, bool maxHeap = false)
        {
            for (int i = list.Length/2; i >= 0; i--)
            {
                Heapify(list, list.Length, i, maxHeap);
            }
        }


        public static void Heapify(int[] heap, int heapSize, int i, bool maxHeap = false)
        {
            if (maxHeap) 
            {

                int l = 2 * i;
                int r = 2 *i + 1;
                int max = i;
                if (l < heapSize && heap[l] > heap[i])
                    max = l;
                if (r < heapSize && heap[r] > heap[max])
                    max = r;
                if (max != i)
                {
                    int tmp = heap[i];
                    heap[i] = heap[max];
                    heap[max] = tmp;
                    Heapify(heap, heapSize, max, maxHeap);
                }
            }
            else
            {

                int l = 2*i;
                int r = 2*i+1;
                int min = i;
                if (l < heapSize && heap[l] < heap[i])
                    min = l;
                if (r < heapSize && heap[r] < heap[min])
                    min = r;
                if (min != i)
                {
                    int tmp = heap[i];
                    heap[i] = heap[min];
                    heap[min] = tmp;
                    Heapify(heap, heapSize, min, maxHeap);
                }
            }
        }

        public static long getMemory()
        {
            return System.Diagnostics.Process.GetCurrentProcess().WorkingSet64;
        }
    }



}