using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primov_Algoritam
{
    public class DynamicGraph
    {
        public Dictionary<Node, List<Edge>> Nodes { get; set; }
        public List<Edge> Edges { get; set; }

        public DynamicGraph()
        {
            Nodes = new Dictionary<Node, List<Edge>>();
            Edges = new List<Edge>();

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
            if (Edges.Contains(e))
                return false;
            Edges.Add(e);
            n1.AddAdjacency(e);
            n2.AddAdjacency(e);
            return true;
        }

        public List<Edge>? Prim(Node root)
        {
            foreach(Edge e in Edges)
            {
                e.MST = false;
            }
            List<Edge> MST = new List<Edge>();
            FHeap pq = new FHeap();
            foreach(Node n in Nodes.Keys)
            {
                pq.Insert(n);
            }
            root.FNode.Key = 0;
            root.Parent = null;
            pq.FindMin();
            try
            {
                while (pq.NumberOfNodes > 0)
                {
                    //Console.WriteLine("\n\n\n");
                    //pq.PrintHeap();
                    FNode u = pq.ExtractMin();
                    if (u == null)
                    {
                        throw new Exception("u je null");
                    }
                    u.Data.FNode = null;
                    foreach(Edge incidentEdge in u.Data.Adjacent)
                    {
                        Node v = (incidentEdge.start == u.Data) ? incidentEdge.dest : incidentEdge.start;
                        if (v.FNode != null && incidentEdge.Weight < v.FNode.Key)
                        {
                            v.Parent = u.Data;
                            pq.UpdateNode(v.FNode, incidentEdge.Weight);
                            //Console.WriteLine("\n\n\n");
                            //pq.PrintHeap();
                        }
                    }
                    
                }

                foreach(Node n in Nodes.Keys)
                {
                    Node dest = n.Parent;
                    Edge target = new Edge(n, dest, 0);
                    if (dest != null)
                    {
                        foreach(Edge e in n.Adjacent)
                        {
                            if (e.Equals(target))
                            {
                                e.MST = true; 
                                break;
                            }
                        }
                    }
                }

                foreach(Edge e in Edges)
                {
                    if (e.MST)
                    {
                        MST.Add(e);
                    }
                }

                //Console.Write(PrintMST());
                return MST;
            }
            catch (Exception ex)
            {
                  Console.WriteLine(ex.Message);
                return null;
            }
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(Node n in Nodes.Keys)
            {
                sb.Append($"{n.Data}");
                foreach (Edge e in n.Adjacent)
                {
                    Node other = (e.start == n) ? e.dest : e.start;
                    sb.Append($"<-({e.Weight})->{other.Data}");
                }
                sb.Append('\n');
                sb.Append('|');
                sb.Append('\n');
            }
            return sb.ToString();
        }


        public string PrintMST()
        {
            StringBuilder sb = new StringBuilder();
            foreach(Edge e in Edges)
            {
                if (e.MST)
                {
                    sb.AppendLine($"{e.start.Data} --({e.Weight})-- {e.dest.Data}");
                }
            }
            return sb.ToString().Trim();
        }
    }
}

/*                while (pq.NumberOfNodes != 0)
                {
                    FNode u = pq.ExtractMin();
                    if (u == null)
                    {
                        throw new Exception("u je null");
                    }

                    u.Data.FNode = null;
                    foreach(Edge incidentEdge in u.Data.Adjacent)
                    {
                        Node v = (incidentEdge.start == u.Data) ? incidentEdge.dest : incidentEdge.start;
                        if (v.FNode != null && incidentEdge.Weight < v.FNode.Key)
                        {
                            pq.UpdateNode(v.FNode, incidentEdge.Weight);
                            v.FNode.Parent = u;
                            pq.FindMin();
                        }
                    }
                    //u.Data.ClearAdjacency();
                    MST.InsertNode(u.Data);
                    if (u.Parent != null) 
                    {
                        MST.InsertEdge(u.Data, u.Parent.Data, u.Key);
*//*                        int iterator = 0;
                        while(u.Data.Adjacent.Count > 1)
                        {
                            if (!e.Equals(u.Data.Adjacent[iterator]))
                            {
                                u.Data.Adjacent.RemoveAt(iterator);
                            }
                        }*//*
                    }
                }*/
