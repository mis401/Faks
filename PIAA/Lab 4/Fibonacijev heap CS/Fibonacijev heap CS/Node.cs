using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacijev_heap_CS
{
    internal class Node
    {
        public int Data { get; set; }
        public int Degree { get; set; }
        public Node? Next { get; set; }
        public Node? Previous { get; set; }
        public Node? Parent { get; set; }
        public Node? LeftMostChild { get; set; }
        public bool Mark { get; set; }

        public Node()
        {
            Data = 0;
            Degree = 0;
            Next = this;
            Previous = this;
            Parent = null;
            LeftMostChild = null;
            Mark = false;
        }

        public Node(int data)
        {
            Data = data;
            Degree = 0;
            Next = this;
            Previous = this;
            Parent = null;
            LeftMostChild = null;
            Mark = false;
        }


        public static Node RemoveFromList(Node n)
        { 
            n.Previous.Next = n.Next;
            n.Next.Previous = n.Previous;
            n.Parent = null;
            n.Next = n;
            n.Previous = n;
            return n;
        }


        public static void InsertIntoList(Node n, Node start)
        {
            if (start == null)
            {
                start = n;
                start.Next = start;
                start.Previous = start;
            }
            else
            {
                n.Previous = start.Previous;
                start.Previous.Next = n;
                n.Next = start;
                start.Previous = n;
            }
        }
    }
}
