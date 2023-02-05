using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primov_Algoritam
{
    public class Node
    {
        public int Data { get; set; }
        public List<Edge> Adjacent { get; }
        public FNode? FNode { get; set; }
        public Node? Parent { get; set; }

        public Node()
        { 
            Data = 0;
            Adjacent=new List<Edge>();
        }

        public Node(int data)
        {
            Data = data;
            Adjacent= new List<Edge>();
        }

        public void AddAdjacency(Edge edge)
        {
            Adjacent.Add(edge);
        }

        public void ClearAdjacency()
        {
            Adjacent.Clear();
        }
    }
}
