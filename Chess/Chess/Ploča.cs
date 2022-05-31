using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public static class Ploča
    {
        public static Polje[,] ploca = new Polje[8, 8];
        public static List<Polje> listaPolja = new List<Polje>();
        public static List<Figura> listaFiguraBijeli = new List<Figura>();
        public static List<Figura> listaFiguraCrni = new List<Figura>();
        public static bool statusIgre = true;
        public static bool jeŠah = false;
        public static bool neriješeno = false;
        public static Strana stranaUŠahu;
        public static List<string> triZadnjaPoteza = new List<string>();
    }
}
