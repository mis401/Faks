using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Prim
{
    public class Node : IComparable<Node>
    {
        public int Data { get; set; }
        public List<Edge> Adjacent { get; }
        public double Key { get; set; }

        public Node? Parent { get; set; }

        public Node()
        {
            Data = 0;
            Adjacent = new List<Edge>();
        }

        public Node(int data)
        {
            Data = data;
            Adjacent = new List<Edge>();
        }

        public void AddAdjacency(Edge edge)
        {
            Adjacent.Add(edge);
        }

        public void ClearAdjacency()
        {
            Adjacent.Clear();
        }

        public int CompareTo(Node? other)
        {
            if (other == null)
                return 1;
            return Key.CompareTo(other.Key);
        }
    }
}
