using System;
using System.Data.SqlClient;

namespace Orm_Bager
{
    class Program
    {
        static void Main(string[] args)
        {
            int valg;
            string svar;

            SqlConnection myConn = new SqlConnection("Server=localhost;database=Bager;uid=anne3013;pwd=1234");
            PostnummerOgBy p = new PostnummerOgBy(myConn);

            Console.WriteLine("Vælg hvilken du vil lave noget med");
            Console.WriteLine("#1 - postnummer og by");
            Console.WriteLine("#2 - Størrelse");
            Console.WriteLine("#3 - Kunde");
            Console.WriteLine("#4 - Kage");
            valg = Convert.ToInt32(Console.ReadLine());

            switch (valg)
            {
                case 1:
                    PostnummerOgBy();
                    break;
                case 2:
                    StorrelseTilKager();
                    break;
                case 3:
                    Kunder();
                    break;
                case 4:
                    Kager();
                    break;
                default:
                    Console.WriteLine("Du har skrevet en værdi der ikke findes");
                    break;
            }

            void PostnummerOgBy()
            {
                //CRUD crud = new CRUD();
                int postnumer;
                int gpostnr;
                string bynavn;
                string gbynavn;


                Console.WriteLine("Vælg hvad du vil i Postnummer og by");
                Console.WriteLine("#1 - Lav ny postnummer og by");
                Console.WriteLine("#2 - Updater postnummer og by");
                Console.WriteLine("#3 - Hvis postnummer og by");
                Console.WriteLine("#4 - Slet postnummer og by");
                valg = Convert.ToInt32(Console.ReadLine());

                switch (valg)
                {
                    case 1:
                        Console.WriteLine("skriv et postnummer");
                        postnumer = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("skriv et bynavn til");
                        bynavn = Console.ReadLine();

                        p.Postnummer = postnumer;
                        p.Bynavn = bynavn;
                        //crud.Create();
                        p.Save(valg, 0);
                        break;
                    case 2:
                        Console.WriteLine("Hvor meget vil du ændre");
                        Console.WriteLine("#1 - Postnummer");
                        Console.WriteLine("#2 - By");
                        Console.WriteLine("#3 - Postnummer og by");
                        valg = Convert.ToInt32(Console.ReadLine());

                        switch (valg)
                        {
                            case 1:
                                Console.WriteLine("Hvilket postnummer vil du ændre?");
                                //p.Show();
                                p.Save(3, 0);
                                gpostnr = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Hvad skal det ændres til?");
                                postnumer = Convert.ToInt32(Console.ReadLine());

                                p.Postnummer = postnumer;
                                p.GentagPostnr = gpostnr;
                                //p.Update(valg);
                                p.Save(2, valg);
                                break;
                            case 2:
                                Console.WriteLine("Hvilken by vil du ændre?");
                                //p.Show();
                                p.Save(3, 0);
                                gbynavn = Console.ReadLine();
                                Console.WriteLine("Hvad skal byen hedder?");
                                bynavn = Console.ReadLine();

                                p.GentagBynavn = gbynavn;
                                p.Bynavn = bynavn;
                                //p.Update(valg);
                                p.Save(2, valg);
                                break;
                            case 3:
                                Console.WriteLine("Hvilket postnummer vil du ændre?");
                                //p.Show();
                                p.Save(3, 0);
                                gpostnr = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Hvad skal det ændres til?");
                                postnumer = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Hvad skal byen hedder?");
                                bynavn = Console.ReadLine();

                                p.Postnummer = postnumer;
                                p.GentagPostnr = gpostnr;
                                p.Bynavn = bynavn;
                                //p.Update(valg);
                                p.Save(2, valg);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        //p.Show();
                        p.Save(valg,0);
                        break;
                    case 4:
                        Console.WriteLine("Hvilket postnummer");
                        //p.Show();
                        p.Save(3,0);
                        postnumer = Convert.ToInt32(Console.ReadLine());
                        p.Postnummer = postnumer;

                        Console.WriteLine("Vil du slettet den");
                        svar = Console.ReadLine();
                        if (svar == "ja")
                        {
                            Console.WriteLine("Okay, den bliver slettet");
                            p.Save(valg,0);
                        }
                        break;
                    default:
                        Console.WriteLine("Du har skrevet en værdi der ikke findes");
                        break;
                }

                
                
            }

            void StorrelseTilKager()
            {
                Storrelse s = new Storrelse(myConn);
                string storrelse;

                Console.WriteLine("Vælg hvad du vil i størrelse");
                Console.WriteLine("#1 - Lav ny størrelse");
                Console.WriteLine("#2 - Updater størrelse");
                Console.WriteLine("#3 - Hvis størrelse");
                Console.WriteLine("#4 - Slet størrelse");
                valg = Convert.ToInt32(Console.ReadLine());

                switch (valg)
                {
                    case 1:
                        Console.WriteLine("Skriv en ny størrese");
                        storrelse = Console.ReadLine();

                        s.storrelse = storrelse;
                        s.Save(valg, 0);
                        //s.Insert();
                        break;
                    case 2:

                        s.Update();
                        break;
                    case 3:
                        //s.Show();
                        s.Save(valg, 0);
                        break;
                    case 4:
                        Console.WriteLine("Hvilken størrelse");
                        //p.Show();
                        s.Save(3, 0);
                        storrelse = Console.ReadLine();
                        s.storrelse = storrelse;

                        Console.WriteLine("Vil du slettet den");
                        svar = Console.ReadLine();
                        if (svar == "ja")
                        {
                            Console.WriteLine("Okay, den bliver slettet");
                            s.Save(valg, 0);
                        }
                        //s.Delect();
                        break;
                    default:
                        break;
                }
            }

            void Kunder()
            {
                Kunde k = new Kunde(myConn);
                string fornavn;
                string efternavn;
                string vejnavn;
                string vejnummer;
                int mobil;
                int postnummer;

                Console.WriteLine("Vælg hvad du vil i kunde");
                Console.WriteLine("#1 - Lav ny kunde");
                Console.WriteLine("#2 - Updater kunde");
                Console.WriteLine("#3 - Hvis kunde");
                Console.WriteLine("#4 - Slet kunde");
                valg = Convert.ToInt32(Console.ReadLine());

                switch (valg)
                {
                    case 1:
                        Console.WriteLine("Hvad er fornavnet?");
                        fornavn = Console.ReadLine();
                        Console.WriteLine("Hvad er efternavnet?");
                        efternavn = Console.ReadLine();
                        Console.WriteLine("Hvad er vejnavnet?");
                        vejnavn = Console.ReadLine();
                        Console.WriteLine("Hvad er vejnummeret?");
                        vejnummer = Console.ReadLine();
                        Console.WriteLine("Hvad er mobilnummeret?");
                        mobil = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Hvad er det for en by?");
                        p.Save(3,0);
                        postnummer = Convert.ToInt32(Console.ReadLine());

                        k.Fornavn = fornavn;
                        k.Efternavn = efternavn;
                        k.Vejnavn = vejnavn + " " + vejnummer;
                        k.Mobil = mobil;
                        k.Postnummer = postnummer;
                        k.Save(valg, 0);
                        break;
                    case 2:
                        break;
                    case 3:
                        k.Save(valg,0);
                        break;
                    case 4:
                        Console.WriteLine("Hvilken person?");
                        k.Save(3, 0);
                        fornavn = Console.ReadLine();
                        Console.WriteLine("Skal skrive mobil nummer også");
                        mobil = Convert.ToInt32(Console.ReadLine());

                        k.Mobil = mobil;
                        Console.WriteLine("Vil du slettet den");
                        svar = Console.ReadLine();
                        if (svar == "ja")
                        {
                            Console.WriteLine("Okay, den bliver slettet");
                            //k.Save(valg, 0);
                        }
                        break;
                    default:
                        Console.WriteLine("Du har skrevet en værdi der ikke findes");
                        break;
                }
            } // ikke færdig

            void Kager()
            {
                Storrelse s = new Storrelse(myConn);
                Kage k = new Kage(myConn);
                string navn;
                string storrelse;
                int pris;

                Console.WriteLine("Vælg hvad du vil i kage");
                Console.WriteLine("#1 - Lav ny kage");
                Console.WriteLine("#2 - Updater kage");
                Console.WriteLine("#3 - Hvis kage");
                Console.WriteLine("#4 - Slet kage");
                valg = Convert.ToInt32(Console.ReadLine());

                switch (valg)
                {
                    case 1:
                        Console.WriteLine("Hvad hedder kagen?");
                        navn = Console.ReadLine();
                        Console.WriteLine("Hvilken størrelse er kagen?");
                        s.Save(3, 0);
                        storrelse = Console.ReadLine();
                        Console.WriteLine("Hvor meget koster kagen?");
                        pris = Convert.ToInt32(Console.ReadLine());

                        k.Navn = navn;
                        k.Pris = pris;
                        k.Sstorrelse = storrelse;
                        k.Save(valg, 0);
                        break;
                    case 2:
                        break;
                    case 3:
                        k.Save(valg, 0);
                        break;
                    case 4:
                        Console.WriteLine("Hvilken person?");
                        k.Save(3, 0);
                        
                        break;
                    default:
                        break;
                }
            } // ikke færdig

        }
    }
}
