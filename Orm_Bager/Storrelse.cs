using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace Orm_Bager
{
    class Storrelse
    {
        private SqlConnection myConn;
        private string size;    // værdi til storrelse koloenne
        private string gsize;   // værdi til gentagelse storrelse til update
        //private string query;   
        public string tablename = "Storrelse";  // for at få tablenavn fra databasse som en værdi
        public List<string> Keys = new List<string>();  // list til kolonnenavne
        public ArrayList Values = new ArrayList();      // list til Values af værdierne vi skriver end

        /// <summary>
        /// for at få forbindelse til databsen fra program
        /// </summary>
        /// <param name="c"></param>
        public Storrelse(SqlConnection c)
        {
            myConn = c;
        }

        /// <summary>
        /// sætter værdier og trækker værdierne ud for storrelse
        /// </summary>
        public string storrelse 
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        /// <summary>
        /// sætter værdier og trækker værdierne ud for GentagStorrelse
        /// </summary>
        public string GentagStorrelse
        {
            get
            {
                return gsize;
            }
            set
            {
                gsize = value;
            }
        }

        /// <summary>
        /// for at fidne ud af hvor man skal sendes hen i CRUD
        /// </summary>
        /// <param name="crud"></param>
        /// <param name="valg"></param>
        public void Save(int crud, int valg)
        {
            CRUD Crud = new CRUD(myConn);   // forbindelse til CRUD

            // tilføj kolonne til Keys
            Keys.Add("Storrelse");
            //Keys.Add("Id");

            // tilføj værdier til Values
            Values.Add(storrelse);

            ArrayList gValues = new ArrayList();    // arraylist til alle de gentagene storrelser til update
            gValues.Add(GentagStorrelse);

            switch (crud)   // for at find eud af hvor sakl sende til i CRUD
            {
                case 1:
                    Crud.Create(Keys, Values, tablename);
                    break;
                case 2:
                    break;
                case 3:
                    Crud.Show(Keys, tablename, "");
                    break;
                case 4:
                    Crud.Delect(Keys, Values, tablename, 1);
                    break;
            }

        }

        /// <summary>
        /// laver en ny størrelse
        /// </summary>
        /*public void Insert()
        {
            query = "INSERT INTO [Storrelse] ([Storrelse]) VALUES (@storrelse)";
            myConn.Open();
            SqlCommand cmd = new SqlCommand(query, myConn);
            cmd.Parameters.AddWithValue("@storrelse", size);
            cmd.ExecuteNonQuery();
            myConn.Close();
        }*/

        /*public void Delect()
        {

        }*/

        /*public void Update()
        {

        }*/

        /// <summary>
        /// viser det der er i størrelse(Storrelse)
        /// </summary>
        /*public void Show()
        {
            query = "SELECT Storrelse FROM [Storrelse]";
            myConn.Open();
            SqlCommand cmd = new SqlCommand(query, myConn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                //Console.WriteLine(string.Format("{0}", reader["Storrelse"]));
                Console.WriteLine(reader.GetString(0));
            }
            myConn.Close();
        }*/
    }
}
