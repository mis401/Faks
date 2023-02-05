using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primov_Algoritam
{
    public class FHeap
    {
        public FNode? Min { get; set; }

        public FNode? MostRecentlyAdded { get; set; }

        public int NumberOfNodes { get; set; }
        public FHeap()
        {
            Min = null;
        }

        public void Insert(Node n)
        {

            FNode newNode = new FNode(n);
            if (Min == null)//da li je struktura prazna
            {
                Min = newNode;
                newNode.Next = newNode;
                newNode.Previous = newNode;
            }
            else
            {

                FNode.InsertIntoList(newNode, Min);

            }
            if (newNode.Key < Min.Key)
                Min = newNode;

            n.FNode = newNode;
            MostRecentlyAdded = newNode;
            NumberOfNodes++;
        }
        public FNode? FindMin()
        {
            if (Min == null)
                return null;
            FNode tmp = Min;
            FNode newMin = Min;
            double min = Min.Key;
            do
            {
                if (tmp.Key < min)
                {
                    newMin = tmp;
                    min = tmp.Key;
                }
                tmp = tmp.Next;
            } while (tmp != Min);
            Min = newMin;
            return Min;
        }
        public FNode RemoveMostRecent()
        {
            if (MostRecentlyAdded == null)
                return null;
            FNode tmp = MostRecentlyAdded;
            if (tmp == Min)
            {
                Min = Min.Next;
            }
            if (Min == Min.Next)
            {
                Min = null;
            }
            FNode.RemoveFromList(tmp);
            MostRecentlyAdded = null;
            NumberOfNodes--;
            FindMin();
            return tmp;
        }

        public FNode? ExtractMin()
        {
            if (Min == null)
                return null;
            FindMin();
            FNode tmp = Min;
            if (NumberOfNodes==1)
            {
                NumberOfNodes--;
                Min = null;
                return tmp;
            }

            if (Min.LeftMostChild != null)//prodji kroz listu dece, izvadi iz liste i stavi u root listu
            {

                FNode left = Min.Previous;
                FNode right = Min;
                Min.LeftMostChild.Previous.Next = right;
                right.Previous = Min.LeftMostChild.Previous;
                left.Next = Min.LeftMostChild;
                Min.LeftMostChild.Previous = left;
                FNode iterate = left.Next;
                while (iterate != Min)
                {
                    iterate.Parent = null;
                    iterate = iterate.Next;
                }
                Min.LeftMostChild = null;
            }
            FNode MinPar = tmp.Parent;
            Min = tmp.Next;
            FNode.RemoveFromList(tmp);
            tmp.Parent = MinPar;
            NumberOfNodes--;
            Consolidate();
            FindMin();

            return tmp;
        }

        public void Consolidate()
        {

                FNode tmp;


                if (Min == null)
                    return;
                tmp = Min;
                int maxDegree = 0;
                do
                {
                    if (tmp.Degree > maxDegree)
                        maxDegree = tmp.Degree;
                    tmp = tmp.Next;
                } while (tmp != Min);//nalazimo najveci stepen
                int newMaxDegree = (int)Math.Floor(Math.Log2((double)NumberOfNodes));

                FNode?[] degrees = new FNode?[Math.Max(maxDegree, newMaxDegree) + 2];
                for (int i = 0; i < Math.Max(maxDegree, newMaxDegree) + 2; i++)
                {
                    degrees[i] = null;
                }

                tmp = Min;
                bool flag;
                do
                {
                    flag = false;
                    if (degrees[tmp.Degree] == null)
                    {
                        degrees[tmp.Degree] = tmp;
                        flag = true;
                    }

                    else if (degrees[tmp.Degree] != tmp)
                    {
                        int oldDegree = tmp.Degree;//makeChild menja stepen nodea pa mora da se upamti
                        tmp = MakeChild(tmp, degrees[tmp.Degree]);
                        Min = tmp; //min ostaje u root nivou
                        degrees[oldDegree] = null;
                        flag = true;
                        degrees[tmp.Degree] = tmp;
                    }
                    tmp = tmp.Next;
                } while (tmp != Min || flag == true);

        }


        public FNode MakeChild(FNode node1, FNode node2) // vraca parenta
        {
            if (node1.Key < node2.Key)//namestimo da node2 uvek bude manji node radi prostijeg koda, kacimo node1 na node2
            {
                FNode tmp = node2;
                node2 = node1;
                node1 = tmp;
            }
            FNode.RemoveFromList(node1);

            FNode.InsertIntoList(node1, node2.LeftMostChild);
            node2.LeftMostChild = node1;
            node1.Parent = node2;

            if (node1.Degree >= node2.Degree)
            {
                node2.Degree = node1.Degree + 1;
            }
            node1.Mark = false;
            return node2;
        }

        public void PrintHeap()
        {
            Console.WriteLine($"Number of nodes: {NumberOfNodes}");
            if (Min == null)
                return;
            int x = 0;
            Print(Min, x);

        }


        private static void Print(FNode n, int lvl)
        {
            FNode node = n;
            List<FNode> siblingsChildren = new List<FNode>();

            Console.Write($"Nodes of level {lvl}, ");
            if (node.Parent != null)
            {
                Console.WriteLine($"children of {node.Parent.Data.Data}");
            }
            else
            {
                Console.WriteLine();
            }
            Console.Write("=");
            do
            {
                Console.Write($"{node.Data.Data}({node.Degree})=");
                if (node.LeftMostChild != null)
                {
                    siblingsChildren.Add(node.LeftMostChild);
                }
                node = node.Next;
            } while (node != n);
            Console.Write("\n____\n");
            foreach (FNode child in siblingsChildren)
            {
                FHeap.Print(child, lvl + 1);
            }
        }

        public FNode? RemoveNode(FNode x)
        {
            if (Min == null)
                return null;
            if (x.Parent == null)
            {
                FNode.RemoveFromList(x);
                return x;
            }
            else
            {
                UpdateNode(x, double.MinValue);
                return ExtractMin();
            }
        }



        public void UpdateNode(FNode target, double newKey)
        {
            target.Key = newKey;
            if (target.Parent != null && target.Key < target.Parent.Key)
            {
                Cut(target);
                CascadingCut(target.Parent);
            }
            FindMin();
        }


        private void Cut(FNode x)
        {
            try
            {
                if (Min == null)
                    return;

                if (x.Parent == null)
                    return;
                FNode par = x.Parent;
                FNode tmp;
                if (x.LeftMostChild != null)
                {
                    FNode left = par.LeftMostChild.Previous;
                    FNode right = par.LeftMostChild;
                    x.LeftMostChild.Previous.Next = right;
                    right.Previous = x.LeftMostChild.Previous;
                    left.Next = x.LeftMostChild;
                    x.LeftMostChild.Previous = left;
                    FNode iterate = left.Next;
                    while (iterate != par.LeftMostChild)
                    {
                        iterate.Parent = par;
                        iterate = iterate.Next;
                    }
                }

                if (par.LeftMostChild == par.LeftMostChild.Previous)
                {
                    par.LeftMostChild = null;
                    par.Degree--;
                }
                if (par.LeftMostChild == x)
                    par.LeftMostChild = par.LeftMostChild.Next;
                FNode.RemoveFromList(x);

                if (par.LeftMostChild != null)
                {
                    int deg = 0;
                    tmp = par.LeftMostChild;
                    do
                    {
                        tmp.Parent = par;
                        if (tmp.Degree > deg)
                        {
                            deg = tmp.Degree;
                        }
                        tmp = tmp.Next;
                    } while (tmp != par.LeftMostChild);
                    par.Degree = deg + 1;
                }
                FNode.InsertIntoList(x, Min);
                x.LeftMostChild = null;
                x.Degree = 0;
                x.Parent = null;
                x.Mark = false;

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void CascadingCut(FNode x)
        {
            if (x != null)
            {
                if (x.Parent != null)
                {
                    if (x.Mark == false)
                        x.Mark = true;
                    else
                    {
                        Cut(x);
                        CascadingCut(x.Parent);
                    }
                }
            }
        }
        public FNode? RemoveFirst(int target)
        {
            FNode? task = FindFirst(target);
            if (task == null)
                return null;
            UpdateNode(task, double.MinValue);
            return ExtractMin();
        }

        public List<FNode?> RemoveAll(int target)
        {
            List<FNode?> list = FindAll(target);
            List<FNode?> result = new List<FNode?>();
            foreach (FNode n in list)
            {
                UpdateNode(n, double.MinValue);
                result.Add(ExtractMin());
            }
            return result;
        }

        public List<FNode?> FindAll(int target)
        {
            if (Min == null)
                return null;
            List<FNode?> result = new List<FNode?>();
            result = FindNode(ref result, Min, target, false);
            return result;
        }

        public FNode? FindFirst(int target)
        {
            if (Min == null)
                return null;
            List<FNode?> result = new List<FNode?>();
            result = FindNode(ref result, Min, target, true);
            return result.FirstOrDefault();
        }
        private List<FNode?> FindNode(ref List<FNode?> list, FNode n, int target, bool first)
        {
            FNode task = n;
            do
            {
                if (task.Key == target)
                {
                    list.Add(task);
                    if (first == true)
                        return list;
                }
                if (task.Key <= target && task.LeftMostChild != null)
                    FindNode(ref list, task.LeftMostChild, target, first);
                task = task.Next;
            } while (task != Min);
            return list;
        }

    }
}
