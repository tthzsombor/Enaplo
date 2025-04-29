using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enaplo
{
    public class Diak
    {
        public int Id { get; set; }
        public string Nev { get; set; }

        public Diak() { }

        public Diak(int id, string nev)
        {
            Id = id;
            Nev = nev;
        }
    }
}
