using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace Orm_Bager
{
    class CRUD
    {
        private SqlConnection myConn;
        private string values;
        private string query;
        private string keys;

        public CRUD(SqlConnection c)
        {
            myConn = c;
        }

        /// <summary>
/// laver en ny værdi i en af tablene
/// </summary>
/// <param name="Keys"></param>
/// <param name="Values"></param>
/// <param name="Tablename"></param> 
        public void Create(List<string> Keys, ArrayList Values, string Tablename)
        {
            keys = String.Join(", ", Keys);
            values = "@" + string.Join(", @", Keys);

            query = "INSERT INTO " + Tablename + "(" + keys + ") VALUES (" + values + ")";
            Console.WriteLine(query);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(query, myConn);
            for (int i = 0; i < Keys.Count; i++)
            {
                cmd.Parameters.AddWithValue("@" + Keys[i], Values[i]);
                Console.WriteLine(Values[i]);
            }
            cmd.ExecuteNonQuery();
            myConn.Close();
        }

        /// <summary>
        /// Viser det indhold der er i den tabel man har vælgt 
        /// </summary>
        /// <param name="Keys"></param>
        /// <param name="Tablename"></param>
        public void Show(List<string> Keys, string Tablename, string join)
        {
            string total = "";

            keys = String.Join(", ", Keys);
            for (int i = 0; i < Keys.Count; i++)
            {
                values = Tablename + "." + Keys[i] + ",";
                total = total + values;
            }
            //Console.WriteLine(total);

            if (Tablename == "Postnummer" || Tablename == "Storrelse")

            {
                query = "SELECT " + keys + " FROM " + Tablename;
            }
            else
            {
                //query = "SELECT kunde.Fornavn, Kunde.Fornavn, Kunde.Mobil, Kunde.VejNavn, Postnummer.Postnr, Postnummer.ByNavn FROM Kunde INNER JOIN Postnummer ON Kunde.Postnummer_id = Postnummer.Postnr";
                query = "SELECT " + total + join;
                //Console.WriteLine(query);
            }

            myConn.Open();
            SqlCommand cmd = new SqlCommand(query, myConn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ArrayList arrayList = new ArrayList();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string dataType = reader.GetDataTypeName(i);
                    if (dataType == "int")
                    {
                        arrayList.Add(reader.GetInt32(i));
                        //Console.WriteLine(dataType);
                    }
                    else if (dataType == "nvarchar" || dataType == "varchar")
                    {
                        arrayList.Add(reader.GetString(i));
                        //Console.WriteLine(dataType);
                    }
                    else if (dataType == "date" || dataType == "datetime")
                    {
                        arrayList.Add(reader.GetDateTime(i));
                        //Console.WriteLine(dataType);
                    }
                }
                switch (Tablename)
                {
                    case "Postnummer":
                        Console.WriteLine(string.Format("{0} {1}", arrayList[0], arrayList[1]));
                        break;
                    case "Storrelse":
                        Console.WriteLine(string.Format("{0}", arrayList[0]));
                        break;
                    case "Kunde":
                        Console.WriteLine(string.Format("{0} {1}: mobilnr. {2}, bor på {3} ved {4} {5}", arrayList[0], arrayList[1], arrayList[2], arrayList[3], arrayList[4], arrayList[5]));
                        break;
                    case "Kage":
                        Console.WriteLine(string.Format("Kagen: {0}, koster {1} kr., størrelsen er {2}", arrayList[0], arrayList[1], arrayList[3]));
                        break;
                }
            }
            
            myConn.Close();
            
        }

        public void Update() /* ikke begundt på i nu*/
        {

        }

        /// <summary>
        /// sletter en række i den table man har valg der er i databasen
        /// </summary>
        /// <param name="Keys"></param>
        /// <param name="Values"></param>
        /// <param name="Tablename"></param>
        public void Delect(List<string> Keys, ArrayList Values, string Tablename, int antalvalues) // ikke færdig i nu
        {
            string total = "";

            if ( antalvalues > 1)
            {
                for (int i = 0; i < Keys.Count; i++)
                {
                    total = total + Tablename + "." + Keys[i] + " = " + Values[i];
                    if (i == 0)
                    {
                        total = total + " & ";
                    }
                }

                query = "DELETE Id FROM " + Tablename + " WHERE " + total;
                Console.WriteLine(query);
                //query = "DELETE Id FROM " + Tablename + " WHERE " + Tablename + "." + Keys + " = @Values";
            }
            else
            {
                query = "DELETE FROM " + Tablename + " WHERE " + Tablename + "." + Keys + " = @Values"; 
            }
           
            Console.WriteLine(query);
            myConn.Open();
            SqlCommand cmd = new SqlCommand(query, myConn);
            cmd.Parameters.AddWithValue("@Values", Values);
            cmd.ExecuteNonQuery();
            myConn.Close();
        } // ikke færdig i nu
    }
}
