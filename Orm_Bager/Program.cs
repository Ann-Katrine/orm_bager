using System;
using System.Data.SqlClient;

namespace Orm_Bager
{
    class Program
    {

        static void Main(string[] args)
        {
            int valg = 0;  // vælge vej man skal gå
            string svar;   // til delete, om man vil delete
            int intværdi = 0; 

            // laver forbindelse til databasen med det login man har lavet
            SqlConnection myConn = new SqlConnection("Server=localhost;database=Bager;uid=anne3013;pwd=1234");  
            PostnummerOgBy p = new PostnummerOgBy(myConn);  // laver forbindels til postnummerogby

            Console.WriteLine("Vælg hvilken du vil lave noget med");
            Console.WriteLine("#1 - postnummer og by");
            Console.WriteLine("#2 - Størrelse");
            Console.WriteLine("#3 - Kunde");
            Console.WriteLine("#4 - Kage");
            fejlFindVedValg();  // for at vælge

            // for at finde ud af hvor man skal hen efter ens valg
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
            }

            /// <summary>
            /// hvad man kan gør med postnummerogby
            /// </summary>
            /// <param name="c"></param>
            void PostnummerOgBy()
            {
                //CRUD crud = new CRUD();
                int postnumer = 0;  // værdi for postnummer
                int gpostnr;        // værdi for gentagelse af postnummer til update
                string bynavn;      // værdi for bynavn
                string gbynavn;     // værdi for gentagelse af postnummer til update


                Console.WriteLine("Vælg hvad du vil i Postnummer og by");
                Console.WriteLine("#1 - Lav ny postnummer og by");
                Console.WriteLine("#2 - Updater postnummer og by");
                Console.WriteLine("#3 - Hvis postnummer og by");
                Console.WriteLine("#4 - Slet postnummer og by");
                fejlFindVedValg();  // for at man kan vælge

                switch (valg)   // for at vises hen til hvad man har valgt
                {
                    case 1: // create
                        Console.WriteLine("skriv et postnummer");
                        do
                        {
                            try
                            {
                                postnumer = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                postnumer = 0;
                                Console.WriteLine("du har ikke skrevet et tal");
                            }
                        } while (postnumer == 0);   //for at fejl finde og få en værdi til postnummer
                        Console.WriteLine("skriv et bynavn til");
                        bynavn = Console.ReadLine();    // for at få bynavn værdi

                        p.Postnummer = postnumer;   // sender værdien fra postummer variablen på denne side over til metoden postnummer 
                        p.Bynavn = bynavn;          // sender værdien fra Bynavn variablen på denne side over til metoden Bynavn
                        //crud.Create();
                        p.Save(valg, 0);    // sendes videre i procesen til at oprette et postnummer
                        break;

                    case 2: // update
                        Console.WriteLine("Hvor meget vil du ændre");
                        Console.WriteLine("#1 - Postnummer");
                        Console.WriteLine("#2 - By");
                        Console.WriteLine("#3 - Postnummer og by");
                        fejlFindVedValg();  // for at man kan vælge

                        switch (valg)   // for at vises hen til hvad man har valgt
                        {
                            case 1:     // kun postnummer
                                Console.WriteLine("Hvilket postnummer vil du ændre?");
                                //p.Show();
                                p.Save(3, 0);
                                gpostnr = Convert.ToInt32(Console.ReadLine());
                                /*værdier();
                                gpostnr = intværdi;
                                Console.WriteLine(gpostnr);*/
                                Console.WriteLine("Hvad skal det ændres til?");
                                postnumer = Convert.ToInt32(Console.ReadLine());

                                p.Postnummer = postnumer;
                                p.GentagPostnr = gpostnr;
                                //p.Update(valg);
                                p.Save(2, valg);
                                break;
                            case 2:     // kun bynavn
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
                            case 3:     // begge
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
                        }
                        break;

                    case 3: // read
                        //p.Show();
                        p.Save(valg,0);
                        break;

                    case 4: // delete
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
                fejlFindVedValg();

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

                        //s.Update();
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
                fejlFindVedValg();

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
                        Console.WriteLine("Hvad er det for et postnummer?");
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
                fejlFindVedValg();

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
                }
            } // ikke færdig

            void fejlFindVedValg()
            {
                do
                {
                    valg = Convert.ToInt32(Console.ReadLine()); // skriver værdi
                    if (valg >= 5 || valg <= 0) // tjekker om rigtig værdi
                    {
                        Console.WriteLine("Du har ikke skrevet et tal imellem 1-4, så prøv igen.");
                    }
                } while (valg >= 5 || valg <= 0); // fejl finding med og rigtig tal
            } // for at fejl finde og ikke gentage samme proces flere gange

            void værdier()
            {
                intværdi = Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}
