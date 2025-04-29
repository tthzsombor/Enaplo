using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enaplo
{
    internal class Adatkezelo
    {
        
        public static List<Diak> BeolvasDiakok(string fajl)
        {
            return File.ReadAllLines(fajl)
                .Select(s => s.Split(';'))
                .Select(p => new Diak { Id = int.Parse(p[0]), Nev = p[1] })
                .ToList();
        }

        public static List<Tantargy> BeolvasTantargyak(string fajl)
        {
            return File.ReadAllLines(fajl)
                .Select(s => new Tantargy { Tantargynev = s.Trim() })
                .ToList();
        }

        public static List<Jegy> BeolvasJegyek(string fajl, List<Diak> diakok, List<Tantargy> tantargyak)
        {
            return File.ReadAllLines(fajl)
                .Select(s => s.Split(';'))
                .Select(p =>
                {
                    int diakId = int.Parse(p[0]);
                    string tantargyNev = p[1];
                    int ertek = int.Parse(p[2]);

                    var diak = diakok.FirstOrDefault(d => d.Id == diakId);
                    var tantargy = tantargyak.FirstOrDefault(t => t.Tantargynev == tantargyNev);

                    if (diak == null || tantargy == null)
                        throw new Exception("Érvénytelen adat a jegyek fájlban.");

                    return new Jegy
                    {
                        Diak = diak,
                        Tantargy = tantargy,
                        Ertek = ertek
                    };
                })
                .ToList();
        }

        public static void MentesDiakok(string fajl, List<Diak> diakok)
        {
            File.WriteAllLines(fajl, diakok.Select(d => $"{d.Id};{d.Nev}"));
        }

        public static void MentesTantargyak(string fajl, List<Tantargy> tantargyak)
        {
            File.WriteAllLines(fajl, tantargyak.Select(t => t.Tantargynev));
        }

        public static void MentesJegyek(string fajl, List<Jegy> jegyek)
        {
            File.WriteAllLines(fajl, jegyek.Select(j => $"{j.Diak.Id};{j.Tantargy.Tantargynev};{j.Ertek}"));
        }
    }
}
