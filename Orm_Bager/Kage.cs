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
        private string tablename = "Kage";
        private string navn;
        private string storrelse;
        private string join;
        private string query;
        private int pris;

        public Kage(SqlConnection c)
        {
            myConn = c;
        }

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

        public string Sstorrelse
        {
            get
            {
                return storrelse;
            }
            set
            {
                storrelse = value;
                query = "SELECT Id FROM Storrelse WHERE Storrelse.Storrelse = '" + storrelse + "'";
                //Console.WriteLine(query);
                myConn.Open();
                SqlCommand cmd = new SqlCommand(query, myConn);
                storrelse = Convert.ToString(cmd.ExecuteScalar());
                myConn.Close();
                //Console.WriteLine(storrelse);
            }
        }

        public void Save(int crud, int valg)
        {
            CRUD Crud = new CRUD(myConn);
            Storrelse s = new Storrelse(myConn);
            

            List<string> Keys = new List<string>();
            Keys.Add("Navn");
            Keys.Add("Pris");
            Keys.Add("Storrelse_id"); 

            ArrayList Values = new ArrayList();
            Values.Add(Navn);
            Values.Add(Pris);
            Values.Add(Sstorrelse);

            switch (crud)
            {
                case 1:
                    Crud.Create(Keys, Values, tablename);
                    break;
                case 2:
                    break;
                case 3:
                    s.Save(5,0);
                    join = s.tablename + "." + s.Keys[0] + " FROM " + tablename + " INNER JOIN " + s.tablename + " ON " + tablename + "." + Keys[2] + " = " + s.tablename + "." + s.Keys[0];
                    Console.WriteLine(join);
                    Crud.Show(Keys, tablename, join);
                    break;
                case 4:
                    Keys.Add("Id");
                    break;
            }
        }
    }
}
