using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Evaluacija_WinForm
{
    internal class XMLStudent
    {
        
        public XMLStudent()
        {
            
        }
        public static List<Student> xmlReadStudents(string filename, out string error)
        {
            XmlDocument doc = new XmlDocument();
            List<Student> lista = new List<Student>();
            error = "No error";
            try
            {
                doc.LoadXml(filename);
            }
            catch (Exception ex)
            {
                error = "Doslo je do greske. Nemoguce je otvoriti ovaj fajl.";
                return null;
            }
            if (doc.DocumentElement == null)
            {
                error = "XML fajl je prazan";
                return null;
            }
            XmlNodeList listaStudenata = doc.ChildNodes;
            foreach (XmlNode node in listaStudenata)
            {
                string index = node["Indeks"].Value;
                string predmet = node["Predmet"].Value;
                double poeniUsmeni = double.Parse(node["Usmeni"].Value);
                List<ElemValues> elemValues = new List<ElemValues>();
                XmlNodeList elementi = node.SelectNodes("/Elementi/Item");
                foreach (XmlNode element in elementi)
                {
                    elemValues.Add(new ElemValues(element["Name"].InnerText, double.Parse(element["Value"].InnerText)));
                }
                lista.Add(new Student(index, predmet, elemValues));
            }
            return lista;
        }
    }
}
