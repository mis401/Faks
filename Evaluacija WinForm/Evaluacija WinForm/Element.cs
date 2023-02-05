using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacija_WinForm
{
    internal class Element : IElement
    {
        private string ime;

        private IUslov uslov;

        
        public string Ime { get { return ime; } set { ime = value; } }

        public IUslov Uslov { get { return uslov; } set { uslov = value; } }
        public bool ispunjavaUslov(double poeni)
        {
            return uslov.ispunjava(poeni);
        }

        public Element(string ime, IUslov uslov)
        {
            this.ime = ime;
            this.uslov = uslov;
        }
    }
}
