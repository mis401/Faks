using Prim;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prim
{
    public class Edge : IComparable<Edge>
    {
        public Node start;
        public Node dest;
        public double Weight { get; set; }
        public Edge()
        {
            start = dest = null;
            Weight = 0;
        }

        public Edge(Node node1, Node node2, double w)
        {

            this.start = node1;
            this.dest = node2;
            Weight = w;
        }



        public override bool Equals(Object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                return false;
            else
            {
                Edge e2 = obj as Edge;
                return (start == e2.start && dest == e2.dest
                        || start == e2.dest && dest == e2.start);

            }
        }
        public static bool operator ==(Edge? e1, Edge? e2)
        {
            if (e1 == null || e2 == null)
                return false;
            return (e1.start == e2.start && e1.dest == e2.dest
                        || e1.start == e2.dest && e1.dest == e2.start);
        }

        public static bool operator !=(Edge? e1, Edge? e2)
        {
            if (e1 == null || e2 == null)
                return false;
            return !(e1.start == e2.start && e1.dest == e2.dest
            || e1.start == e2.dest && e1.dest == e2.start);
        }

        public override int GetHashCode()
        {
            return Weight.GetHashCode();
        }

        public int CompareTo(Edge? other)
        {
            if (other == null)
                return 1;
            return Weight.CompareTo(other.Weight);
        }

        public static bool operator <(Edge e1, Edge e2)
        {
            return e1.Weight < e2.Weight;
        }
        public static bool operator >(Edge e1, Edge e2)
        {
            return e1.Weight > e2.Weight;
        }
    }
}
