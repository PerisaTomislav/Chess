using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public enum Strana {Bijeli,Crni}
    public enum Boja {Bijela,Crna}
    class Program
    {
        static void Main(string[] args)
        {
            //Glavni meni
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            string izbor;
            do
            {
                Console.Clear();
                Console.WriteLine("ŠAH");
                Console.WriteLine("---");
                Console.WriteLine();
                Console.WriteLine("1.Nova igra");
                Console.WriteLine("2.Nastavi");
                Console.WriteLine("3.Analiziraj");
                Console.WriteLine("4.Pomoć");
                Console.WriteLine("5.Postavke");
                Console.WriteLine("6.Izlaz");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Odaberite opciju: ");
                izbor = Console.ReadLine();
                Console.Clear();
                //Opcije
                switch (izbor)
                {
                    case "1": Console.Clear(); Funkcionalnosti.NovaIgra(); break;
                    case "2": Console.Clear(); Funkcionalnosti.Nastavi(); break;
                    case "3": Console.Clear(); Funkcionalnosti.Analiziraj(); break;
                    case "4": Console.Clear(); Funkcionalnosti.Pomoć(); break;
                    case "5": Console.Clear(); Funkcionalnosti.Postavke(); break;
                }
            } while (izbor != "6");
        }
    }
}
