using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacija_WinForm
{
    internal interface IElement
    {
        string Ime { get; set; }

        IUslov Uslov { get; set; }
        bool ispunjavaUslov(double value);

    }
}
