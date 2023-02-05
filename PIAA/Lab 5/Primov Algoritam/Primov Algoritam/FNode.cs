using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Primov_Algoritam
{
    public class FNode
    {
        public Node Data { get; set; }
        public double Key { get; set; }
        public int Degree { get; set; }

        public FNode? Parent { get; set; }
        public FNode? Next { get; set; }
        public FNode? Previous { get; set; }
        public FNode? LeftMostChild { get; set; }
        public bool Mark { get; set; }

        public FNode()
        {
            Data = null;
            Key = 0;
            Degree = 0;
            Next = this;
            Previous = this;
            Parent = null;
            LeftMostChild = null;
            Mark = false;
        }

        public FNode(Node n)
        {
            Data = n;
            Key = Double.MaxValue;
            Degree = 0;
            Next = this;
            Previous = this;
            Parent = null;
            LeftMostChild = null;
            Mark = false;
        }


        public static FNode RemoveFromList(FNode n)
        {
            n.Previous.Next = n.Next;
            n.Next.Previous = n.Previous;
            n.Parent = null;
            n.Next = n;
            n.Previous = n;
            return n;
        }


        public static void InsertIntoList(FNode? n, FNode? start)
        {
            if (n == null)
                return;
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