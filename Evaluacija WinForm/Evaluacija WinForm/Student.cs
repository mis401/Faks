using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Evaluacija_WinForm
{
    internal class Student
    {
        private string indeks;
        private string predmet;
        private List<ElemValues> rezultati;

        public string Indeks { get { return indeks; } set { indeks = value; } }
                
        public string Predmet { get { return predmet; } set { predmet = value; } } 
        public Student(string indeks, string predmet, List<ElemValues> rezultati)
        {
            this.indeks = indeks;
            this.rezultati = rezultati;
            this.predmet = predmet;
        }

        public override string ToString()
        {
            string s = indeks + " " + predmet;
            foreach (ElemValues v in rezultati)
            {
                s += " " + v.Name + " " + v.Value;
            }
            return s;
        }
    }
}
