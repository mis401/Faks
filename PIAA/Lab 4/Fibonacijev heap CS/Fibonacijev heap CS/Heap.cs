using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Fibonacijev_heap_CS
{
    internal class Heap
    {
        public Node? Min { get; set; }

        public Node? MostRecentlyAdded { get; set; }

        public int NumberOfNodes { get; set; }
        public Heap()
        {
            Min = null;
        }

        public void Insert(int data)
        {
            Node newNode = new Node(data);
            if (Min == null)//da li je struktura prazna
            {
                Min = newNode;
                newNode.Next = newNode;
                newNode.Previous = newNode;
            }
            else
            {
                
                Node.InsertIntoList(newNode, Min);
                
            }
            if (newNode.Data < Min.Data)
                Min = newNode;
            MostRecentlyAdded = newNode;
            NumberOfNodes++;
        }
        public Node? FindMin()
        {
            if (Min == null)
                return null;
            Node tmp = Min;
            Node newMin = Min;
            int min = Min.Data;
            do
            {
                if (tmp.Data < min)
                {
                    newMin = tmp;
                    min = tmp.Data;
                }
                tmp = tmp.Next;
            } while (tmp != Min);
            Min = newMin;
            return Min;
        }
        public Node RemoveMostRecent()
        {
            if (MostRecentlyAdded == null)
                return null;
            Node tmp = MostRecentlyAdded;
            if (tmp == Min)
            {
                Min = Min.Next;
            }
            if (Min == Min.Next)
            {
                Min = null;
            }
            Node.RemoveFromList(tmp);
            MostRecentlyAdded = null;
            NumberOfNodes--;
            FindMin();
            return tmp;
        }

        public Node? ExtractMin()
        {
            if (Min == null)
                return null;
            FindMin();
            Node tmp = Min;
            if (Min == Min.Next)
            {
                NumberOfNodes--;
                Min = null;
                return tmp;
            }

            if (Min.LeftMostChild != null)//prodji kroz listu dece, izvadi iz liste i stavi u root listu
            {
                // Node iterator = Min.LeftMostChild;
                /*                do
                                {
                                    Min.LeftMostChild = iterator.Next;
                                    Node.RemoveFromList(iterator);
                                    Node.InsertIntoList(iterator, Min);
                                    iterator = Min.LeftMostChild;
                                } while (iterator.Next != iterator);*/
                //Node.InsertIntoList(iterator, Min);
                Node left = Min.Previous;
                Node right = Min;
                Min.LeftMostChild.Previous.Next = right;
                right.Previous = Min.LeftMostChild.Previous;
                left.Next = Min.LeftMostChild;
                Min.LeftMostChild.Previous = left;
                Node iterate = left.Next;
                while (iterate != Min)
                {
                    iterate.Parent = null;
                    iterate = iterate.Next;
                }
                Min.LeftMostChild = null;
            }
            Min = tmp.Next;
            Node.RemoveFromList(tmp);
            NumberOfNodes--;
            Consolidate();
            FindMin();

            return tmp;
        }

        public void Consolidate()
        {
            if (Min == null)
                return;
            Node tmp = Min;
            int maxDegree = 0;
            do
            {
                if (tmp.Degree > maxDegree)
                    maxDegree = tmp.Degree;
                tmp = tmp.Next;
            } while (tmp != Min);//nalazimo najveci stepen
            int newMaxDegree = (int)Math.Floor(Math.Log2((double)NumberOfNodes));

            Node?[] degrees = new Node?[newMaxDegree + 1];
            for (int i = 0; i < newMaxDegree+1; i++)
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


        public Node MakeChild(Node node1, Node node2) // vraca parenta
        {
            if (node1.Data < node2.Data)//namestimo da node2 uvek bude manji node radi prostijeg koda, kacimo node1 na node2
            {
                Node tmp = node2;
                node2 = node1;
                node1 = tmp;
            }
            Node.RemoveFromList(node1);
            
            Node.InsertIntoList(node1, node2.LeftMostChild);
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


        private static void Print( Node n, int lvl)
        {
            Node node = n;
            List<Node> siblingsChildren = new List<Node>();

            Console.Write($"Nodes of level {lvl}, ");
            if (node.Parent != null)
            {
                Console.WriteLine($"children of {node.Parent.Data}");
            }
            else
            {
                Console.WriteLine();
            }
            Console.Write("=");
            do
            {
                Console.Write($"{node.Data}({node.Degree})=");
                if (node.LeftMostChild != null)
                {
                    siblingsChildren.Add(node.LeftMostChild);
                }
                node = node.Next;
            } while (node != n);
            Console.Write("\n____\n");
            foreach(Node child in siblingsChildren)
            {
                Heap.Print(child, lvl+1);
            }
        }

        public Node? RemoveNode(Node x)
        {
            if (Min == null)
                return null;
            if (x.Parent == null)
            {
                Node.RemoveFromList(x);
                return x;
            }
            else
            {
                UpdateNode(x, int.MinValue);
                return ExtractMin();
            }
        }

        //nisu neophodne ove naredne funkcije

        public void UpdateNode(Node target, int newValue)
        {
            target.Data = newValue;
            if (target.Parent == null || target.Data > target.Parent.Data)
            {
                return;
            }
            Cut(target);
            CascadingCut(target.Parent);
            FindMin();
        }

        public Node? RemoveFirst(int target)
        {
            Node? task = FindFirst(target);
            if (task == null)
                return null;
            UpdateNode(task, int.MinValue);
            return ExtractMin();
        }

        public List<Node?> RemoveAll(int target)
        {
            List<Node?> list = FindAll(target);
            List<Node?> result = new List<Node?>();
            foreach(Node n in list)
            {
                UpdateNode(n, int.MinValue);
                result.Add(ExtractMin());
            }
            return result;
        }

        public List<Node?> FindAll(int target)
        {
            if (Min == null)
                return null;
            List<Node?> result = new List<Node?>();
            result = FindNode(ref result, Min, target, false);
            return result;
        }

        public Node? FindFirst(int target)
        {   if (Min == null)
                return null;
            List<Node?> result = new List<Node?>();
            result = FindNode(ref result, Min, target, true);
            return result.FirstOrDefault();
        }
        private List<Node?> FindNode(ref List<Node?> list, Node n, int target, bool first)
        {
            Node task = n;
            do
            {
                if (task.Data == target)
                {
                    list.Add(task);
                    if (first == true)
                        return list;
                }
                if (task.Data <= target && task.LeftMostChild != null)
                    FindNode(ref list, task.LeftMostChild, target, first);
                task = task.Next;
            } while (task != Min);
            return list;
        }


        private void Cut(Node x)
        {
            if (Min == null)
                return;
            Node.RemoveFromList(x);
            
            Node.InsertIntoList(x, Min);
            
            x.Parent = null;
            x.Mark = false;
        }

        private void CascadingCut(Node x)
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
}
