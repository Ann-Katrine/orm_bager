using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace Orm_Bager
{
    class Storrelse
    {
        private string size;
        private string gsize;
        private string query;
        public string tablename = "Storrelse";
        private SqlConnection myConn;
        public List<string> Keys = new List<string>();
        public ArrayList Values = new ArrayList();

        public Storrelse(SqlConnection c)
        {
            myConn = c;
        }

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
        /// træet med grenene ud til hvor man har bestem man skal i CRUD
        /// </summary>
        /// <param name="crud"></param>
        /// <param name="valg"></param>
        public void Save(int crud, int valg)
        {
            CRUD Crud = new CRUD(myConn);

            Keys.Add("Storrelse");
            Keys.Add("Id");

            Values.Add(storrelse);

            ArrayList gValues = new ArrayList();
            gValues.Add(GentagStorrelse);

            switch (crud)
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

        public void Update()
        {

        }

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
