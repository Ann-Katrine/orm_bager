using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;

namespace Orm_Bager
{
    class Kage
    {
        private SqlConnection myConn;
        private int pris;             // værdi til pris kolonne
        private string navn;          // værdi til navn kolonne
        private string storrelse;     // værdi til storrelse_id kolonne
        private string join;          // for at kunne lave en join i SQL
        private string query;         // at kunne lave lave SQL til at få id'et på storrelsen
        private string tablename = "Kage";      // for at få tablenavn fra databasse som en værdi

        /// <summary>
        /// for at få forbindelse til databsen fra program
        /// </summary>
        /// <param name="c"></param>
        public Kage(SqlConnection c)
        {
            myConn = c;
        }

        /// <summary>
        /// sætter værdier og trækker værdierne ud for navn
        /// </summary>
        public string Navn
        {
            get
            {
                return navn;
            }
            set
            {
                navn = value;
            }
        }

        /// <summary>
        /// sætter værdier og trækker værdierne ud for pris
        /// </summary>
        public int Pris
        {
            get
            {
                return pris;
            }
            set
            {
                pris = value;
            }
        }

        /// <summary>
        /// sætter værdier og trækker værdierne ud for storrelse
        /// </summary>
        public string Sstorrelse
        {
            get
            {
                return storrelse;
            }
            set // gør at man får id på den storrelse man vælger
            {
                storrelse = value;  // man får værdien fra program
                query = "SELECT Id FROM Storrelse WHERE Storrelse.Storrelse = '" + storrelse + "'";     // skriver en SQL kommando til at få id af storrelsen
                //Console.WriteLine(query);
                myConn.Open();  // åbner forbindelsen
                SqlCommand cmd = new SqlCommand(query, myConn);
                storrelse = Convert.ToString(cmd.ExecuteScalar());  // laver executeScalar om til en string
                myConn.Close(); // lukker forbindelsen
                //Console.WriteLine(storrelse);
            }
        }

        /// <summary>
        /// for at finde ud af hvor man skal sende en hen i CRUD
        /// </summary>
        /// <param name="crud"></param>
        /// <param name="valg"></param>
        public void Save(int crud, int valg)
        {
            CRUD Crud = new CRUD(myConn);   // forbindelse til CRUD
            Storrelse s = new Storrelse(myConn);    // forbindelse til Storrelse
            
            List<string> Keys = new List<string>();     // list til alle kolonnenavne
            Keys.Add("Navn");
            Keys.Add("Pris");
            Keys.Add("Storrelse_id"); 

            ArrayList Values = new ArrayList();     // arraylist til alle værdierne man skrever ind
            Values.Add(Navn);
            Values.Add(Pris);
            Values.Add(Sstorrelse);

            switch (crud)   // for at finde ud af hvor man skal sende hen
            {
                case 1:
                    Crud.Create(Keys, Values, tablename);
                    break;
                case 2:
                    break;
                case 3:
                    s.Save(5,0);    // for lige at tilføje storrelse's tablenavn og kolonnenavne
                    // join er som navnet siger at man laver en join her er det inner join der bliver brugt
                    join = s.tablename + "." + s.Keys[0] + " FROM " + tablename + " RIGHT JOIN " + s.tablename + " ON " + tablename + "." + Keys[2] + " = " + s.tablename + "." + s.Keys[1];
                    //Console.WriteLine(join);
                    Crud.Show(Keys, tablename, join);
                    break;
                case 4:
                    Keys.Add("Id");
                    break;
            }
        }
    }
}
