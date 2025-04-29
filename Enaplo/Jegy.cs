using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enaplo
{
    public class Jegy
    {
        public Diak Diak { get; set; }
        public Tantargy Tantargy { get; set; }
        public int Ertek { get; set; }

        public Jegy() { }

        public Jegy(Diak diak, Tantargy tantargy, int ertek)
        {
            Diak = diak;
            Tantargy = tantargy;
            Ertek = ertek;
        }
    }
}
