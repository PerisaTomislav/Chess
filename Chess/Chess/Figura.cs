using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Figura
    {
        public string naziv;
        public string oznaka;
        public Strana strana;
        public int pozRed;
        public int pozStupac;
        public int brojKretnji;
        public ConsoleColor boja;
        public List<string> listaMogućihPoteza;

        public Figura(string Naziv, string Oznaka, Strana Strana, int PozRed, int PozStupac)
        {
            naziv = Naziv;
            oznaka = Oznaka;
            strana = Strana;
            brojKretnji = 0;
            pozRed = PozRed;
            pozStupac = PozStupac;
            if(strana == Strana.Bijeli)
            {
                boja = ConsoleColor.White;
            }
            else
            {
                boja = ConsoleColor.Black;
            }
            listaMogućihPoteza = new List<string>();
        }
    }
}
