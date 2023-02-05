using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacija_WinForm
{
    internal class KontrolerElemenata
    {
        private IElement[] elementi;

        public KontrolerElemenata(params IElement[] elems)
        {
            elementi = new IElement[elems.Length];
            for (int i = 0; i < elems.Length; i++)
            {
                elementi[i] = elems[i];
            }
        }
        public bool ispunjeniUslovi(List<ElemValues> rezultatiStudenta)
        {
            bool flag = true;
            foreach(IElement el in elementi)
            {
                foreach(ElemValues elVal in rezultatiStudenta)
                {
                    if (el.Ime == elVal.Name)
                    {
                        if (el.ispunjavaUslov(elVal.Value) == false)
                        {
                            flag = false;
                            break;
                        }   
                    }
                }
                if (!flag)
                    break;
            }
            return flag;
        }
        public double brojPoena(List<ElemValues> rezultatiStudenta)
        {
            double poeni = 0;
            foreach(ElemValues el in rezultatiStudenta)
            {
                poeni += el.Value;
            }
            return poeni;
        }
    }
}
