using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Prim
{
    public class DynamicGraph
    {

        public Dictionary<Node, List<Edge>> Nodes { get; set; }
        public Dictionary<int, Edge> Edges { get; set; }

        public DynamicGraph()
        {
            Nodes = new Dictionary<Node, List<Edge>>();
            Edges = new Dictionary<int, Edge>();

        }

        public void InsertNode(int data)
        {
            Node n = new Node(data);
            Nodes.Add(n, n.Adjacent);
        }
        public bool InsertNode(Node n)
        {
            if (Nodes.ContainsKey(n))
                return false;
            Nodes.Add(n, n.Adjacent);
            /*            {
                            if (!Edges.ContainsValue(e))
                                Edges.Add(Edges.Count, e);
                        }*/
            return true;
        }

        public bool InsertEdge(Node n1, Node n2, double w)
        {
            if (n1 == n2)
                return false;
            Edge e = new Edge(n1, n2, w);
            if (Edges.ContainsValue(e))
                return false;
            Edges.Add(Edges.Count, e);
            n1.AddAdjacency(e);
            n2.AddAdjacency(e);
            return true;
        }


        public DynamicGraph Prim(Node root)
        {
            DynamicGraph MST  = new DynamicGraph();
            MinHeap<Node> heap = new();
            foreach(Node n in Nodes.Keys)
            {
                if (n == root)
                {
                    n.Key = 0;
                }
                else
                {
                    n.Key = double.MaxValue;
                }
                n.Parent = null;
                heap.Insert(n);
            }
            while (heap.Size > 0)
            {
                Node u = heap.PopMin;
                Node uCopy = new Node(u.Data);
                uCopy.Key = u.Key;
                uCopy.Parent = u.Parent;

                
                foreach(Edge incidentEdge in u.Adjacent)
                {
                    Node v = (incidentEdge.start == u) ? incidentEdge.dest : incidentEdge.start;
                    if(v != heap.Peek && incidentEdge.Weight < v.Key)
                    {
                        v.Key = incidentEdge.Weight;
                        
                        v.Parent = uCopy;
                        
                    }
                }
                MST.InsertNode(uCopy);
                if (uCopy.Parent!= null)
                    MST.InsertEdge(uCopy, uCopy.Parent, u.Key);
            }
            return MST;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Node n in Nodes.Keys)
            {
                sb.Append($"{n.Data}");
                foreach (Edge e in n.Adjacent)
                {
                    Node other = (e.start == n) ? e.dest : e.start;
                    sb.Append($"<->{other.Data}");
                }
                sb.Append('\n');
                sb.Append('|');
                sb.Append('\n');
            }
            return sb.ToString();
        }
    }
}
