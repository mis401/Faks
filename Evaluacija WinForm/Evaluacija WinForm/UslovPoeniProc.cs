using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacija_WinForm
{
    internal class UslovPoeniProc : UslovPoeni, IUslov
    {
        private double proc;
        public override bool ispunjava(double poeni)
        {
            return (poeni >= maxPoena * proc/100);
        }
        public UslovPoeniProc(double max, double proc) : base(max)
        {
            this.proc = proc;
        }
    }
}
