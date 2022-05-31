using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Chess
{
    public static class Funkcionalnosti
    {
        public static void NovaIgra()
        {
            //Početak igre
            //Biranje vrste igre (PVP/PVE)
            Console.Clear();
            Console.Write("Izaberite vrstu igre(PvP/PvE): ");
            string izborVrsteIgre = Console.ReadLine();
            Metode.PripremiPločuZaIgru();
            //PVP
            if (izborVrsteIgre.ToUpper() == "PVP")
            {
                int brojac = 0;
                Console.Clear();
                Metode.KreirajFigure();
                Metode.KreirajPloču();
                Metode.PosložiFigure();
                //Metode.IspišiPločuPvP(brojac);
                string potezIgrača = "";
                while (Ploča.statusIgre == true)
                {
                    //Bijeli je na potezu
                    if (brojac % 2 == 0)
                    {
                        do
                        {
                            //Izračunavanje mogućih poteza
                            Metode.OčistiPotezeFigura();
                            Metode.IzračunajMogućePoteze(Strana.Bijeli);
                            Metode.IzračunajMogućePoteze(Strana.Crni);
                            Metode.ProvjeriŠah(Strana.Bijeli);
                            Metode.ProvjeriJelNeriješeno(Strana.Bijeli);
                            if (Ploča.statusIgre == false || Ploča.neriješeno==true) break;
                            //Ispis ploče
                            Console.Clear();
                            Metode.IspišiPločuPvP(brojac);
                            Console.WriteLine();
                            if(Ploča.jeŠah == true && Ploča.stranaUŠahu == Strana.Bijeli)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Šah!");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.WriteLine();
                            }
                            Console.Write("Bijeli igra: ");
                            //Unos poteza
                            potezIgrača = Console.ReadLine();
                            //Provjera poteza
                            if(Metode.ProvjeriPotez(Strana.Bijeli, potezIgrača) == "")
                            {
                                Console.Clear();
                                //Console.WriteLine("Taj potez nije moguće odigrati!");
                                Process.Start("https://www.youtube.com/watch?v=7qnd-hdmgfk");
                                Console.ReadKey();
                            }
                        } while (Metode.ProvjeriPotez(Strana.Bijeli,potezIgrača) == "");
                        if (Ploča.statusIgre == false || Ploča.neriješeno==true) break;
                        //Odigravanje poteza ako se može odigrati
                        Metode.OdigrajPotez(Strana.Bijeli, potezIgrača);
                        Metode.ProvjeriThreefoldRepeticiju();
                        Metode.ProvjeriJelNeriješeno(Strana.Bijeli);
                        Metode.ProvjeriIIzvršiPromociju(Strana.Bijeli);
                    }
                    if (Ploča.statusIgre == false || Ploča.neriješeno==true) break;
                    //Crni je na potezu
                    if(brojac%2==1)
                    {
                        do
                        {
                            //Izračunavanje mogućih poteza
                            Metode.OčistiPotezeFigura();
                            Metode.IzračunajMogućePoteze(Strana.Crni);
                            Metode.IzračunajMogućePoteze(Strana.Bijeli);
                            Metode.ProvjeriŠah(Strana.Crni);
                            Metode.ProvjeriJelNeriješeno(Strana.Crni);
                            if (Ploča.statusIgre == false || Ploča.neriješeno==true) break;
                            //Ispis ploče
                            Console.Clear();
                            Metode.IspišiPločuPvP(brojac);
                            Console.WriteLine();
                            if (Ploča.jeŠah == true && Ploča.stranaUŠahu == Strana.Crni)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Šah!");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.WriteLine();
                            }
                            Console.Write("Crni igra: ");
                            //Unos poteza
                            potezIgrača = Console.ReadLine();
                            //Provjera poteza
                            if (Metode.ProvjeriPotez(Strana.Crni, potezIgrača) == "")
                            {
                                Console.Clear();
                                //Console.WriteLine("Taj potez nije moguće odigrati!");
                                Process.Start("https://www.youtube.com/watch?v=7qnd-hdmgfk");
                                Console.ReadKey();
                            }
                        } while (Metode.ProvjeriPotez(Strana.Crni, potezIgrača) == "");
                        if (Ploča.statusIgre == false || Ploča.neriješeno==true) break;
                        //Odigravanje poteza ako se može odigrati
                        Metode.OdigrajPotez(Strana.Crni, potezIgrača);
                        Metode.ProvjeriThreefoldRepeticiju();
                        Metode.ProvjeriJelNeriješeno(Strana.Bijeli);
                        Metode.ProvjeriIIzvršiPromociju(Strana.Crni);
                    }
                    brojac++;
                }
                Console.Clear();
                Console.WriteLine("Završetak igre!");
                if (Ploča.jeŠah==true && Ploča.stranaUŠahu == Strana.Bijeli)
                {
                    Console.WriteLine("Pobjednik je Crni!");
                }
                if(Ploča.jeŠah==true && Ploča.stranaUŠahu == Strana.Crni)
                {
                    Console.WriteLine("Pobjednik je Bijeli!");
                }
                if (Ploča.neriješeno == true && Ploča.jeŠah == false)
                {
                    Console.WriteLine("Neriješeno!");
                }
                Console.ReadKey();
                Metode.OčistiPloču();
            }
            //PVE
            if (izborVrsteIgre.ToUpper() == "PVE")
            {
                Console.Clear();
                //Biranje strane (B/C)
                Console.Write("Izaberite stranu (B/C): ");
                string izborStrane = Console.ReadLine();
                //Igrač je bijeli
                if (izborStrane == "B" || izborStrane == "b")
                {
                    Console.Clear();
                    Metode.KreirajFigure();
                    Metode.KreirajPloču();
                    Metode.PosložiFigure();
                    Metode.IspišiPločuPvE(Strana.Bijeli);
                    Console.ReadKey();
                    while (Ploča.statusIgre == true)
                    {
                        string potezIgrača = Console.ReadLine();
                        Metode.OdigrajPotezBOT();
                    }
                }
                //Igrač je crni
                if (izborStrane == "C" || izborStrane == "c")
                {
                    Console.Clear();
                    Metode.KreirajFigure();
                    Metode.KreirajPloču();
                    Metode.PosložiFigure();
                    Metode.IspišiPločuPvE(Strana.Crni);
                    Console.ReadKey();
                    while (Ploča.statusIgre == true)
                    {
                        Metode.OdigrajPotezBOT();
                        string potezIgrača = Console.ReadLine();
                        for (int i = 0; i < 3; i++)
                        {
                            //treba provjerit unos igrača, proučit šahovsku notaciju do kraja
                        }
                    }
                }
            }
        }



        public static void Nastavi()
        {

        }



        public static void Analiziraj()
        {

        }



        public static void Pomoć()
        {

        }



        public static void Postavke()
        {

        }
    }
}
