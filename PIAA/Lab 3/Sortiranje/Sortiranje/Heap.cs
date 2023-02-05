using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sortiranje
{
    internal class HeapInt
    {
        private enum Type
        {
            min,
            max
        };
        private Type type;
        private int maxSize;
        public int[] heap { get { return heap; } set { heap = value; } }
        public int count { get { return count; } set { count = value; } }

        private int Left(int i)
        {
            return 2 * i;
        }
        private int Right(int i)
        {
            return 2 * i + 1;
        }

        private int Parent(int i)
        {
            return i / 2;
        }
        public HeapInt(int maxSize, bool maxHeap = false)
        {
            this.maxSize = maxSize;
            heap = new int[maxSize];
            count = 0;
            type = maxHeap ? Type.max : Type.min;
        }

        public void Heapify(int i)
        {
            if (type == Type.max)
            {
                if (i == 0)
                    return;
                int l = Left(i);
                int r = Right(i);
                int max = i;
                if (l <= maxSize && heap[l] > heap[i])
                    max = l;
                if (r <= maxSize && heap[r] > heap[max])
                    max = r;
                if (max != i)
                {
                    int tmp = heap[i];
                    heap[i] = heap[max];
                    heap[max] = tmp;
                    Heapify(max);
                }
            }
            else
            {

                if (i == 0)
                    return;
                int l = Left(i);
                int r = Right(i);
                int min = i;
                if (l <= maxSize && heap[l] < heap[i])
                    min = l;
                if (r <= maxSize && heap[r] < heap[min])
                    min = r;
                if (min != i)
                {
                    int tmp = heap[i];
                    heap[i] = heap[min];
                    heap[min] = tmp;
                    Heapify(min);
                }
            }
        }

        /*        public static void BuildHeap(int[] list, bool max = false)
                {
                    for (int i = list.Length/2; i >= 0; i--)
                    {
                        Heapify(list);
                    }
                } 
            }*/
    }
}
