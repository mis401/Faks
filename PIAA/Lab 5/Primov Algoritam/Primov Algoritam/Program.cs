using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Primov_Algoritam
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DynamicGraph G, Rand;
            Random r = new Random();
            List<Edge> MST;
            Stopwatch sw = new Stopwatch();

            /*            G = GenerateRandom(10, 20);
                        Console.WriteLine(G);
                        MST = G.Prim(G.Nodes.ElementAt(1).Key);
                        Console.WriteLine(G.PrintMST());*/

            for (int N = 100; N <= 100000; N = N * 10)
            {
                //random 2n
                G = GenerateRandom(N, 2 * N);
                sw.Restart();
                MST = G.Prim(G.Nodes.ElementAt(r.Next(N)).Key);
                sw.Stop();
                Console.WriteLine($"Uradjeno {N}, {2 * N}, operacija je trajala {sw.ElapsedTicks / 100.0}ns");

                //daisy random 2n
                G = GenerateWithDaisy(N, 2 * N);
                sw.Restart();
                MST = G.Prim(G.Nodes.ElementAt(r.Next(N)).Key);
                sw.Stop();
                Console.WriteLine($"Uradjeno sa daisy chainom {N}, {2 * N}, operacija je trajala {sw.ElapsedTicks / 100.0}ns");

                //random 5n
                G = GenerateRandom(N, 5 * N);
                sw.Restart();
                MST = G.Prim(G.Nodes.ElementAt(r.Next(N)).Key);
                sw.Stop();
                Console.WriteLine($"Uradjeno {N}, {5 * N}, operacija je trajala {sw.ElapsedTicks / 100.0}ns");

                //random daisy 5n
                G = GenerateWithDaisy(N, 5 * N);
                sw.Restart();
                MST = G.Prim(G.Nodes.ElementAt(r.Next(N)).Key);
                sw.Stop();
                Console.WriteLine($"Uradjeno sa daisy chainom {N}, {5 * N}, operacija je trajala {sw.ElapsedTicks / 100.0}ns");

                //random 10n
                G = GenerateRandom(N, 10 * N);
                sw.Restart();
                MST = G.Prim(G.Nodes.ElementAt(r.Next(N)).Key);
                sw.Stop();
                Console.WriteLine($"Uradjeno {N}, {10 * N}, operacija je trajala {sw.ElapsedTicks / 100.0}ns");

                //random daisy 10n
                G = GenerateWithDaisy(N, 10 * N);
                sw.Restart();
                MST = G.Prim(G.Nodes.ElementAt(r.Next(N)).Key);
                sw.Stop();
                Console.WriteLine($"Uradjeno sa daisty chainom {N}, {10 * N}, operacija je trajala {sw.ElapsedTicks / 100.0}ns");

                //random 20n
                G = GenerateRandom(N, 20 * N);
                sw.Restart();
                MST = G.Prim(G.Nodes.ElementAt(r.Next(N)).Key);
                sw.Stop();
                Console.WriteLine($"Uradjeno {N}, {20 * N} , operacija je trajala  {sw.ElapsedTicks / 100.0} ns");

                //random daisy 20n
                G = GenerateWithDaisy(N, 20 * N);
                sw.Restart();
                MST = G.Prim(G.Nodes.ElementAt(r.Next(N)).Key);
                sw.Stop();
                Console.WriteLine($"Uradjeno sa daisy chainom {N}, {20 * N} , operacija je trajala  {sw.ElapsedTicks / 100.0} ns");

                //random 33n
                G = GenerateRandom(N, 33 * N);
                sw.Restart();
                MST = G.Prim(G.Nodes.ElementAt(r.Next(N)).Key);
                sw.Stop();
                Console.WriteLine($"Uradjeno {N}, {33 * N}, operacija je trajala {sw.ElapsedTicks / 100.0}ns");

                //random daisy 33n
                G = GenerateWithDaisy(N, 33 * N);
                sw.Restart();
                MST = G.Prim(G.Nodes.ElementAt(r.Next(N)).Key);
                sw.Stop();
                Console.WriteLine($"Uradjeno sa daisy chainom {N}, {33 * N}, operacija je trajala {sw.ElapsedTicks / 100.0}ns");

                //random 50n
                G = GenerateRandom(N, 50 * N);
                sw.Restart();
                MST = G.Prim(G.Nodes.ElementAt(r.Next(N)).Key);
                sw.Stop();
                Console.WriteLine($"Uradjeno {N}, {50 * N}, operacija je trajala {sw.ElapsedTicks / 100.0}ns");

                //random daisy 50n
                G = GenerateWithDaisy(N, 50 * N);
                sw.Restart();
                MST = G.Prim(G.Nodes.ElementAt(r.Next(N)).Key);
                sw.Stop();
                Console.WriteLine($"Uradjeno sa daisy chainom {N}, {50 * N}, operacija je trajala {sw.ElapsedTicks / 100.0}ns");
            }

        }

        public static DynamicGraph GenerateRandom(int n, int k)
        {
            DynamicGraph G = new DynamicGraph();
            Random r = new Random();
            for (int i = 0; i < n; i++)
            {
                G.InsertNode(i);
            }
            int randomConnected = r.Next(n);
            
            foreach(Node node in G.Nodes.Keys)
            {
                G.InsertEdge(G.Nodes.ElementAt(randomConnected).Key, node, r.Next(1000));
            }

            for (int i = 0; i < Math.Min(k, n*(n-1)/2) - (n-1); i++)
            {
                if (!G.InsertEdge(G.Nodes.ElementAt(r.Next(n)).Key, G.Nodes.ElementAt(r.Next(n)).Key, r.Next(1000)))
                    i--;
            }

            return G;
        }

        public static DynamicGraph GenerateWithDaisy(int n, int k)
        {
            DynamicGraph G = new();
            Random r = new Random();

            for (int i = 0; i < n; i++)
            {
                G.InsertNode(i);
            }

            for (int i = 0; i < n-1; i++)
            {
                G.InsertEdge(G.Nodes.ElementAt(i).Key, G.Nodes.ElementAt(i + 1).Key, r.Next(n));
            }
            G.InsertEdge(G.Nodes.ElementAt(n-1).Key, G.Nodes.ElementAt(0).Key, r.Next(n));

            for (int i = 0; i < Math.Min(k, n * (n - 1) / 2) - n; i++)
            {
                if (!G.InsertEdge(G.Nodes.ElementAt(r.Next(n)).Key, G.Nodes.ElementAt(r.Next(n)).Key, r.Next(1000)))
                    i--;
            }
            return G;
        }
        public static DynamicGraph GeneratePrimer()
        {
            DynamicGraph G = new DynamicGraph();
            for (int i = 0; i < 8; i++)
            {
                G.InsertNode(new Node(i+65));
            }
            G.InsertEdge(G.Nodes.ElementAt(0).Key, G.Nodes.ElementAt(1).Key, 4);
            G.InsertEdge(G.Nodes.ElementAt(0).Key, G.Nodes.ElementAt(7).Key, 6);
            G.InsertEdge(G.Nodes.ElementAt(1).Key, G.Nodes.ElementAt(7).Key, 5);
            G.InsertEdge(G.Nodes.ElementAt(1).Key, G.Nodes.ElementAt(2).Key, 9);
            G.InsertEdge(G.Nodes.ElementAt(1).Key, G.Nodes.ElementAt(4).Key, 2);
            G.InsertEdge(G.Nodes.ElementAt(3).Key, G.Nodes.ElementAt(4).Key, 15);
            G.InsertEdge(G.Nodes.ElementAt(4).Key, G.Nodes.ElementAt(5).Key, 8);
            G.InsertEdge(G.Nodes.ElementAt(5).Key, G.Nodes.ElementAt(6).Key, 3);
            G.InsertEdge(G.Nodes.ElementAt(5).Key, G.Nodes.ElementAt(7).Key, 10);
            G.InsertEdge(G.Nodes.ElementAt(6).Key, G.Nodes.ElementAt(7).Key, 14);
            G.InsertEdge(G.Nodes.ElementAt(0).Key, G.Nodes.ElementAt(0).Key, 1);

            return G;
        }

        public static DynamicGraph GenerateComplete(int n)
        {
            DynamicGraph G = new DynamicGraph();
            Random r = new Random();
            for (int i = 0; i < n; i++)
            {
                G.InsertNode(i + 65);
            }

            foreach (Node node in G.Nodes.Keys)
            {
                int i = 0;
                while (node.Adjacent.Count != n - 1)
                {
                    G.InsertEdge(node, G.Nodes.ElementAt(i).Key, i*2);
                    i++;
                    i %= n;
                }
            }

            return G;
        }
    }
}