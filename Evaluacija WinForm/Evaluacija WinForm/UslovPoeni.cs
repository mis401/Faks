using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacija_WinForm
{
    internal abstract class UslovPoeni :IUslov
    {

        protected double maxPoena;

        abstract public bool ispunjava(double poeni);

        public UslovPoeni(double max)
        {
            this.maxPoena = max;
        }
    }
}
