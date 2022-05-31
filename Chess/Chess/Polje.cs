using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Polje
    {
        public int red;
        public int stupac;
        public ConsoleColor boja;
        public Figura figura;

        public Polje(int Red, int Stupac, ConsoleColor Boja)
        {
            red = Red;
            stupac = Stupac;
            boja = Boja;
            figura = null;
        }

    }
}
