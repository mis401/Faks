using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacija_WinForm
{
    internal class Strategija
    {
        private int[] granice;
        public Strategija(params int[] granice)
        {
            this.granice = granice;
        }
        int oceni(double poeni)
        {
            if (poeni < granice[0])
                return 5;
            else if (poeni >= granice[0] && poeni <= granice[1])
                return 6;
            else if (poeni > granice[1] && poeni <= granice[2])
                return 7;
            else if (poeni > granice[2] && poeni <= granice[3])
                return 8;
            else if (poeni > granice[3] && poeni <= granice[4])
                return 9;
            else if (poeni > granice[4] && poeni <= granice[5])
                return 10;
            else
                return 0;
        }
    }
}
