namespace Prim
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DynamicGraph G = Generator(10, 15);
            Console.WriteLine(G+"\n\n\n\n");
            DynamicGraph MST = G.Prim(G.Nodes.ElementAt(5).Key);
            Console.WriteLine(MST);
        }

        public static DynamicGraph Generator(int n, int k)
        {
            DynamicGraph G = new DynamicGraph();
            Random r = new Random();
            for (int i = 0; i < n; i++)
            {
                G.InsertNode(i + 1);
            }
            int randomConnected = r.Next(n);

            foreach (Node node in G.Nodes.Keys)
            {
                G.InsertEdge(G.Nodes.ElementAt(randomConnected).Key, node, r.Next(1000));
            }

            for (int i = 0; i < k; i++)
            {
                if (!G.InsertEdge(G.Nodes.ElementAt(r.Next(n)).Key, G.Nodes.ElementAt(r.Next(n)).Key, r.Next(1000)))
                    i--;
            }

            return G;
        }
    }
}