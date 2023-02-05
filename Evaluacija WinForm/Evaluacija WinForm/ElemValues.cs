using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacija_WinForm
{
    internal struct ElemValues
    {
        private string name;
        private double value;
        public string Name { get { return name; } set { } }

        public double Value { get { return value; } set { } }

        public ElemValues(string ime, double poeni)
        {
            this.name = ime;
            this.value = poeni;
        }
    }
}
