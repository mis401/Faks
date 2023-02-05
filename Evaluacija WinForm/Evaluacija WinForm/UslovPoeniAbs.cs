using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacija_WinForm
{
    internal class UslovPoeniAbs : UslovPoeni, IUslov
    {
        private double minPoena;
        public override bool ispunjava(double poeni)
        {
            return (poeni >= minPoena);
        }

        public UslovPoeniAbs(double max, double min) : base(max)
        {
            minPoena = min;    
        }
    }
}
