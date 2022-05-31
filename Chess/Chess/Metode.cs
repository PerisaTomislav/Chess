using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public static class Metode
    {
        public static void PripremiPločuZaIgru()
        {
            Ploča.listaFiguraBijeli.Clear();
            Ploča.listaFiguraCrni.Clear();
            Ploča.jeŠah = false;
            Ploča.statusIgre = true;
            Ploča.neriješeno = false;
            Ploča.triZadnjaPoteza.Clear();
        }

        

        public static void OčistiPloču()
        {
            for (int red = 0; red < 8; red++)
            {
                for (int stupac = 0; stupac < 8; stupac++)
                {
                    if (Ploča.ploca[red, stupac].figura != null) Ploča.ploca[red, stupac].figura = null;
                }
            }
        }



        public static void KreirajFigure()
        {
            //Atributi
            string naziv;
            string oznakaBijeli;
            string oznakaCrni;

            int pozStupac;
            Strana strana;
            //Kreiranje pijuna
            for(int j = 0; j < 8; j++)
            {
                naziv = "Pijun";
                oznakaBijeli = "♙";
                oznakaCrni = "♙";
                
                strana = Strana.Bijeli;
                pozStupac = j;
                Ploča.listaFiguraBijeli.Add(new Figura(naziv, oznakaBijeli, strana, 6, pozStupac));
                
                strana = Strana.Crni;
                pozStupac = j;
                Ploča.listaFiguraCrni.Add(new Figura(naziv, oznakaCrni, strana, 1, pozStupac));
                
            }
            //Ostale bijele figure
            Ploča.listaFiguraBijeli.Add(new Figura("Top", "♜", Strana.Bijeli,7,0));
            Ploča.listaFiguraBijeli.Add(new Figura("Top", "♜", Strana.Bijeli, 7, 7));
            Ploča.listaFiguraBijeli.Add(new Figura("Skakač", "♞", Strana.Bijeli, 7, 1));
            Ploča.listaFiguraBijeli.Add(new Figura("Skakač", "♞", Strana.Bijeli, 7, 6));
            Ploča.listaFiguraBijeli.Add(new Figura("Lovac", "♝", Strana.Bijeli, 7, 2));
            Ploča.listaFiguraBijeli.Add(new Figura("Lovac", "♝", Strana.Bijeli, 7, 5));
            Ploča.listaFiguraBijeli.Add(new Figura("Kralj", "♚", Strana.Bijeli, 7, 4));
            Ploča.listaFiguraBijeli.Add(new Figura("Kraljica", "♛", Strana.Bijeli, 7, 3));
            //Ostale crne figure
            Ploča.listaFiguraCrni.Add(new Figura("Top", "♜", Strana.Crni, 0, 0));
            Ploča.listaFiguraCrni.Add(new Figura("Top", "♜", Strana.Crni, 0, 7));
            Ploča.listaFiguraCrni.Add(new Figura("Skakač", "♞", Strana.Crni, 0, 1));
            Ploča.listaFiguraCrni.Add(new Figura("Skakač", "♞", Strana.Crni, 0, 6));
            Ploča.listaFiguraCrni.Add(new Figura("Lovac", "♝", Strana.Crni, 0, 2));
            Ploča.listaFiguraCrni.Add(new Figura("Lovac", "♝", Strana.Crni, 0, 5));
            Ploča.listaFiguraCrni.Add(new Figura("Kralj", "♚", Strana.Crni, 0, 4));
            Ploča.listaFiguraCrni.Add(new Figura("Kraljica", "♛", Strana.Crni, 0, 3));
        } 



        public static void KreirajPloču()
        {
            int brojac = 0;
            ConsoleColor bojaPolja;
            for(int red = 0; red < 8; red++)
            {
                for(int stupac = 0; stupac < 8; stupac++)
                {
                    if (brojac % 2 == 0)
                    {
                        bojaPolja = ConsoleColor.Gray;
                    }
                    else
                    {
                        bojaPolja = ConsoleColor.DarkGray;
                    }
                    Polje polje = new Polje(red, stupac, bojaPolja);
                    Ploča.ploca[red, stupac] = polje;
                    Ploča.listaPolja.Add(polje);
                    brojac++;
                }
                brojac--;
            }
        }



        public static void PosložiFigure()
        {
            foreach(Figura figurica in Ploča.listaFiguraBijeli)
            {
                foreach(Polje polje in Ploča.listaPolja)
                {
                    if(polje.red==figurica.pozRed && polje.stupac == figurica.pozStupac)
                    {
                        polje.figura = figurica;
                    }
                }
            }
            foreach (Figura figurica in Ploča.listaFiguraCrni)
            {
                foreach (Polje polje in Ploča.listaPolja)
                {
                    if (polje.red == figurica.pozRed && polje.stupac == figurica.pozStupac)
                    {
                        polje.figura = figurica;
                    }
                }
            }
        }



        //Metoda za ispis, poziva metode ispis bijeli i ispis crni ovisno o strani koja je na potezu
        public static void IspišiPločuPvP(int brojac)
        {
            if (brojac % 2 == 0)
            {
                IspisBijeli();
            }
            else
            {
                IspisCrni();
            }
        }



        public static void IspišiPločuPvE(Strana Strana)
        {
            //Igrač je bijeli
            if(Strana == Strana.Bijeli)
            {
                IspisBijeli();
            }
            //Igrač je crni
            else
            {
                IspisCrni();
            }
        }



        public static void IzračunajMogućePoteze(Strana Strana)
        {
            //Bijele figure
            if(Strana == Strana.Bijeli)
            {
                foreach (Figura figura in Ploča.listaFiguraBijeli)
                {
                    figura.listaMogućihPoteza.Clear();
                    //Pijun
                    if (figura.naziv == "Pijun")
                    {
                        foreach (Polje polje in Ploča.listaPolja)
                        {
                            if (polje.figura == null && polje.stupac == figura.pozStupac && polje.red == figura.pozRed - 1)
                            {
                                figura.listaMogućihPoteza.Add(polje.red.ToString() + polje.stupac.ToString());
                                if (figura.pozRed == 6 && polje.figura == null)
                                {
                                    foreach (Polje poljeIza in Ploča.listaPolja)
                                    {
                                        if (poljeIza.stupac == polje.stupac && poljeIza.red == polje.red - 1 && poljeIza.figura == null)
                                        {
                                            figura.listaMogućihPoteza.Add(poljeIza.red.ToString() + poljeIza.stupac.ToString());
                                        }
                                    }
                                }
                            }
                            if (polje.red == figura.pozRed - 1 && ((polje.stupac == figura.pozStupac - 1) || (polje.stupac == figura.pozStupac + 1)) && polje.figura != null && polje.figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add(polje.red.ToString() + polje.stupac.ToString());
                            }
                        }
                    }
                    //Top
                    if (figura.naziv == "Top")
                    {
                        int pocetniRed = figura.pozRed;
                        int pocetniStupac = figura.pozStupac;
                        bool kraj = false;
                        //Kretnja prema gore
                        for (int promatraniRed = pocetniRed - 1; promatraniRed >= 0; promatraniRed--)
                        {
                            if (promatraniRed >= 0 && kraj == false)
                            {
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura == null)
                                {
                                    figura.listaMogućihPoteza.Add(promatraniRed.ToString() + pocetniStupac.ToString());
                                }
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura != null && Ploča.ploca[promatraniRed, pocetniStupac].figura.strana == Strana.Bijeli)
                                {
                                    kraj = true;
                                }
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura != null && Ploča.ploca[promatraniRed, pocetniStupac].figura.strana == Strana.Crni)
                                {
                                    figura.listaMogućihPoteza.Add(promatraniRed.ToString() + pocetniStupac.ToString());
                                    kraj = true;
                                }
                            }
                        }
                        //Kretnja prema dolje
                        kraj = false;
                        for (int promatraniRed = pocetniRed + 1; promatraniRed <= 7; promatraniRed++)
                        {
                            if (promatraniRed <= 7 && kraj == false)
                            {
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura == null)
                                {
                                    figura.listaMogućihPoteza.Add(promatraniRed.ToString() + pocetniStupac.ToString());
                                }
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura != null && Ploča.ploca[promatraniRed, pocetniStupac].figura.strana == Strana.Bijeli)
                                {
                                    kraj = true;
                                }
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura != null && Ploča.ploca[promatraniRed, pocetniStupac].figura.strana == Strana.Crni)
                                {
                                    figura.listaMogućihPoteza.Add(promatraniRed.ToString() + pocetniStupac.ToString());
                                    kraj = true;
                                }
                            }
                        }
                        //Kretnja prema lijevo
                        kraj = false;
                        for (int promatraniStupac = pocetniStupac - 1; promatraniStupac >= 0; promatraniStupac--)
                        {
                            if (promatraniStupac >= 0 && kraj == false)
                            {
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura == null)
                                {
                                    figura.listaMogućihPoteza.Add(pocetniRed.ToString() + promatraniStupac.ToString());
                                }
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura != null && Ploča.ploca[pocetniRed, promatraniStupac].figura.strana == Strana.Bijeli)
                                {
                                    kraj = true;
                                }
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura != null && Ploča.ploca[pocetniRed, promatraniStupac].figura.strana == Strana.Crni)
                                {
                                    figura.listaMogućihPoteza.Add(pocetniRed.ToString() + promatraniStupac.ToString());
                                    kraj = true;
                                }
                            }
                        }
                        //Kretnja prema desno
                        kraj = false;
                        for (int promatraniStupac = pocetniStupac + 1; promatraniStupac <= 7; promatraniStupac++)
                        {
                            if (promatraniStupac <= 7 && kraj == false)
                            {
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura == null)
                                {
                                    figura.listaMogućihPoteza.Add(pocetniRed.ToString() + promatraniStupac.ToString());
                                }
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura != null && Ploča.ploca[pocetniRed, promatraniStupac].figura.strana == Strana.Bijeli)
                                {
                                    kraj = true;
                                }
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura != null && Ploča.ploca[pocetniRed, promatraniStupac].figura.strana == Strana.Crni)
                                {
                                    figura.listaMogućihPoteza.Add(pocetniRed.ToString() + promatraniStupac.ToString());
                                    kraj = true;
                                }
                            }
                        }
                    }
                    //Skakač
                    if (figura.naziv == "Skakač")
                    {
                        //Desno 2 dolje 1
                        if (figura.pozRed + 1 <= 7 && figura.pozStupac + 2 <= 7)
                        {
                            if (Ploča.ploca[figura.pozRed + 1, figura.pozStupac + 2].figura == null || Ploča.ploca[figura.pozRed + 1, figura.pozStupac + 2].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed + 1).ToString() + (figura.pozStupac + 2).ToString());
                            }
                        }
                        //Desno 2 gore 1 
                        if (figura.pozRed - 1 >= 0 && figura.pozStupac + 2 <= 7)
                        {
                            if (Ploča.ploca[figura.pozRed - 1, figura.pozStupac + 2].figura == null || Ploča.ploca[figura.pozRed - 1, figura.pozStupac + 2].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed - 1).ToString() + (figura.pozStupac + 2).ToString());
                            }
                        }
                        //Dolje 2 desno 1
                        if (figura.pozRed + 2 <= 7 && figura.pozStupac + 1 <= 7)
                        {
                            if (Ploča.ploca[figura.pozRed + 2, figura.pozStupac + 1].figura == null || Ploča.ploca[figura.pozRed + 2, figura.pozStupac + 1].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed + 2).ToString() + (figura.pozStupac + 1).ToString());
                            }
                        }
                        //Dolje 2 Lijevo 1
                        if (figura.pozRed + 2 <= 7 && figura.pozStupac - 1 >= 0)
                        {
                            if (Ploča.ploca[figura.pozRed + 2, figura.pozStupac - 1].figura == null || Ploča.ploca[figura.pozRed + 2, figura.pozStupac - 1].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed + 2).ToString() + (figura.pozStupac - 1).ToString());
                            }
                        }
                        //Lijevo 2 gore 1
                        if (figura.pozRed - 1 >= 0 && figura.pozStupac - 2 >= 0)
                        {
                            if (Ploča.ploca[figura.pozRed - 1, figura.pozStupac - 2].figura == null || Ploča.ploca[figura.pozRed - 1, figura.pozStupac - 2].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed - 1).ToString() + (figura.pozStupac - 2).ToString());
                            }
                        }
                        //Lijevo 2 dolje 1
                        if (figura.pozRed + 1 <= 7 && figura.pozStupac - 2 >= 0)
                        {
                            if (Ploča.ploca[figura.pozRed + 1, figura.pozStupac - 2].figura == null || Ploča.ploca[figura.pozRed + 1, figura.pozStupac - 2].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed + 1).ToString() + (figura.pozStupac - 2).ToString());
                            }
                        }
                        //Gore 2 lijevo 1
                        if (figura.pozRed - 2 >= 0 && figura.pozStupac - 1 >= 0)
                        {
                            if (Ploča.ploca[figura.pozRed - 2, figura.pozStupac - 1].figura == null || Ploča.ploca[figura.pozRed - 2, figura.pozStupac - 1].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed - 2).ToString() + (figura.pozStupac - 1).ToString());
                            }
                        }
                        //Gore 2 desno 1
                        if (figura.pozRed - 2 >= 0 && figura.pozStupac + 1 <= 7)
                        {
                            if (Ploča.ploca[figura.pozRed - 2, figura.pozStupac + 1].figura == null || Ploča.ploca[figura.pozRed - 2, figura.pozStupac + 1].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed - 2).ToString() + (figura.pozStupac + 1).ToString());
                            }
                        }
                    }
                    //Lovac
                    if (figura.naziv == "Lovac")
                    {
                        int promatraniRed;
                        int promatraniStupac;
                        bool kraj;
                        //Gore desno
                        kraj = false;
                        promatraniRed = figura.pozRed - 1;
                        promatraniStupac = figura.pozStupac + 1;
                        while (promatraniRed >= 0 && promatraniStupac <= 7 && kraj == false)
                        {
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura == null)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed.ToString() + promatraniStupac.ToString());
                            }
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura != null && Ploča.ploca[promatraniRed, promatraniStupac].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed.ToString() + promatraniStupac.ToString());
                                kraj = true;
                            }
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura != null && Ploča.ploca[promatraniRed, promatraniStupac].figura.strana == Strana.Bijeli)
                            {
                                kraj = true;
                            }
                            promatraniRed--;
                            promatraniStupac++;
                        }
                        //Gore lijevo
                        kraj = false;
                        promatraniRed = figura.pozRed - 1;
                        promatraniStupac = figura.pozStupac - 1;
                        while (promatraniRed >= 0 && promatraniStupac >= 0 && kraj == false)
                        {
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura == null)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed.ToString() + promatraniStupac.ToString());
                            }
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura != null && Ploča.ploca[promatraniRed, promatraniStupac].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed.ToString() + promatraniStupac.ToString());
                                kraj = true;
                            }
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura != null && Ploča.ploca[promatraniRed, promatraniStupac].figura.strana == Strana.Bijeli)
                            {
                                kraj = true;
                            }
                            promatraniRed--;
                            promatraniStupac--;
                        }
                        //Dolje desno
                        kraj = false;
                        promatraniRed = figura.pozRed + 1;
                        promatraniStupac = figura.pozStupac + 1;
                        while (promatraniRed <= 7 && promatraniStupac <= 7 && kraj == false)
                        {
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura == null)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed.ToString() + promatraniStupac.ToString());
                            }
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura != null && Ploča.ploca[promatraniRed, promatraniStupac].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed.ToString() + promatraniStupac.ToString());
                                kraj = true;
                            }
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura != null && Ploča.ploca[promatraniRed, promatraniStupac].figura.strana == Strana.Bijeli)
                            {
                                kraj = true;
                            }
                            promatraniRed++;
                            promatraniStupac++;
                        }
                        //Dolje lijevo
                        kraj = false;
                        promatraniRed = figura.pozRed + 1;
                        promatraniStupac = figura.pozStupac - 1;
                        while (promatraniRed <= 7 && promatraniStupac >= 0 && kraj == false)
                        {
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura == null)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed.ToString() + promatraniStupac.ToString());
                            }
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura != null && Ploča.ploca[promatraniRed, promatraniStupac].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed.ToString() + promatraniStupac.ToString());
                                kraj = true;
                            }
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura != null && Ploča.ploca[promatraniRed, promatraniStupac].figura.strana == Strana.Bijeli)
                            {
                                kraj = true;
                            }
                            promatraniRed++;
                            promatraniStupac--;
                        }
                    }
                    //Kraljica
                    if (figura.naziv == "Kraljica")
                    {
                        int pocetniRed = figura.pozRed;
                        int pocetniStupac = figura.pozStupac;
                        bool kraj = false;
                        //Kretnja prema gore
                        for (int promatraniRed = pocetniRed - 1; promatraniRed >= 0; promatraniRed--)
                        {
                            if (promatraniRed >= 0 && kraj == false)
                            {
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura == null)
                                {
                                    figura.listaMogućihPoteza.Add(promatraniRed.ToString() + pocetniStupac.ToString());
                                }
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura != null && Ploča.ploca[promatraniRed, pocetniStupac].figura.strana == Strana.Bijeli)
                                {
                                    kraj = true;
                                }
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura != null && Ploča.ploca[promatraniRed, pocetniStupac].figura.strana == Strana.Crni)
                                {
                                    figura.listaMogućihPoteza.Add(promatraniRed.ToString() + pocetniStupac.ToString());
                                    kraj = true;
                                }
                            }
                        }
                        //Kretnja prema dolje
                        kraj = false;
                        for (int promatraniRed = pocetniRed + 1; promatraniRed <= 7; promatraniRed++)
                        {
                            if (promatraniRed <= 7 && kraj == false)
                            {
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura == null)
                                {
                                    figura.listaMogućihPoteza.Add(promatraniRed.ToString() + pocetniStupac.ToString());
                                }
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura != null && Ploča.ploca[promatraniRed, pocetniStupac].figura.strana == Strana.Bijeli)
                                {
                                    kraj = true;
                                }
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura != null && Ploča.ploca[promatraniRed, pocetniStupac].figura.strana == Strana.Crni)
                                {
                                    figura.listaMogućihPoteza.Add(promatraniRed.ToString() + pocetniStupac.ToString());
                                    kraj = true;
                                }
                            }
                        }
                        //Kretnja prema lijevo
                        kraj = false;
                        for (int promatraniStupac = pocetniStupac - 1; promatraniStupac >= 0; promatraniStupac--)
                        {
                            if (promatraniStupac >= 0 && kraj == false)
                            {
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura == null)
                                {
                                    figura.listaMogućihPoteza.Add(pocetniRed.ToString() + promatraniStupac.ToString());
                                }
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura != null && Ploča.ploca[pocetniRed, promatraniStupac].figura.strana == Strana.Bijeli)
                                {
                                    kraj = true;
                                }
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura != null && Ploča.ploca[pocetniRed, promatraniStupac].figura.strana == Strana.Crni)
                                {
                                    figura.listaMogućihPoteza.Add(pocetniRed.ToString() + promatraniStupac.ToString());
                                    kraj = true;
                                }
                            }
                        }
                        //Kretnja prema desno
                        kraj = false;
                        for (int promatraniStupac = pocetniStupac + 1; promatraniStupac <= 7; promatraniStupac++)
                        {
                            if (promatraniStupac <= 7 && kraj == false)
                            {
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura == null)
                                {
                                    figura.listaMogućihPoteza.Add(pocetniRed.ToString() + promatraniStupac.ToString());
                                }
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura != null && Ploča.ploca[pocetniRed, promatraniStupac].figura.strana == Strana.Bijeli)
                                {
                                    kraj = true;
                                }
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura != null && Ploča.ploca[pocetniRed, promatraniStupac].figura.strana == Strana.Crni)
                                {
                                    figura.listaMogućihPoteza.Add(pocetniRed.ToString() + promatraniStupac.ToString());
                                    kraj = true;
                                }
                            }
                        }
                        int promatraniRed1;
                        int promatraniStupac1;
                        //Gore desno
                        kraj = false;
                        promatraniRed1 = figura.pozRed - 1;
                        promatraniStupac1 = figura.pozStupac + 1;
                        while (promatraniRed1 >= 0 && promatraniStupac1 <= 7 && kraj == false)
                        {
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura == null)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed1.ToString() + promatraniStupac1.ToString());
                            }
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura != null && Ploča.ploca[promatraniRed1, promatraniStupac1].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed1.ToString() + promatraniStupac1.ToString());
                                kraj = true;
                            }
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura != null && Ploča.ploca[promatraniRed1, promatraniStupac1].figura.strana == Strana.Bijeli)
                            {
                                kraj = true;
                            }
                            promatraniRed1--;
                            promatraniStupac1++;
                        }
                        //Gore lijevo
                        kraj = false;
                        promatraniRed1 = figura.pozRed - 1;
                        promatraniStupac1 = figura.pozStupac - 1;
                        while (promatraniRed1 >= 0 && promatraniStupac1 >= 0 && kraj == false)
                        {
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura == null)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed1.ToString() + promatraniStupac1.ToString());
                            }
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura != null && Ploča.ploca[promatraniRed1, promatraniStupac1].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed1.ToString() + promatraniStupac1.ToString());
                                kraj = true;
                            }
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura != null && Ploča.ploca[promatraniRed1, promatraniStupac1].figura.strana == Strana.Bijeli)
                            {
                                kraj = true;
                            }
                            promatraniRed1--;
                            promatraniStupac1--;
                        }
                        //Dolje desno
                        kraj = false;
                        promatraniRed1 = figura.pozRed + 1;
                        promatraniStupac1 = figura.pozStupac + 1;
                        while (promatraniRed1 <= 7 && promatraniStupac1 <= 7 && kraj == false)
                        {
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura == null)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed1.ToString() + promatraniStupac1.ToString());
                            }
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura != null && Ploča.ploca[promatraniRed1, promatraniStupac1].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed1.ToString() + promatraniStupac1.ToString());
                                kraj = true;
                            }
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura != null && Ploča.ploca[promatraniRed1, promatraniStupac1].figura.strana == Strana.Bijeli)
                            {
                                kraj = true;
                            }
                            promatraniRed1++;
                            promatraniStupac1++;
                        }
                        //Dolje lijevo
                        kraj = false;
                        promatraniRed1 = figura.pozRed + 1;
                        promatraniStupac1 = figura.pozStupac - 1;
                        while (promatraniRed1 <= 7 && promatraniStupac1 >= 0 && kraj == false)
                        {
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura == null)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed1.ToString() + promatraniStupac1.ToString());
                            }
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura != null && Ploča.ploca[promatraniRed1, promatraniStupac1].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed1.ToString() + promatraniStupac1.ToString());
                                kraj = true;
                            }
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura != null && Ploča.ploca[promatraniRed1, promatraniStupac1].figura.strana == Strana.Bijeli)
                            {
                                kraj = true;
                            }
                            promatraniRed1++;
                            promatraniStupac1--;
                        }
                    }
                    //Kralj
                    if (figura.naziv == "Kralj")
                    {
                        //Kretnja prema gore
                        if (figura.pozRed - 1 >= 0)
                        {
                            if (Ploča.ploca[figura.pozRed - 1, figura.pozStupac].figura == null || Ploča.ploca[figura.pozRed - 1, figura.pozStupac].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed - 1).ToString() + figura.pozStupac.ToString());
                            }
                        }
                        //Kretnja prema dolje
                        if (figura.pozRed + 1 <= 7)
                        {
                            if (Ploča.ploca[figura.pozRed + 1, figura.pozStupac].figura == null || Ploča.ploca[figura.pozRed + 1, figura.pozStupac].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed + 1).ToString() + figura.pozStupac.ToString());
                            }
                        }
                        //Kretnja prema lijevo
                        if (figura.pozStupac - 1 >= 0)
                        {
                            if (Ploča.ploca[figura.pozRed, figura.pozStupac - 1].figura == null || Ploča.ploca[figura.pozRed, figura.pozStupac - 1].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed).ToString() + (figura.pozStupac - 1).ToString());
                            }
                        }
                        //Kretnja prema desno
                        if (figura.pozStupac + 1 <= 7)
                        {
                            if (Ploča.ploca[figura.pozRed, figura.pozStupac + 1].figura == null || Ploča.ploca[figura.pozRed, figura.pozStupac + 1].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed).ToString() + (figura.pozStupac + 1).ToString());
                            }
                        }
                        //Gore desno (red-1, stupac +1)
                        if (figura.pozRed - 1 >= 0 && figura.pozStupac + 1 <= 7)
                        {
                            if (Ploča.ploca[figura.pozRed - 1, figura.pozStupac + 1].figura == null || Ploča.ploca[figura.pozRed - 1, figura.pozStupac + 1].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed - 1).ToString() + (figura.pozStupac + 1).ToString());
                            }
                        }
                        //Gore lijevo (red-1 stupac -1)
                        if (figura.pozRed - 1 >= 0 && figura.pozStupac - 1 >= 0)
                        {
                            if (Ploča.ploca[figura.pozRed - 1, figura.pozStupac - 1].figura == null || Ploča.ploca[figura.pozRed - 1, figura.pozStupac - 1].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed - 1).ToString() + (figura.pozStupac - 1).ToString());
                            }
                        }
                        //Dolje desno (red +1 stupac+1)
                        if (figura.pozRed + 1 <= 7 && figura.pozStupac + 1 <= 7)
                        {
                            if (Ploča.ploca[figura.pozRed + 1, figura.pozStupac + 1].figura == null || Ploča.ploca[figura.pozRed + 1, figura.pozStupac + 1].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed + 1).ToString() + (figura.pozStupac + 1).ToString());
                            }
                        }
                        //Dolje lijevo (red +1 stupac -1)
                        if (figura.pozRed + 1 <= 7 && figura.pozStupac - 1 >= 0)
                        {
                            if (Ploča.ploca[figura.pozRed + 1, figura.pozStupac - 1].figura == null || Ploča.ploca[figura.pozRed + 1, figura.pozStupac - 1].figura.strana == Strana.Crni)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed + 1).ToString() + (figura.pozStupac - 1).ToString());
                            }
                        }
                    }
                }
            }
            //Crne figure
            if(Strana == Strana.Crni)
            {
                foreach (Figura figura in Ploča.listaFiguraCrni)
                {
                    figura.listaMogućihPoteza.Clear();
                    //Pijun
                    if (figura.naziv == "Pijun")
                    {
                        foreach (Polje polje in Ploča.listaPolja)
                        {
                            if (polje.figura == null && polje.stupac == figura.pozStupac && polje.red == figura.pozRed + 1)
                            {
                                figura.listaMogućihPoteza.Add(polje.red.ToString() + polje.stupac.ToString());
                                if (figura.pozRed == 1 && polje.figura == null)
                                {
                                    foreach (Polje poljeIza in Ploča.listaPolja)
                                    {
                                        if (poljeIza.stupac == polje.stupac && poljeIza.red == polje.red + 1 && poljeIza.figura == null)
                                        {
                                            figura.listaMogućihPoteza.Add(poljeIza.red.ToString() + poljeIza.stupac.ToString());
                                        }
                                    }
                                }
                            }
                            if (polje.red == figura.pozRed + 1 && ((polje.stupac == figura.pozStupac - 1) || (polje.stupac == figura.pozStupac + 1)) && polje.figura != null && polje.figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add(polje.red.ToString() + polje.stupac.ToString());
                            }
                        }
                    }
                    //Top
                    if (figura.naziv == "Top")
                    {
                        int pocetniRed = figura.pozRed;
                        int pocetniStupac = figura.pozStupac;
                        bool kraj = false;
                        //Kretnja prema gore
                        for (int promatraniRed = pocetniRed - 1; promatraniRed >= 0; promatraniRed--)
                        {
                            if (promatraniRed >= 0 && kraj == false)
                            {
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura == null)
                                {
                                    figura.listaMogućihPoteza.Add(promatraniRed.ToString() + pocetniStupac.ToString());
                                }
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura != null && Ploča.ploca[promatraniRed, pocetniStupac].figura.strana == Strana.Crni)
                                {
                                    kraj = true;
                                }
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura != null && Ploča.ploca[promatraniRed, pocetniStupac].figura.strana == Strana.Bijeli)
                                {
                                    figura.listaMogućihPoteza.Add(promatraniRed.ToString() + pocetniStupac.ToString());
                                    kraj = true;
                                }
                            }
                        }
                        //Kretnja prema dolje
                        kraj = false;
                        for (int promatraniRed = pocetniRed + 1; promatraniRed <= 7; promatraniRed++)
                        {
                            if (promatraniRed <= 7 && kraj == false)
                            {
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura == null)
                                {
                                    figura.listaMogućihPoteza.Add(promatraniRed.ToString() + pocetniStupac.ToString());
                                }
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura != null && Ploča.ploca[promatraniRed, pocetniStupac].figura.strana == Strana.Crni)
                                {
                                    kraj = true;
                                }
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura != null && Ploča.ploca[promatraniRed, pocetniStupac].figura.strana == Strana.Bijeli)
                                {
                                    figura.listaMogućihPoteza.Add(promatraniRed.ToString() + pocetniStupac.ToString());
                                    kraj = true;
                                }
                            }
                        }
                        //Kretnja prema lijevo
                        kraj = false;
                        for (int promatraniStupac = pocetniStupac - 1; promatraniStupac >= 0; promatraniStupac--)
                        {
                            if (promatraniStupac >= 0 && kraj == false)
                            {
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura == null)
                                {
                                    figura.listaMogućihPoteza.Add(pocetniRed.ToString() + promatraniStupac.ToString());
                                }
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura != null && Ploča.ploca[pocetniRed, promatraniStupac].figura.strana == Strana.Crni)
                                {
                                    kraj = true;
                                }
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura != null && Ploča.ploca[pocetniRed, promatraniStupac].figura.strana == Strana.Bijeli)
                                {
                                    figura.listaMogućihPoteza.Add(pocetniRed.ToString() + promatraniStupac.ToString());
                                    kraj = true;
                                }
                            }
                        }
                        //Kretnja prema desno
                        kraj = false;
                        for (int promatraniStupac = pocetniStupac + 1; promatraniStupac <= 7; promatraniStupac++)
                        {
                            if (promatraniStupac <= 7 && kraj == false)
                            {
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura == null)
                                {
                                    figura.listaMogućihPoteza.Add(pocetniRed.ToString() + promatraniStupac.ToString());
                                }
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura != null && Ploča.ploca[pocetniRed, promatraniStupac].figura.strana == Strana.Crni)
                                {
                                    kraj = true;
                                }
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura != null && Ploča.ploca[pocetniRed, promatraniStupac].figura.strana == Strana.Bijeli)
                                {
                                    figura.listaMogućihPoteza.Add(pocetniRed.ToString() + promatraniStupac.ToString());
                                    kraj = true;
                                }
                            }
                        }
                    }
                    //Skakač
                    if (figura.naziv == "Skakač")
                    {
                        //Desno 2 dolje 1
                        if (figura.pozRed + 1 <= 7 && figura.pozStupac + 2 <= 7)
                        {
                            if (Ploča.ploca[figura.pozRed + 1, figura.pozStupac + 2].figura == null || Ploča.ploca[figura.pozRed + 1, figura.pozStupac + 2].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed + 1).ToString() + (figura.pozStupac + 2).ToString());
                            }
                        }
                        //Desno 2 gore 1 
                        if (figura.pozRed - 1 >= 0 && figura.pozStupac + 2 <= 7)
                        {
                            if (Ploča.ploca[figura.pozRed - 1, figura.pozStupac + 2].figura == null || Ploča.ploca[figura.pozRed - 1, figura.pozStupac + 2].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed - 1).ToString() + (figura.pozStupac + 2).ToString());
                            }
                        }
                        //Dolje 2 desno 1
                        if (figura.pozRed + 2 <= 7 && figura.pozStupac + 1 <= 7)
                        {
                            if (Ploča.ploca[figura.pozRed + 2, figura.pozStupac + 1].figura == null || Ploča.ploca[figura.pozRed + 2, figura.pozStupac + 1].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed + 2).ToString() + (figura.pozStupac + 1).ToString());
                            }
                        }
                        //Dolje 2 Lijevo 1
                        if (figura.pozRed + 2 <= 7 && figura.pozStupac - 1 >= 0)
                        {
                            if (Ploča.ploca[figura.pozRed + 2, figura.pozStupac - 1].figura == null || Ploča.ploca[figura.pozRed + 2, figura.pozStupac - 1].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed + 2).ToString() + (figura.pozStupac - 1).ToString());
                            }
                        }
                        //Lijevo 2 gore 1
                        if (figura.pozRed - 1 >= 0 && figura.pozStupac - 2 >= 0)
                        {
                            if (Ploča.ploca[figura.pozRed - 1, figura.pozStupac - 2].figura == null || Ploča.ploca[figura.pozRed - 1, figura.pozStupac - 2].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed - 1).ToString() + (figura.pozStupac - 2).ToString());
                            }
                        }
                        //Lijevo 2 dolje 1
                        if (figura.pozRed + 1 <= 7 && figura.pozStupac - 2 >= 0)
                        {
                            if (Ploča.ploca[figura.pozRed + 1, figura.pozStupac - 2].figura == null || Ploča.ploca[figura.pozRed + 1, figura.pozStupac - 2].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed + 1).ToString() + (figura.pozStupac - 2).ToString());
                            }
                        }
                        //Gore 2 lijevo 1
                        if (figura.pozRed - 2 >= 0 && figura.pozStupac - 1 >= 0)
                        {
                            if (Ploča.ploca[figura.pozRed - 2, figura.pozStupac - 1].figura == null || Ploča.ploca[figura.pozRed - 2, figura.pozStupac - 1].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed - 2).ToString() + (figura.pozStupac - 1).ToString());
                            }
                        }
                        //Gore 2 desno 1
                        if (figura.pozRed - 2 >= 0 && figura.pozStupac + 1 <= 7)
                        {
                            if (Ploča.ploca[figura.pozRed - 2, figura.pozStupac + 1].figura == null || Ploča.ploca[figura.pozRed - 2, figura.pozStupac + 1].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed - 2).ToString() + (figura.pozStupac + 1).ToString());
                            }
                        }
                    }
                    //Lovac
                    if (figura.naziv == "Lovac")
                    {
                        int promatraniRed;
                        int promatraniStupac;
                        bool kraj;
                        //Gore desno
                        kraj = false;
                        promatraniRed = figura.pozRed - 1;
                        promatraniStupac = figura.pozStupac + 1;
                        while (promatraniRed >= 0 && promatraniStupac <= 7 && kraj == false)
                        {
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura == null)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed.ToString() + promatraniStupac.ToString());
                            }
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura != null && Ploča.ploca[promatraniRed, promatraniStupac].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed.ToString() + promatraniStupac.ToString());
                                kraj = true;
                            }
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura != null && Ploča.ploca[promatraniRed, promatraniStupac].figura.strana == Strana.Crni)
                            {
                                kraj = true;
                            }
                            promatraniRed--;
                            promatraniStupac++;
                        }
                        //Gore lijevo
                        kraj = false;
                        promatraniRed = figura.pozRed - 1;
                        promatraniStupac = figura.pozStupac - 1;
                        while (promatraniRed >= 0 && promatraniStupac >= 0 && kraj == false)
                        {
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura == null)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed.ToString() + promatraniStupac.ToString());
                            }
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura != null && Ploča.ploca[promatraniRed, promatraniStupac].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed.ToString() + promatraniStupac.ToString());
                                kraj = true;
                            }
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura != null && Ploča.ploca[promatraniRed, promatraniStupac].figura.strana == Strana.Crni)
                            {
                                kraj = true;
                            }
                            promatraniRed--;
                            promatraniStupac--;
                        }
                        //Dolje desno
                        kraj = false;
                        promatraniRed = figura.pozRed + 1;
                        promatraniStupac = figura.pozStupac + 1;
                        while (promatraniRed <= 7 && promatraniStupac <= 7 && kraj == false)
                        {
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura == null)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed.ToString() + promatraniStupac.ToString());
                            }
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura != null && Ploča.ploca[promatraniRed, promatraniStupac].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed.ToString() + promatraniStupac.ToString());
                                kraj = true;
                            }
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura != null && Ploča.ploca[promatraniRed, promatraniStupac].figura.strana == Strana.Crni)
                            {
                                kraj = true;
                            }
                            promatraniRed++;
                            promatraniStupac++;
                        }
                        //Dolje lijevo
                        kraj = false;
                        promatraniRed = figura.pozRed + 1;
                        promatraniStupac = figura.pozStupac - 1;
                        while (promatraniRed <= 7 && promatraniStupac >= 0 && kraj == false)
                        {
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura == null)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed.ToString() + promatraniStupac.ToString());
                            }
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura != null && Ploča.ploca[promatraniRed, promatraniStupac].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed.ToString() + promatraniStupac.ToString());
                                kraj = true;
                            }
                            if (Ploča.ploca[promatraniRed, promatraniStupac].figura != null && Ploča.ploca[promatraniRed, promatraniStupac].figura.strana == Strana.Crni)
                            {
                                kraj = true;
                            }
                            promatraniRed++;
                            promatraniStupac--;
                        }
                    }
                    //Kraljica
                    if (figura.naziv == "Kraljica")
                    {
                        int pocetniRed = figura.pozRed;
                        int pocetniStupac = figura.pozStupac;
                        bool kraj = false;
                        //Kretnja prema gore
                        for (int promatraniRed = pocetniRed - 1; promatraniRed >= 0; promatraniRed--)
                        {
                            if (promatraniRed >= 0 && kraj == false)
                            {
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura == null)
                                {
                                    figura.listaMogućihPoteza.Add(promatraniRed.ToString() + pocetniStupac.ToString());
                                }
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura != null && Ploča.ploca[promatraniRed, pocetniStupac].figura.strana == Strana.Crni)
                                {
                                    kraj = true;
                                }
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura != null && Ploča.ploca[promatraniRed, pocetniStupac].figura.strana == Strana.Bijeli)
                                {
                                    figura.listaMogućihPoteza.Add(promatraniRed.ToString() + pocetniStupac.ToString());
                                    kraj = true;
                                }
                            }
                        }
                        //Kretnja prema dolje
                        kraj = false;
                        for (int promatraniRed = pocetniRed + 1; promatraniRed <= 7; promatraniRed++)
                        {
                            if (promatraniRed <= 7 && kraj == false)
                            {
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura == null)
                                {
                                    figura.listaMogućihPoteza.Add(promatraniRed.ToString() + pocetniStupac.ToString());
                                }
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura != null && Ploča.ploca[promatraniRed, pocetniStupac].figura.strana == Strana.Crni)
                                {
                                    kraj = true;
                                }
                                if (Ploča.ploca[promatraniRed, pocetniStupac].figura != null && Ploča.ploca[promatraniRed, pocetniStupac].figura.strana == Strana.Bijeli)
                                {
                                    figura.listaMogućihPoteza.Add(promatraniRed.ToString() + pocetniStupac.ToString());
                                    kraj = true;
                                }
                            }
                        }
                        //Kretnja prema lijevo
                        kraj = false;
                        for (int promatraniStupac = pocetniStupac - 1; promatraniStupac >= 0; promatraniStupac--)
                        {
                            if (promatraniStupac >= 0 && kraj == false)
                            {
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura == null)
                                {
                                    figura.listaMogućihPoteza.Add(pocetniRed.ToString() + promatraniStupac.ToString());
                                }
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura != null && Ploča.ploca[pocetniRed, promatraniStupac].figura.strana == Strana.Crni)
                                {
                                    kraj = true;
                                }
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura != null && Ploča.ploca[pocetniRed, promatraniStupac].figura.strana == Strana.Bijeli)
                                {
                                    figura.listaMogućihPoteza.Add(pocetniRed.ToString() + promatraniStupac.ToString());
                                    kraj = true;
                                }
                            }
                        }
                        //Kretnja prema desno
                        kraj = false;
                        for (int promatraniStupac = pocetniStupac + 1; promatraniStupac <= 7; promatraniStupac++)
                        {
                            if (promatraniStupac <= 7 && kraj == false)
                            {
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura == null)
                                {
                                    figura.listaMogućihPoteza.Add(pocetniRed.ToString() + promatraniStupac.ToString());
                                }
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura != null && Ploča.ploca[pocetniRed, promatraniStupac].figura.strana == Strana.Crni)
                                {
                                    kraj = true;
                                }
                                if (Ploča.ploca[pocetniRed, promatraniStupac].figura != null && Ploča.ploca[pocetniRed, promatraniStupac].figura.strana == Strana.Bijeli)
                                {
                                    figura.listaMogućihPoteza.Add(pocetniRed.ToString() + promatraniStupac.ToString());
                                    kraj = true;
                                }
                            }
                        }
                        int promatraniRed1;
                        int promatraniStupac1;
                        //Gore desno
                        kraj = false;
                        promatraniRed1 = figura.pozRed - 1;
                        promatraniStupac1 = figura.pozStupac + 1;
                        while (promatraniRed1 >= 0 && promatraniStupac1 <= 7 && kraj == false)
                        {
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura == null)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed1.ToString() + promatraniStupac1.ToString());
                            }
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura != null && Ploča.ploca[promatraniRed1, promatraniStupac1].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed1.ToString() + promatraniStupac1.ToString());
                                kraj = true;
                            }
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura != null && Ploča.ploca[promatraniRed1, promatraniStupac1].figura.strana == Strana.Crni)
                            {
                                kraj = true;
                            }
                            promatraniRed1--;
                            promatraniStupac1++;
                        }
                        //Gore lijevo
                        kraj = false;
                        promatraniRed1 = figura.pozRed - 1;
                        promatraniStupac1 = figura.pozStupac - 1;
                        while (promatraniRed1 >= 0 && promatraniStupac1 >= 0 && kraj == false)
                        {
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura == null)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed1.ToString() + promatraniStupac1.ToString());
                            }
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura != null && Ploča.ploca[promatraniRed1, promatraniStupac1].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed1.ToString() + promatraniStupac1.ToString());
                                kraj = true;
                            }
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura != null && Ploča.ploca[promatraniRed1, promatraniStupac1].figura.strana == Strana.Crni)
                            {
                                kraj = true;
                            }
                            promatraniRed1--;
                            promatraniStupac1--;
                        }
                        //Dolje desno
                        kraj = false;
                        promatraniRed1 = figura.pozRed + 1;
                        promatraniStupac1 = figura.pozStupac + 1;
                        while (promatraniRed1 <= 7 && promatraniStupac1 <= 7 && kraj == false)
                        {
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura == null)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed1.ToString() + promatraniStupac1.ToString());
                            }
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura != null && Ploča.ploca[promatraniRed1, promatraniStupac1].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed1.ToString() + promatraniStupac1.ToString());
                                kraj = true;
                            }
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura != null && Ploča.ploca[promatraniRed1, promatraniStupac1].figura.strana == Strana.Crni)
                            {
                                kraj = true;
                            }
                            promatraniRed1++;
                            promatraniStupac1++;
                        }
                        //Dolje lijevo
                        kraj = false;
                        promatraniRed1 = figura.pozRed + 1;
                        promatraniStupac1 = figura.pozStupac - 1;
                        while (promatraniRed1 <= 7 && promatraniStupac1 >= 0 && kraj == false)
                        {
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura == null)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed1.ToString() + promatraniStupac1.ToString());
                            }
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura != null && Ploča.ploca[promatraniRed1, promatraniStupac1].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add(promatraniRed1.ToString() + promatraniStupac1.ToString());
                                kraj = true;
                            }
                            if (Ploča.ploca[promatraniRed1, promatraniStupac1].figura != null && Ploča.ploca[promatraniRed1, promatraniStupac1].figura.strana == Strana.Crni)
                            {
                                kraj = true;
                            }
                            promatraniRed1++;
                            promatraniStupac1--;
                        }
                    }
                    //Kralj
                    if (figura.naziv == "Kralj")
                    {
                        //Kretnja prema gore
                        if (figura.pozRed - 1 >= 0)
                        {
                            if (Ploča.ploca[figura.pozRed - 1, figura.pozStupac].figura == null || Ploča.ploca[figura.pozRed - 1, figura.pozStupac].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed - 1).ToString() + figura.pozStupac.ToString());
                            }
                        }
                        //Kretnja prema dolje
                        if (figura.pozRed + 1 <= 7)
                        {
                            if (Ploča.ploca[figura.pozRed + 1, figura.pozStupac].figura == null || Ploča.ploca[figura.pozRed + 1, figura.pozStupac].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed + 1).ToString() + figura.pozStupac.ToString());
                            }
                        }
                        //Kretnja prema lijevo
                        if (figura.pozStupac - 1 >= 0)
                        {
                            if (Ploča.ploca[figura.pozRed, figura.pozStupac - 1].figura == null || Ploča.ploca[figura.pozRed, figura.pozStupac - 1].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed).ToString() + (figura.pozStupac - 1).ToString());
                            }
                        }
                        //Kretnja prema desno
                        if (figura.pozStupac + 1 <= 7)
                        {
                            if (Ploča.ploca[figura.pozRed, figura.pozStupac + 1].figura == null || Ploča.ploca[figura.pozRed, figura.pozStupac + 1].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed).ToString() + (figura.pozStupac + 1).ToString());
                            }
                        }
                        //Gore desno (red-1, stupac +1)
                        if (figura.pozRed - 1 >= 0 && figura.pozStupac + 1 <= 7)
                        {
                            if (Ploča.ploca[figura.pozRed - 1, figura.pozStupac + 1].figura == null || Ploča.ploca[figura.pozRed - 1, figura.pozStupac + 1].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed - 1).ToString() + (figura.pozStupac + 1).ToString());
                            }
                        }
                        //Gore lijevo (red-1 stupac -1)
                        if (figura.pozRed - 1 >= 0 && figura.pozStupac - 1 >= 0)
                        {
                            if (Ploča.ploca[figura.pozRed - 1, figura.pozStupac - 1].figura == null || Ploča.ploca[figura.pozRed - 1, figura.pozStupac - 1].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed - 1).ToString() + (figura.pozStupac - 1).ToString());
                            }
                        }
                        //Dolje desno (red +1 stupac+1)
                        if (figura.pozRed + 1 <= 7 && figura.pozStupac + 1 <= 7)
                        {
                            if (Ploča.ploca[figura.pozRed + 1, figura.pozStupac + 1].figura == null || Ploča.ploca[figura.pozRed + 1, figura.pozStupac + 1].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed + 1).ToString() + (figura.pozStupac + 1).ToString());
                            }
                        }
                        //Dolje lijevo (red +1 stupac -1)
                        if (figura.pozRed + 1 <= 7 && figura.pozStupac - 1 >= 0)
                        {
                            if (Ploča.ploca[figura.pozRed + 1, figura.pozStupac - 1].figura == null || Ploča.ploca[figura.pozRed + 1, figura.pozStupac - 1].figura.strana == Strana.Bijeli)
                            {
                                figura.listaMogućihPoteza.Add((figura.pozRed + 1).ToString() + (figura.pozStupac - 1).ToString());
                            }
                        }
                    }
                }
            }
            OgraničiPotezeKralja(Strana);
        }



        public static void OgraničiPotezeKralja(Strana Strana)
        {
            if(Strana == Strana.Bijeli)
            {
                foreach(Figura figura in Ploča.listaFiguraBijeli)
                {
                    if (figura.naziv == "Kralj")
                    {
                        foreach(Figura figurica in Ploča.listaFiguraCrni)
                        {
                            if (figurica.naziv != "Pijun" && figurica.naziv!="Kralj")
                            {
                                foreach (string potez in figurica.listaMogućihPoteza)
                                {
                                    if (figura.listaMogućihPoteza.Contains(potez)) figura.listaMogućihPoteza.Remove(potez);
                                }
                            }
                            if (figurica.naziv == "Pijun")
                            {
                                if (figurica.pozRed < 7)
                                {
                                    foreach (string potez in figurica.listaMogućihPoteza)
                                    {
                                        int red = int.Parse(potez[0].ToString());
                                        int stupac = int.Parse(potez[1].ToString());
                                        if (figurica.pozStupac != stupac)
                                        {
                                            if (figura.listaMogućihPoteza.Contains(potez)) figura.listaMogućihPoteza.Remove(potez);
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (string potez in figurica.listaMogućihPoteza)
                                    {
                                        if (figura.listaMogućihPoteza.Contains(potez)) figura.listaMogućihPoteza.Remove(potez);
                                    }
                                }
                            }
                            if (figurica.naziv == "Kralj")
                            {
                                List<string> listaPoljaOkoKralja = new List<string>();
                                int red = figurica.pozRed;
                                int stupac = figurica.pozStupac;
                                if (red - 1 >= 0)
                                {
                                    listaPoljaOkoKralja.Add((red - 1).ToString() + stupac.ToString());
                                }
                                if (red + 1 <= 7)
                                {
                                    listaPoljaOkoKralja.Add((red + 1).ToString() + stupac.ToString());
                                }
                                if (stupac - 1 >= 0)
                                {
                                    listaPoljaOkoKralja.Add(red.ToString()+(stupac-1).ToString());
                                }
                                if (stupac + 1 <= 7)
                                {
                                    listaPoljaOkoKralja.Add(red.ToString() + (stupac + 1).ToString());
                                }
                                if (red - 1 >= 0 && stupac + 1 <= 7)
                                {
                                    listaPoljaOkoKralja.Add((red - 1).ToString()+(stupac+1).ToString());
                                }
                                if (red - 1 >= 0 && stupac - 1 >= 0)
                                {
                                    listaPoljaOkoKralja.Add((red - 1).ToString() + (stupac - 1).ToString());
                                }
                                if (red + 1 <= 7 && stupac + 1 <= 7)
                                {
                                    listaPoljaOkoKralja.Add((red + 1).ToString() + (stupac + 1).ToString());
                                }
                                if (red + 1 <= 7 && stupac - 1 >= 0)
                                {
                                    listaPoljaOkoKralja.Add((red + 1).ToString() + (stupac - 1).ToString());
                                }
                                foreach(string polje in listaPoljaOkoKralja)
                                {
                                    if (figura.listaMogućihPoteza.Contains(polje)) figura.listaMogućihPoteza.Remove(polje);
                                }
                            }
                        }
                    }
                }
            }
            if(Strana == Strana.Crni)
            {
                foreach (Figura figura in Ploča.listaFiguraCrni)
                {
                    if (figura.naziv == "Kralj")
                    {
                        foreach (Figura figurica in Ploča.listaFiguraBijeli)
                        {
                            if (figurica.naziv != "Pijun" && figurica.naziv!="Kralj")
                            {
                                foreach (string potez in figurica.listaMogućihPoteza)
                                {
                                    if (figura.listaMogućihPoteza.Contains(potez)) figura.listaMogućihPoteza.Remove(potez);
                                }
                            }
                            if (figurica.naziv == "Pijun")
                            {
                                if (figurica.pozRed > 0)
                                {
                                    foreach (string potez in figurica.listaMogućihPoteza)
                                    {
                                        int red = int.Parse(potez[0].ToString());
                                        int stupac = int.Parse(potez[1].ToString());
                                        if (figurica.pozStupac != stupac)
                                        {
                                            if (figura.listaMogućihPoteza.Contains(potez)) figura.listaMogućihPoteza.Remove(potez);
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (string potez in figurica.listaMogućihPoteza)
                                    {
                                        if (figura.listaMogućihPoteza.Contains(potez)) figura.listaMogućihPoteza.Remove(potez);
                                    }
                                }

                            }
                            if (figurica.naziv == "Kralj")
                            {
                                List<string> listaPoljaOkoKralja = new List<string>();
                                int red = figurica.pozRed;
                                int stupac = figurica.pozStupac;
                                if (red - 1 >= 0)
                                {
                                    listaPoljaOkoKralja.Add((red - 1).ToString() + stupac.ToString());
                                }
                                if (red + 1 <= 7)
                                {
                                    listaPoljaOkoKralja.Add((red + 1).ToString() + stupac.ToString());
                                }
                                if (stupac - 1 >= 0)
                                {
                                    listaPoljaOkoKralja.Add(red.ToString() + (stupac - 1).ToString());
                                }
                                if (stupac + 1 <= 7)
                                {
                                    listaPoljaOkoKralja.Add(red.ToString() + (stupac + 1).ToString());
                                }
                                if (red - 1 >= 0 && stupac + 1 <= 7)
                                {
                                    listaPoljaOkoKralja.Add((red - 1).ToString() + (stupac + 1).ToString());
                                }
                                if (red - 1 >= 0 && stupac - 1 >= 0)
                                {
                                    listaPoljaOkoKralja.Add((red - 1).ToString() + (stupac - 1).ToString());
                                }
                                if (red + 1 <= 7 && stupac + 1 <= 7)
                                {
                                    listaPoljaOkoKralja.Add((red + 1).ToString() + (stupac + 1).ToString());
                                }
                                if (red + 1 <= 7 && stupac - 1 >= 0)
                                {
                                    listaPoljaOkoKralja.Add((red + 1).ToString() + (stupac - 1).ToString());
                                }
                                foreach (string polje in listaPoljaOkoKralja)
                                {
                                    if (figura.listaMogućihPoteza.Contains(polje)) figura.listaMogućihPoteza.Remove(polje);
                                }
                            }
                        }
                    }
                }
            }
        }



        public static void IspisBijeli()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  a b c d e f g h");
            Console.WriteLine();
            for (int red = 0; red < 8; red++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write((8 - red).ToString() + " ");
                for (int stupac = 0; stupac < 8; stupac++)
                {
                    Console.BackgroundColor = Ploča.ploca[red, stupac].boja;
                    if (Ploča.ploca[red, stupac].figura != null)
                    {
                        Console.ForegroundColor = Ploča.ploca[red, stupac].figura.boja;
                        Console.Write(Ploča.ploca[red, stupac].figura.oznaka+' ');
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  a b c d e f g h");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        


        public static void IspisCrni()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  h g f e d c b a");
            Console.WriteLine();
            for (int red = 7; red >= 0; red--)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write((8 - red).ToString() + " ");
                for (int stupac = 7; stupac >= 0; stupac--)
                {
                    Console.BackgroundColor = Ploča.ploca[red, stupac].boja;
                    if (Ploča.ploca[red, stupac].figura != null)
                    {
                        Console.ForegroundColor = Ploča.ploca[red, stupac].figura.boja;
                        Console.Write(Ploča.ploca[red, stupac].figura.oznaka +' ');
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  h g f e d c b a");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }



        //Ako je željeni potez moguće odigrati, odigra se
        public static void OdigrajPotez(Strana Strana, string PotezIgrača)
        {
            string tipPoteza = ProvjeriPotez(Strana, PotezIgrača);
            if(tipPoteza != "")
            {
                Ploča.triZadnjaPoteza.Add(PotezIgrača);
            }
            //Običan potez
            if(tipPoteza == "Običan")
            {
                string potezIgrača = PretvoriStringPotez(PotezIgrača);
                //Ukoliko se na željenom mjestu nalazi neprijateljska figura
                if(Ploča.ploca[int.Parse(potezIgrača[4].ToString()), int.Parse(potezIgrača[3].ToString())].figura!=null && Ploča.ploca[int.Parse(potezIgrača[4].ToString()), int.Parse(potezIgrača[3].ToString())].figura.strana != Strana)
                {
                    //Obriši figuru
                    if(Strana == Strana.Bijeli)
                    {
                        Ploča.listaFiguraCrni.Remove(Ploča.ploca[int.Parse(potezIgrača[4].ToString()), int.Parse(potezIgrača[3].ToString())].figura);
                    }
                    else
                    {
                        Ploča.listaFiguraBijeli.Remove(Ploča.ploca[int.Parse(potezIgrača[4].ToString()), int.Parse(potezIgrača[3].ToString())].figura);
                    }
                }
                Ploča.ploca[int.Parse(potezIgrača[4].ToString()), int.Parse(potezIgrača[3].ToString())].figura = Ploča.ploca[int.Parse(potezIgrača[1].ToString()), int.Parse(potezIgrača[0].ToString())].figura;
                Ploča.ploca[int.Parse(potezIgrača[4].ToString()), int.Parse(potezIgrača[3].ToString())].figura.pozRed = int.Parse(potezIgrača[4].ToString());
                Ploča.ploca[int.Parse(potezIgrača[4].ToString()), int.Parse(potezIgrača[3].ToString())].figura.pozStupac = int.Parse(potezIgrača[3].ToString());
                Ploča.ploca[int.Parse(potezIgrača[1].ToString()), int.Parse(potezIgrača[0].ToString())].figura = null;
                Ploča.ploca[int.Parse(potezIgrača[4].ToString()), int.Parse(potezIgrača[3].ToString())].figura.brojKretnji++;
            }
            //Velika rohada
            if (tipPoteza=="Velika Rohada")
            {
                if(Strana == Strana.Bijeli)
                {
                    //Pomak kralja
                    Ploča.ploca[7, 2].figura = Ploča.ploca[7, 4].figura;
                    Ploča.ploca[7, 2].figura.pozStupac = 2;
                    Ploča.ploca[7, 2].figura.brojKretnji++;
                    Ploča.ploca[7, 4].figura = null;
                    //Pomak topa
                    Ploča.ploca[7, 3].figura = Ploča.ploca[7, 0].figura;
                    Ploča.ploca[7, 3].figura.pozStupac = 3;
                    Ploča.ploca[7, 0].figura = null;
                }
                if(Strana == Strana.Crni)
                {
                    //Pomak kralja
                    Ploča.ploca[0, 2].figura = Ploča.ploca[0, 4].figura;
                    Ploča.ploca[0, 2].figura.pozStupac = 2;
                    Ploča.ploca[0, 2].figura.brojKretnji++;
                    Ploča.ploca[0, 4].figura = null;
                    //Pomak topa
                    Ploča.ploca[0, 3].figura = Ploča.ploca[0, 0].figura;
                    Ploča.ploca[0, 3].figura.pozStupac = 3;
                    Ploča.ploca[0, 0].figura = null;
                }
            }
            //Mala rohada
            if (tipPoteza=="Mala Rohada")
            {
                if(Strana == Strana.Bijeli)
                {
                    //Pomak kralja
                    Ploča.ploca[7, 6].figura = Ploča.ploca[7, 4].figura;
                    Ploča.ploca[7, 6].figura.pozStupac = 6;
                    Ploča.ploca[7, 6].figura.brojKretnji++;
                    Ploča.ploca[7, 4].figura = null;
                    //Pomak topa
                    Ploča.ploca[7, 5].figura = Ploča.ploca[7, 7].figura;
                    Ploča.ploca[7, 5].figura.pozStupac = 5;
                    Ploča.ploca[7, 7].figura = null;
                }
                if(Strana == Strana.Crni)
                {
                    //Pomak kralja
                    Ploča.ploca[0, 6].figura = Ploča.ploca[0, 4].figura;
                    Ploča.ploca[0, 6].figura.pozStupac = 6;
                    Ploča.ploca[0, 6].figura.brojKretnji++;
                    Ploča.ploca[0, 4].figura = null;
                    //Pomak topa
                    Ploča.ploca[0, 5].figura = Ploča.ploca[0, 7].figura;
                    Ploča.ploca[0, 5].figura.pozStupac = 5;
                    Ploča.ploca[0, 7].figura = null;
                }
            }
        }



        //Nakon unosa poteza, prvo se provjerava ispravnost poteza kojeg igrač želi odigrati, može vratiti: "Običan" || "Velika Rohada" || "Mala Rohada" || ""
        public static string ProvjeriPotez(Strana Strana,string PotezIgrača)
        {
            string vrsta = "";
            //Provjera unosa---------------------------------------
            //Običan potez
            if (PotezIgrača.Count() == 5)
            {
                if(PotezIgrača[0]=='a' || PotezIgrača[0]=='b' || PotezIgrača[0] == 'c' || PotezIgrača[0] == 'd' || PotezIgrača[0] == 'e' || PotezIgrača[0] == 'f' || PotezIgrača[0] == 'g' || PotezIgrača[0] == 'h')
                {
                    if (PotezIgrača[1] == '1' || PotezIgrača[1] == '2' || PotezIgrača[1] == '3' || PotezIgrača[1] == '4' || PotezIgrača[1] == '5' || PotezIgrača[1] == '6' || PotezIgrača[1] == '7' || PotezIgrača[1] == '8')
                    {
                        if (PotezIgrača[2] == ' ')
                        {
                            if (PotezIgrača[3] == 'a' || PotezIgrača[3] == 'b' || PotezIgrača[3] == 'c' || PotezIgrača[3] == 'd' || PotezIgrača[3] == 'e' || PotezIgrača[3] == 'f' || PotezIgrača[3] == 'g' || PotezIgrača[3] == 'h')
                            {
                                if (PotezIgrača[4] == '1' || PotezIgrača[4] == '2' || PotezIgrača[4] == '3' || PotezIgrača[4] == '4' || PotezIgrača[4] == '5' || PotezIgrača[4] == '6' || PotezIgrača[4] == '7' || PotezIgrača[4] == '8')
                                {
                                    vrsta = "Običan";
                                }
                            }
                        }
                    }
                }
            }
            //Velika rohada
            if(PotezIgrača.Count() == 5 && PotezIgrača[0]=='O' && PotezIgrača[1]=='-' && PotezIgrača[2]=='O' && PotezIgrača[3]=='-' && PotezIgrača[4]=='O')
            {
                vrsta = "Velika Rohada";
            }
            //Mala rohada
            if(PotezIgrača.Count()==3 && PotezIgrača[0]=='O' && PotezIgrača[1]=='-' && PotezIgrača[2] == 'O')
            {
                vrsta = "Mala Rohada";
            }
            //Provjera poteza----------------------------------------
            string poruka = "";
            //Običan potez
            if(vrsta == "Običan")
            {
                string potezIgrača = PretvoriStringPotez(PotezIgrača);
                if (Ploča.ploca[int.Parse(potezIgrača[1].ToString()), int.Parse(potezIgrača[0].ToString())].figura.strana == Strana)
                {
                    string željeniPotez = potezIgrača[4].ToString() + potezIgrača[3].ToString();
                    if (Ploča.ploca[int.Parse(potezIgrača[1].ToString()), int.Parse(potezIgrača[0].ToString())].figura.listaMogućihPoteza.Contains(željeniPotez))
                    {
                        poruka = "Običan";
                        return poruka;
                    }
                    else
                    {
                        return poruka;
                    }
                }
                else
                {
                    return poruka;
                }
            }
            //Velika rohada
            if(vrsta=="Velika Rohada")
            {
                //Bijeli koristi rohadu
                if(Strana == Strana.Bijeli)
                {
                    //Kralj i top nisu micani
                    if(Ploča.ploca[7, 4].figura.brojKretnji==0 && Ploča.ploca[7, 0].figura.brojKretnji == 0)
                    {
                        //Polje na kojem se nalazi kralj nije pod udarom
                        if (ProvjeriUdarNaPolje(Strana.Bijeli, Ploča.ploca[7, 4]) == false)
                        {
                            //Prvo polje do kralja nije zauzeto i nije pod udarom
                            if(Ploča.ploca[7,3].figura==null && ProvjeriUdarNaPolje(Strana.Bijeli, Ploča.ploca[7, 3]) == false)
                            {
                                //Drugo polje do kralja nije zauzeto i nije pod udarom
                                if(Ploča.ploca[7,2].figura==null && ProvjeriUdarNaPolje(Strana.Bijeli, Ploča.ploca[7, 2]) == false)
                                {
                                    //Treće polje do kralja nije zauzeto
                                    if (Ploča.ploca[7, 1].figura == null)
                                    {
                                        poruka = "Velika Rohada";
                                        return poruka;
                                    }
                                }
                            }
                        }
                    }
                }
                //Crni koristi rohadu
                if(Strana == Strana.Crni)
                {
                    //Kralj i top nisu micani
                    if (Ploča.ploca[0, 4].figura.brojKretnji == 0 && Ploča.ploca[0, 0].figura.brojKretnji == 0)
                    {
                        //Polje na kojem se nalazi kralj nije pod udarom
                        if (ProvjeriUdarNaPolje(Strana.Crni, Ploča.ploca[0, 4]) == false)
                        {
                            //Prvo polje do kralja nije zauzeto i nije pod udarom
                            if (Ploča.ploca[0, 3].figura == null && ProvjeriUdarNaPolje(Strana.Crni, Ploča.ploca[0, 3]) == false)
                            {
                                //Drugo polje do kralja nije zauzeto i nije pod udarom
                                if (Ploča.ploca[0, 2].figura == null && ProvjeriUdarNaPolje(Strana.Crni, Ploča.ploca[0, 2]) == false)
                                {
                                    //Treće polje do kralja nije zauzeto
                                    if (Ploča.ploca[0, 1].figura == null)
                                    {
                                        poruka = "Velika Rohada";
                                        return poruka;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //Mala rohada
            if(vrsta=="Mala Rohada")
            {
                //Bijeli koristi rohadu
                if(Strana == Strana.Bijeli)
                {
                    //Kralj i top nisu micani
                    if(Ploča.ploca[7,4].figura.brojKretnji==0 && Ploča.ploca[7, 7].figura.brojKretnji == 0)
                    {
                        //Polje na kojem se nalazi kralj nije pod udarom
                        if (ProvjeriUdarNaPolje(Strana.Bijeli,Ploča.ploca[7,4])==false)
                        {
                            //Polje do kralja nije zauzeto i nije pod udarom
                            if(Ploča.ploca[7,5].figura==null && ProvjeriUdarNaPolje(Strana.Bijeli, Ploča.ploca[7, 5]) == false)
                            {
                                //Drugo polje do kralja nije zauzeto i nije pod udarom
                                if(Ploča.ploca[7,6].figura==null && ProvjeriUdarNaPolje(Strana.Bijeli, Ploča.ploca[7, 6]) == false)
                                {
                                    poruka = "Mala Rohada";
                                    return poruka;
                                }
                            }
                        }
                    }
                }
                //Crni koristi rohadu
                if (Strana == Strana.Crni)
                {
                    //Kralj i top nisu micani
                    if (Ploča.ploca[0, 4].figura.brojKretnji == 0 && Ploča.ploca[0, 7].figura.brojKretnji == 0)
                    {
                        //Polje na kojem se nalazi kralj nije pod udarom
                        if (ProvjeriUdarNaPolje(Strana.Crni, Ploča.ploca[0, 4]) == false)
                        {
                            //Polje do kralja nije zauzeto i nije pod udarom
                            if (Ploča.ploca[0, 5].figura == null && ProvjeriUdarNaPolje(Strana.Crni, Ploča.ploca[0, 5]) == false)
                            {
                                //Drugo polje do kralja nije zauzeto i nije pod udarom
                                if (Ploča.ploca[0, 6].figura == null && ProvjeriUdarNaPolje(Strana.Crni, Ploča.ploca[0, 6]) == false)
                                {
                                    poruka = "Mala Rohada";
                                    return poruka;
                                }
                            }
                        }
                    }
                }
            }
            return poruka;
        }



        //NPR: c2 c3 //Pokriva samo obično kretanje (rohada i promocija neulaze), koristi se u metodi OdigrajPotez()
        public static string PretvoriStringPotez(string PotezIgrača)
        {
            string rezultat = "";
            switch (PotezIgrača[0])
            {
                case 'a': rezultat += "0"; break;
                case 'b': rezultat += "1"; break;
                case 'c': rezultat += "2"; break;
                case 'd': rezultat += "3"; break;
                case 'e': rezultat += "4"; break;
                case 'f': rezultat += "5"; break;
                case 'g': rezultat += "6"; break;
                case 'h': rezultat += "7"; break;
            }
            switch (PotezIgrača[1])
            {
                case '8': rezultat += "0"; break;
                case '7': rezultat += "1"; break;
                case '6': rezultat += "2"; break;
                case '5': rezultat += "3"; break;
                case '4': rezultat += "4"; break;
                case '3': rezultat += "5"; break;
                case '2': rezultat += "6"; break;
                case '1': rezultat += "7"; break;
            }
            rezultat += " ";
            switch (PotezIgrača[3])
            {
                case 'a': rezultat += "0"; break;
                case 'b': rezultat += "1"; break;
                case 'c': rezultat += "2"; break;
                case 'd': rezultat += "3"; break;
                case 'e': rezultat += "4"; break;
                case 'f': rezultat += "5"; break;
                case 'g': rezultat += "6"; break;
                case 'h': rezultat += "7"; break;
            }
            switch (PotezIgrača[4])
            {
                case '8': rezultat += "0"; break;
                case '7': rezultat += "1"; break;
                case '6': rezultat += "2"; break;
                case '5': rezultat += "3"; break;
                case '4': rezultat += "4"; break;
                case '3': rezultat += "5"; break;
                case '2': rezultat += "6"; break;
                case '1': rezultat += "7"; break;
            }
            return rezultat;
        }



        public static string PretvoriIntegerPotez(string TrenutnoPolje, string NovoPolje)
        {
            string rezultat = "";
            switch (TrenutnoPolje[1])
            {
                case '0': rezultat += "a"; break;
                case '1': rezultat += "b"; break;
                case '2': rezultat += "c"; break;
                case '3': rezultat += "d"; break;
                case '4': rezultat += "e"; break;
                case '5': rezultat += "f"; break;
                case '6': rezultat += "g"; break;
                case '7': rezultat += "h"; break;
            }
            switch (TrenutnoPolje[0])
            {
                case '0': rezultat += "8"; break;
                case '1': rezultat += "7"; break;
                case '2': rezultat += "6"; break;
                case '3': rezultat += "5"; break;
                case '4': rezultat += "4"; break;
                case '5': rezultat += "3"; break;
                case '6': rezultat += "2"; break;
                case '7': rezultat += "1"; break;
            }
            rezultat += " ";
            switch (NovoPolje[1])
            {
                case '0': rezultat += "a"; break;
                case '1': rezultat += "b"; break;
                case '2': rezultat += "c"; break;
                case '3': rezultat += "d"; break;
                case '4': rezultat += "e"; break;
                case '5': rezultat += "f"; break;
                case '6': rezultat += "g"; break;
                case '7': rezultat += "h"; break;
            }
            switch (NovoPolje[0])
            {
                case '0': rezultat += "8"; break;
                case '1': rezultat += "7"; break;
                case '2': rezultat += "6"; break;
                case '3': rezultat += "5"; break;
                case '4': rezultat += "4"; break;
                case '5': rezultat += "3"; break;
                case '6': rezultat += "2"; break;
                case '7': rezultat += "1"; break;
            }
            return rezultat;
        }



        public static void OdigrajPotezBOT()
        {

        }



        //Provjerava poziciju svih pijuna jedne i druge strane, i provodi promociju u određenom slučaju, poziva se na kraju svakog poteza
        public static void ProvjeriIIzvršiPromociju(Strana Strana)
        {
            if (Strana == Strana.Bijeli)
            {
                foreach(Figura figura in Ploča.listaFiguraBijeli)
                {
                    if (figura.pozRed == 0 && figura.naziv == "Pijun")
                    {
                        figura.naziv = "Kraljica";
                        figura.oznaka = "♛";
                    }
                }
            }
            if(Strana == Strana.Crni)
            {
                foreach(Figura figura in Ploča.listaFiguraCrni)
                {
                    if (figura.pozRed == 7 && figura.naziv == "Pijun")
                    {
                        figura.naziv = "Kraljica";
                        figura.oznaka = "♛";
                    }
                }
            }
        }



        //Vraća true ako je polje na udaru
        public static bool ProvjeriUdarNaPolje(Strana Strana, Polje Polje)
        {
            bool naUdaru = false;
            string promatranoPolje = Polje.red.ToString()+Polje.stupac.ToString();
            IzračunajMogućePoteze(Strana);
            List<string> listaMogućihPoteza = new List<string>();
            if(Strana == Strana.Bijeli)
            {
                foreach(Figura figura in Ploča.listaFiguraCrni)
                {
                    foreach(string mogućPotez in figura.listaMogućihPoteza)
                    {
                        listaMogućihPoteza.Add(mogućPotez);
                    }
                }
            }
            else
            {
                foreach (Figura figura in Ploča.listaFiguraBijeli)
                {
                    foreach (string mogućPotez in figura.listaMogućihPoteza)
                    {
                        listaMogućihPoteza.Add(mogućPotez);
                    }
                }
            }
            if (listaMogućihPoteza.Contains(promatranoPolje)) naUdaru = true;
            return naUdaru;
        }



        public static void ProvjeriUdarNaKralja(Strana Strana)
        {
            List<string> listaNeprijateljskihPoteza = new List<string>();
            string položajKralja = "";
            if (Strana == Strana.Bijeli)
            {
                foreach (Figura figura in Ploča.listaFiguraBijeli)
                {
                    if (figura.naziv == "Kralj")
                    {
                        položajKralja = figura.pozRed.ToString() + figura.pozStupac.ToString();
                    }
                }
                foreach (Figura figurica in Ploča.listaFiguraCrni)
                {
                    foreach (string potez in figurica.listaMogućihPoteza) listaNeprijateljskihPoteza.Add(potez);
                }
                if (listaNeprijateljskihPoteza.Contains(položajKralja))
                {
                    Ploča.jeŠah = true;
                    Ploča.stranaUŠahu = Strana.Bijeli;
                }
                if (!listaNeprijateljskihPoteza.Contains(položajKralja))
                {
                    Ploča.jeŠah = false;
                }
            }
            if (Strana == Strana.Crni)
            {
                foreach (Figura figura in Ploča.listaFiguraCrni)
                {
                    if (figura.naziv == "Kralj")
                    {
                        položajKralja = figura.pozRed.ToString() + figura.pozStupac.ToString();
                    }
                }
                foreach (Figura figurica in Ploča.listaFiguraBijeli)
                {
                    foreach (string potez in figurica.listaMogućihPoteza) listaNeprijateljskihPoteza.Add(potez);
                }
                if (listaNeprijateljskihPoteza.Contains(položajKralja))
                {
                    Ploča.jeŠah = true;
                    Ploča.stranaUŠahu = Strana.Crni;
                }
                if(!listaNeprijateljskihPoteza.Contains(položajKralja))
                {
                    Ploča.jeŠah = false;
                }
            }
        }



        public static void ProvjeriŠah(Strana Strana)
        {
            ProvjeriUdarNaKralja(Strana);
            //Ako je šah
            if(Ploča.jeŠah==true && Ploča.stranaUŠahu == Strana)
            {
                int brojacPoteza = 0;
                OgraničiFigureZbogŠaha(Strana);
                //Provjera mata
                if(Strana == Strana.Bijeli)
                {
                    foreach(Figura figura in Ploča.listaFiguraBijeli)
                    {
                        foreach(string potez in figura.listaMogućihPoteza)
                        {
                            brojacPoteza++;
                        }
                    }
                }
                if(Strana == Strana.Crni)
                {
                    foreach(Figura figura in Ploča.listaFiguraCrni)
                    {
                        foreach(string potez in figura.listaMogućihPoteza)
                        {
                            brojacPoteza++;
                        }
                    }
                }
                //Ako igrač nemože odigrati niti jedan potez igra završava
                if (brojacPoteza<1)
                {
                    Ploča.statusIgre = false;
                }
            }
            //Ako nije
            if (Ploča.jeŠah == false)
            {
                //Ista metoda
                OgraničiFigureZbogŠaha(Strana);
            }
        }



        public static void OgraničiFigureZbogŠaha(Strana Strana)
        {
            List<string> ListaNovihMogućihPoteza = new List<string>();
            Figura sačuvanaFigura = null;
            //Ako je šah nad bijelim
            if(Strana == Strana.Bijeli)
            {
                foreach(Figura figura in Ploča.listaFiguraBijeli)
                {
                    ListaNovihMogućihPoteza.Clear();
                    foreach(string potez in figura.listaMogućihPoteza)
                    {
                        string lokacijaFigure = figura.pozRed.ToString() + figura.pozStupac.ToString();
                        sačuvanaFigura = ProvjeriNapadNaFiguru(Strana.Bijeli, PretvoriIntegerPotez(lokacijaFigure, potez));
                        OdigrajPotez(Strana.Bijeli, PretvoriIntegerPotez(lokacijaFigure, potez));
                        IzračunajMogućePoteze(Strana.Crni);
                        ProvjeriUdarNaKralja(Strana.Bijeli);
                        if (Ploča.jeŠah != true)
                        {
                            ListaNovihMogućihPoteza.Add(potez);
                        }
                        PoništiPotez(Strana.Bijeli, PretvoriIntegerPotez(lokacijaFigure, potez),sačuvanaFigura);
                    }
                    figura.listaMogućihPoteza.Clear();
                    foreach(string potez in ListaNovihMogućihPoteza)
                    {
                        figura.listaMogućihPoteza.Add(potez);
                    }
                }
            }
            //Ako je šah nad crnim
            if(Strana == Strana.Crni)
            {
                foreach (Figura figura in Ploča.listaFiguraCrni)
                {
                    ListaNovihMogućihPoteza.Clear();
                    foreach (string potez in figura.listaMogućihPoteza)
                    {
                        string lokacijaFigure = figura.pozRed.ToString() + figura.pozStupac.ToString();
                        sačuvanaFigura = ProvjeriNapadNaFiguru(Strana.Crni, PretvoriIntegerPotez(lokacijaFigure, potez));
                        OdigrajPotez(Strana.Crni, PretvoriIntegerPotez(lokacijaFigure, potez));
                        IzračunajMogućePoteze(Strana.Bijeli);
                        ProvjeriUdarNaKralja(Strana.Crni);
                        if (Ploča.jeŠah != true)
                        {
                            ListaNovihMogućihPoteza.Add(potez);
                        }
                        PoništiPotez(Strana.Crni, PretvoriIntegerPotez(lokacijaFigure, potez), sačuvanaFigura);
                    }
                    figura.listaMogućihPoteza.Clear();
                    foreach (string potez in ListaNovihMogućihPoteza)
                    {
                        figura.listaMogućihPoteza.Add(potez);
                    }
                }
            }
        }



        public static void PoništiPotez(Strana Strana, string PotezIgrača, Figura NeprijateljskaFigura)
        {
            string potezIgrača = PretvoriStringPotez(PotezIgrača);
            //Je napad na neprijateljsku figuru
            Ploča.ploca[int.Parse(potezIgrača[1].ToString()), int.Parse(potezIgrača[0].ToString())].figura = Ploča.ploca[int.Parse(potezIgrača[4].ToString()), int.Parse(potezIgrača[3].ToString())].figura;
            Ploča.ploca[int.Parse(potezIgrača[1].ToString()), int.Parse(potezIgrača[0].ToString())].figura.pozRed = int.Parse(potezIgrača[1].ToString());
            Ploča.ploca[int.Parse(potezIgrača[1].ToString()), int.Parse(potezIgrača[0].ToString())].figura.pozStupac = int.Parse(potezIgrača[0].ToString());
            Ploča.ploca[int.Parse(potezIgrača[4].ToString()), int.Parse(potezIgrača[3].ToString())].figura = null;
            Ploča.ploca[int.Parse(potezIgrača[1].ToString()), int.Parse(potezIgrača[0].ToString())].figura.brojKretnji--;
            if (NeprijateljskaFigura != null)
            {
                //Vrati figuru
                if (Strana == Strana.Bijeli) Ploča.listaFiguraCrni.Add(NeprijateljskaFigura);
                if (Strana == Strana.Crni) Ploča.listaFiguraBijeli.Add(NeprijateljskaFigura);
                Ploča.ploca[int.Parse(potezIgrača[4].ToString()), int.Parse(potezIgrača[3].ToString())].figura = NeprijateljskaFigura;
            }
            Ploča.triZadnjaPoteza.RemoveAt(Ploča.triZadnjaPoteza.Count-1);
        }



        public static Figura ProvjeriNapadNaFiguru(Strana Strana,string PotezIgrača)
        {
            Figura sačuvanaFigura = null; 
            string tipPoteza = ProvjeriPotez(Strana, PotezIgrača);
            if (tipPoteza == "Običan")
            {
                string potezIgrača = PretvoriStringPotez(PotezIgrača);
                //Ukoliko se na željenom mjestu nalazi neprijateljska figura
                if (Ploča.ploca[int.Parse(potezIgrača[4].ToString()), int.Parse(potezIgrača[3].ToString())].figura != null && Ploča.ploca[int.Parse(potezIgrača[4].ToString()), int.Parse(potezIgrača[3].ToString())].figura.strana != Strana)
                {
                    sačuvanaFigura = Ploča.ploca[int.Parse(potezIgrača[4].ToString()), int.Parse(potezIgrača[3].ToString())].figura;
                }
            }
            return sačuvanaFigura;
        }



        //Ako su sva mjesta oko kralja zauzeta ali kralj nije ugrozen
        public static void ProvjeriJelNeriješeno(Strana Strana)
        {

            List<string> listaPotezaBijelog = new List<string>();
            List<string> listaPotezaCrnog = new List<string>();
            List<Figura> listaFigura = new List<Figura>();
            bool samoKraljevi = true;
            if(Strana == Strana.Bijeli)
            {
                foreach (Figura figura in Ploča.listaFiguraBijeli)
                {
                    listaFigura.Add(figura);
                    foreach (string potez in figura.listaMogućihPoteza)
                    {
                        listaPotezaBijelog.Add(potez);
                    }
                }
                if (listaPotezaBijelog.Count == 0) Ploča.neriješeno = true;
            }
            if(Strana == Strana.Crni)
            {
                foreach (Figura figura in Ploča.listaFiguraCrni)
                {
                    listaFigura.Add(figura);
                    foreach (string potez in figura.listaMogućihPoteza)
                    {
                        listaPotezaCrnog.Add(potez);
                    }
                }
                if (listaPotezaCrnog.Count == 0) Ploča.neriješeno = true;
            }
            foreach (Figura figura in listaFigura)
            {
                if (figura.naziv != "Kralj")
                {
                    samoKraljevi = false;
                }
            }
            if (samoKraljevi == true) Ploča.neriješeno = true;
        }



        public static void OčistiPotezeFigura()
        {
            foreach(Figura figura in Ploča.listaFiguraBijeli)
            {
                figura.listaMogućihPoteza.Clear();
            }
            foreach(Figura figura in Ploča.listaFiguraCrni)
            {
                figura.listaMogućihPoteza.Clear();
            }
        }



        public static void ProvjeriThreefoldRepeticiju()
        {
            string prvaPozPrvi = "";
            string drugaPozPrvi = "";
            string prvaPozDrugi = "";
            string drugaPozDrugi = "";

            for (int brojPotezaUListi = 0; brojPotezaUListi < Ploča.triZadnjaPoteza.Count; brojPotezaUListi++)
            {
                if (brojPotezaUListi % 2 == 0)
                {
                    if (brojPotezaUListi == 0)
                    {
                        prvaPozPrvi = Ploča.triZadnjaPoteza[brojPotezaUListi][0].ToString() + Ploča.triZadnjaPoteza[brojPotezaUListi][1].ToString();
                        drugaPozPrvi = Ploča.triZadnjaPoteza[brojPotezaUListi][3].ToString() + Ploča.triZadnjaPoteza[brojPotezaUListi][4].ToString();
                    }
                    if (brojPotezaUListi > 1)
                    {
                        if ((Ploča.triZadnjaPoteza[brojPotezaUListi][0].ToString() + Ploča.triZadnjaPoteza[brojPotezaUListi][1].ToString()) == drugaPozPrvi)
                        {
                            if ((Ploča.triZadnjaPoteza[brojPotezaUListi][3].ToString() + Ploča.triZadnjaPoteza[brojPotezaUListi][4].ToString()) == prvaPozPrvi)
                            {
                                prvaPozPrvi = Ploča.triZadnjaPoteza[brojPotezaUListi][0].ToString() + Ploča.triZadnjaPoteza[brojPotezaUListi][1].ToString();
                                drugaPozPrvi = Ploča.triZadnjaPoteza[brojPotezaUListi][3].ToString() + Ploča.triZadnjaPoteza[brojPotezaUListi][4].ToString();
                            }
                            else
                            {
                                Ploča.triZadnjaPoteza.Clear();
                                break;
                            }
                        }
                        else
                        {
                            Ploča.triZadnjaPoteza.Clear();
                            break;
                        }
                    }
                }
                if (brojPotezaUListi % 2 == 1)
                {
                    if (brojPotezaUListi == 1)
                    {
                        prvaPozDrugi = Ploča.triZadnjaPoteza[brojPotezaUListi][0].ToString() + Ploča.triZadnjaPoteza[brojPotezaUListi][1].ToString();
                        drugaPozDrugi = Ploča.triZadnjaPoteza[brojPotezaUListi][3].ToString() + Ploča.triZadnjaPoteza[brojPotezaUListi][4].ToString();
                    }
                    if (brojPotezaUListi > 1)
                    {
                        if ((Ploča.triZadnjaPoteza[brojPotezaUListi][0].ToString() + Ploča.triZadnjaPoteza[brojPotezaUListi][1].ToString()) == drugaPozDrugi)
                        {
                            if ((Ploča.triZadnjaPoteza[brojPotezaUListi][3].ToString() + Ploča.triZadnjaPoteza[brojPotezaUListi][4].ToString()) == prvaPozDrugi)
                            {
                                prvaPozDrugi = Ploča.triZadnjaPoteza[brojPotezaUListi][0].ToString() + Ploča.triZadnjaPoteza[brojPotezaUListi][1].ToString();
                                drugaPozDrugi = Ploča.triZadnjaPoteza[brojPotezaUListi][3].ToString() + Ploča.triZadnjaPoteza[brojPotezaUListi][4].ToString();
                            }
                            else
                            {
                                Ploča.triZadnjaPoteza.Clear();
                                break;
                            }
                        }
                        else
                        {
                            Ploča.triZadnjaPoteza.Clear();
                            break;
                        }
                    }
                }
            }
            if (Ploča.triZadnjaPoteza.Count == 8)
            {
                Ploča.neriješeno = true;
            }
        }
    }
}
