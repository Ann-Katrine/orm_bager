using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace Orm_Bager
{
    class PostnummerOgBy
    {
        private SqlConnection myConn;
        public List<string> Keys = new List<string>();   // list til alle kolonnenavne
        public ArrayList Values = new ArrayList();       // arraylist til alle værdierne man skrever ind
        private int postnummer;     // værdi til postnummer kolonne
        private int gpostnr;        // værdi til en gentagene postnummer til update
        private string bynavn;      // værdi til bynavn kolonne
        private string gbynavn;     // værdi til en gentagene bynavn til update
        public string Tablename = "Postnummer";     // for at få tablenavn fra databasse som en værdi
        //private string query;   


        /// <summary>
        /// for at få forbindelse til databsen fra program
        /// </summary>
        /// <param name="c"></param>
        public PostnummerOgBy(SqlConnection c)
        {
            myConn = c;
        }

        /// <summary>
        /// sætter værdier og trækker værdierne ud for Postnummer
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
        /// sætter værdier og trækker værdierne ud for GentagPostnr
        /// </summary>
        public int GentagPostnr
        {
            get
            {
                return gpostnr;
            }
            set
            {
                gpostnr = value;
            }
        }

        /// <summary>
        /// sætter værdier og trækker værdierne ud for Bynavn
        /// </summary>
        public string Bynavn
        {
            get
            {
                return bynavn;
            }
            set
            {
                bynavn = value;
            }
        }

        /// <summary>
        /// sætter værdier og trækker værdierne ud for GentagBynavn
        /// </summary>
        public string GentagBynavn
        {
            get
            {
                return gbynavn;
            }
            set
            {
                gbynavn = value;
            }
        }

        /// <summary>
        /// for at finde du af hvor man skal sendes hen i CRUD
        /// </summary>
        /// <param name="crud"></param> for at hjælpe med at finde ud af hvilken switch man skal hen til
        /// <param name="valg"></param> også for at hjælpe med at finde ud af hvilken switch man skal hen til
        public void Save(int crud, int valg)
        {
            CRUD Crud = new CRUD(myConn);   // forbindelse til CRUD
            
            // tilføj kolonner til Keys
            Keys.Add("Postnr");
            Keys.Add("ByNavn");

            // tilføj værdier til Values
            Values.Add(Postnummer);
            Values.Add(Bynavn);

            ArrayList gValues = new ArrayList();    // laver en list til de gentage postnr og bynavn til update
            gValues.Add(GentagPostnr);
            gValues.Add(GentagBynavn);

            switch (crud)   // hvilken vej man skal sendes til CRUD
            {
                case 1:
                    Crud.Create(Keys, Values, Tablename);
                    //Insert(Keys, Values);
                    break;
                case 2:
                    //Update(valg, Keys, Values ,gValues);
                    break;
                case 3:
                    Crud.Show(Keys, Tablename, "");
                    //Show(Keys);
                    break;
                case 4:
                    Crud.Delect(Keys, Values, Tablename, 1);
                    //Delect(Keys, Values);
                    break;
                case 5:

                    break;
            }
        }

        /// <summary>
        /// laver en ny postnummer og by
        /// </summary>
        /*public void Insert(List<string> Keys, ArrayList Values)
        {
            keys = String.Join(", ", Keys);
            values = "@" + string.Join(", @", Keys);

            query = "INSERT INTO " + Tablename + "(" + keys + ") VALUES (" + values + ")";
            
            myConn.Open();
            SqlCommand cmd = new SqlCommand(query, myConn);
            for (int i = 0; i < Keys.Count; i++)
            {
                cmd.Parameters.AddWithValue("@" + Keys[i], Values[i]);
            }
            cmd.ExecuteNonQuery();
            myConn.Close();
        }*/

        /// <summary>
        /// sletter det postnummer og by du har valgt ud fra postnummer
        /// </summary>
        /*public void Delect(List<string> Keys, ArrayList Values)
        {
            //query = "DELETE FROM [Postnummer] WHERE Postnummer.Postnr = " + Postnummer;

            query = "DELETE FROM " + Tablename + " WHERE " + Tablename + "." + Keys[0] + " = " + Values[0];
            Console.WriteLine(query);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(query, myConn);
            cmd.ExecuteNonQuery();
            myConn.Close();
        }*/

        /// <summary>
        /// Updater et eller flere felter i Postnummer
        /// </summary>
        /// <param name="valg"></param>
        /*public void Update(int valg, List<string> Keys, ArrayList Values, ArrayList gValues)
        {
            myConn.Open();
            switch (valg)
            {
                case 1:
                    //query = "UPDATE [Postnummer] SET Postnr = " + Postnummer + " WHERE Postnummer.Postnr = " + GentagPostnr;
                    query = "UPDATE " + Tablename + " SET " + Keys[0] + " = " + Values[0] + " WHERE " + Tablename + "." + Keys[0] + " = " + gValues[0];
                    break;
                case 2:
                    //query = "UPDATE [Postnummer] SET ByNavn = '" + GentagBynavn + "' WHERE Postnummer.Postnr = " + GentagPostnr;
                    query = "UPDATE " + Tablename + " SET " + Keys[1] + " = " + Values[1] + " WHERE " + Tablename + "." + Keys[0] + " = " + gValues[0];
                    break;
                case 3:
                    //query = "UPDATE [Postnummer] SET Postnr = " + Postnummer + ", ByNavn = '" + GentagBynavn + "' WHERE Postnummer.Postnr = " + GentagPostnr;
                    query = "UPDATE " + Tablename + " SET " + Keys[0] + " = " + Values[0] + ", " + Keys[1] + " = " + Values[1] + " WHERE " + Tablename + "." + Keys[0] + " = " + gValues[0];
                    //Console.WriteLine(query);
                    break;
                default:
                    Console.WriteLine("Du har skrevet noget der ikke findes på databasen");
                    break;
            }
            SqlCommand cmd = new SqlCommand(query, myConn);
            //cmd.ExecuteNonQuery();
            myConn.Close();
        }*/

        /// <summary>
        /// Viser alle postnummerne 
        /// </summary>
        /*public void Show(List<string> Keys)
        {
            //query = "SELECT Postnr, ByNavn FROM [Postnummer]";
            keys = String.Join(", ", Keys);

            query = "SELECT " + keys + " FROM " + Tablename;

            myConn.Open();
            SqlCommand cmd = new SqlCommand(query, myConn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                //Console.WriteLine(reader.GetDataTypeName(0).ToString());
                //Console.WriteLine(reader.GetDataTypeName(1).ToString());
                ArrayList arrayList = new ArrayList();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string dataType = reader.GetDataTypeName(i);
                    if (dataType == "int")
                    {
                        arrayList.Add(reader.GetInt32(i));
                    }
                    else if (dataType == "nvarchar" || dataType == "varchar")
                    {
                        arrayList.Add(reader.GetString(i));
                    }
                    else if (dataType == "date" || dataType == "datetime")
                    {
                        arrayList.Add(reader.GetDateTime(i));
                    }
                }
           //     Console.WriteLine(string.Format("{0} {1}", reader.GetInt32(0), reader.GetString(1)));
                 Console.WriteLine(string.Format("{0} {1}",arrayList[0], arrayList[1]));
            }
            
            myConn.Close();
        } */


    }
}
