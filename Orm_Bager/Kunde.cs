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
        private string Tablename = "Kunde";
        private string fornavn;
        private string efternavn;
        private string vejnavn;
        private string join;
        private string query;
        //private string getid;
        private int mobil;
        private int postnummer;

        public Kunde(SqlConnection c)
        {
            myConn = c;
        }

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

        public int Postnummer
        {
            get
            {
                return postnummer;
            }
            set
            {
                postnummer = value;
                query = "SELECT Postnr FROM Postnummer WHERE Postnummer.ByNavn = '" + postnummer + "'";
                myConn.Open();
                SqlCommand cmd = new SqlCommand(query, myConn);
                postnummer = Convert.ToInt32(cmd.ExecuteScalar());
                myConn.Close();
            }
        }

        public void Save(int crud, int valg)
        {
            CRUD Crud = new CRUD(myConn);
            PostnummerOgBy p = new PostnummerOgBy(myConn);

            List<string> Keys = new List<string>();
            Keys.Add("Fornavn");
            Keys.Add("Efternavn");
            Keys.Add("Mobil");
            Keys.Add("VejNavn");
            Keys.Add("Postnummer_id");

            ArrayList Values = new ArrayList();
            Values.Add(Fornavn);
            Values.Add(Efternavn);
            Values.Add(Mobil);
            Values.Add(Vejnavn);
            Values.Add(Postnummer);

            switch (crud)
            {
                case 1:
                    Crud.Create(Keys, Values, Tablename);
                    break;
                case 2:
                    break;
                case 3:
                    // Postnummer.ByNavn FROM Kunde INNER JOIN Postnummer ON Kunde.Postnummer_id = Postnummer.Postnr";
                    p.Save(5,0);
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
