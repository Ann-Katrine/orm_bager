using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;

namespace Orm_Bager
{
    class Kunde
    {
        private SqlConnection myConn;
        private string Tablename = "Kunde"; // for at få en værdi der er tables navn fra databasen
        private string fornavn;      // værdi til fornavn kolonne
        private string efternavn;    // værdi til efternavn kolonne
        private string vejnavn;      // værdi til vejnavn kolonne 
        private int mobil;           // værdi til mobil kolonne
        private int postnummer;      // værdi til postnummer kolonne
        private string join;         // for at kunne lave en join i SQL

        /// <summary>
        /// for at få forbindelse til databsen fra program
        /// </summary>
        /// <param name="c"></param>
        public Kunde(SqlConnection c)
        {
            myConn = c;
        }

        /// <summary>
        /// sætter værdier og trækker værdierne ud for Fornavn
        /// </summary>
        public string Fornavn
        {
            get
            {
                return fornavn;
            }
            set
            {
                fornavn = value;
            }
        }

        /// <summary>
        /// sætter værdier og trækker værdierne ud for Efternavn
        /// </summary>
        public string Efternavn
        {
            get
            {
                return efternavn;
            }
            set
            {
                efternavn = value;
            }
        }

        /// <summary>
        /// sætter værdier og trækker værdierne ud for Vejnavn
        /// </summary>
        public string Vejnavn
        {
            get
            {
                return vejnavn;
            }
            set
            {
                vejnavn = value;
            }
        }

        /// <summary>
        /// sætter værdier og trækker værdierne ud for mobil
        /// </summary>
        public int Mobil
        {
            get
            {
                return mobil;
            }
            set
            {
                mobil = value;
            }
        }

        /// <summary>
        /// sætter værdier og trækker værdierne ud for postnummer
        /// </summary>
        public int Postnummer
        {
            get
            {
                return postnummer;
            }
            set
            {
                postnummer = value;
            }
        }

        /// <summary>
        /// vælger hvor man skal hen vedrørene CRUD
        /// </summary>
        /// <param name="crud"></param>
        /// <param name="valg"></param>
        public void Save(int crud, int valg)
        {
            CRUD Crud = new CRUD(myConn);   // forbindelse til CRUP class'en
            PostnummerOgBy p = new PostnummerOgBy(myConn);  // forbindelse til postnummerogby class'en

            List<string> Keys = new List<string>();     // list til alle kolonnenavne
            Keys.Add("Fornavn");
            Keys.Add("Efternavn");
            Keys.Add("Mobil");
            Keys.Add("VejNavn");
            Keys.Add("Postnummer_id");

            ArrayList Values = new ArrayList();     // arraylist til alle værdierne man skrever ind
            Values.Add(Fornavn);
            Values.Add(Efternavn);
            Values.Add(Mobil);
            Values.Add(Vejnavn);
            Values.Add(Postnummer);

            ArrayList gValues = new ArrayList();

            switch (crud)   // hvor man bliver videre sendt til i CRUD
            {
                case 1:
                    Crud.Create(Keys, Values, Tablename);
                    break;
                case 2:
                    //Crud.Update(Keys, Values, )
                    break;
                case 3:
                    p.Save(5,0);    // for lige at tilføje postnummerOgBy's tablenavn og kolonnenavne
                    // join er som navnet siger at man laver en join her er det inner join der bliver brugt
                    join = p.Tablename + "." + p.Keys[1] + " FROM "  + Tablename + " INNER JOIN " + p.Tablename + " ON " + Tablename + "." + Keys[4]  + " = " + p.Tablename + "." + p.Keys[0];
                    //Console.WriteLine(join);
                    Crud.Show(Keys, Tablename, join);
                    break;
                case 4:
                    //getid = "SELECT Id FROM " + Tablename + " WHERE " + Tablename + "." + Keys[2] + " = " + Values[2];
                    //Console.WriteLine(getid);
                    Crud.Delect(Keys, Values, Tablename, 1);
                    break;
            }

        }
    }
}
