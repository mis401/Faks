using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacija_WinForm
{
    internal class Predmet
    {
        private string ime;
        private IElement usmeniIspit;
        private IStrategija strat;
        private KontrolerElemenata kontroler;

        public IStrategija Strat { get { return strat; } set { strat = value; } }
        public IElement UsmeniIspit { get { return usmeniIspit; } set { } }
        public KontrolerElemenata Kontroler { get { return kontroler; } set {} }

    }
}
