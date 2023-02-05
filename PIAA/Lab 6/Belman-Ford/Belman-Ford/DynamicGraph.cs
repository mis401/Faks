using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belman_Ford
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
            Edge inverse = new Edge(n2, n1, w);
            Edges.Add(inverse);
            n2.AddAdjacency(inverse);
            return true;
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

        public List<Edge>? BelmanFord(Node source)
        {
            List<Edge> result = new List<Edge>();
            SingleSourceInitialize(source);
            for (int i = 0; i < Nodes.Count - 1; i++)
            {
                foreach (Edge e in Edges)
                {
                    Relax(e);
                }
            }
            foreach (Edge e in Edges)
            {

                if (e.dest.D > e.start.D + e.Weight)
                {
                    return null;
                }
                if (e.dest.Parent == e.start)
                {
                    result.Add(e);
                }
            }
            return result;
        }

        private void SingleSourceInitialize(Node source)
        {
            foreach (Node n in Nodes.Keys)
            {
                n.D = double.MaxValue;
                n.Parent = null;
            }
            source.D = 0;
        }

        public String StringifyEdges(List<Edge> list)
        {
            StringBuilder sb = new();
            foreach(Edge e in list)
            {
                sb.AppendLine($"{e.start.Data}--({e.Weight})--{e.dest.Data}");
            }
            return sb.ToString();
        }

        private void Relax(Edge e)
        {
            if (e.dest.D > (e.start.D + e.Weight))
            {
                e.dest.D = e.start.D + e.Weight;
                e.dest.Parent = e.start;
            }
        }

        public List<Edge>? ShortestPathBetweenTwoNodes(Node source, Node dest, bool print = false)
        {
            List<Edge>? minimalPaths = BelmanFord(source);
            if (minimalPaths == null)
                return null;
            StringBuilder sb = new();
            double totalWeight = 0;
            sb.AppendLine($"Path from {source.Data} to {dest.Data}:");
            Node tmp = dest;
            while (tmp != source)
            {
                Edge target = new Edge(tmp, tmp.Parent, 0);
                Edge found = tmp.Adjacent[0];
                foreach (Edge e in tmp.Adjacent)
                {
                    if (e.Equals(target))
                        found = e;
                }
                totalWeight += found.Weight;
                sb.AppendLine($"{tmp.Data}--({found.Weight})--{tmp.Parent.Data}");
                tmp = tmp.Parent;
            }
            sb.AppendLine($"Total weight is {totalWeight}");
            string resultingString = sb.ToString();
            if (print)
                Console.WriteLine( resultingString );
            return minimalPaths;
        }

        public string ShortestPathsPrint(Node source)
        {
            double totalWeight = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Source is {source.Data}");
            foreach (Node n in Nodes.Keys)
            {
                if (n == source)
                {
                    continue;
                }
                sb.AppendLine($"Path from {n.Data} to source:");
                Node tmp = n;
                while (tmp != source)
                {
                    Edge target = new Edge(tmp, tmp.Parent, 0);
                    Edge found = tmp.Adjacent[0];
                    foreach (Edge e in tmp.Adjacent)
                    {
                        if (e.Equals(target))
                            found = e;
                    }
                    totalWeight += found.Weight;
                    sb.AppendLine($"{tmp.Data}--({found.Weight})--{tmp.Parent.Data}");
                    tmp = tmp.Parent;
                }
                sb.AppendLine($"Total distance of node is {n.D}");
                sb.AppendLine();
            }
            sb.Append($"Total weight is: {totalWeight}");
            return sb.ToString();
        }
    }
    /*            foreach (Node node in Nodes.Keys)
            {
                if (node == source)
                {
                    continue;
                }
                Node tmp = node;
                while (tmp != source)
                {
                    if (tmp.Parent != null)
                    {
                        Edge target = new Edge(tmp, tmp.Parent, 0);

                        foreach (Edge e in node.Adjacent)
                        {
                            if (e == target)
                            {
                                result.Add(e);
                                break;
                            }
                        }
                        tmp = tmp.Parent;
                    }
                    else
                        break;
                }
            }*/
}
