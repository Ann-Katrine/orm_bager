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
        private string total = "";  // skal bruges til en lang sætning

        /// <summary>
        /// for at få database forbindelse fra program
        /// </summary>
        /// <param name="c"></param>
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
        public void Create(List<string> Keys, ArrayList Values, string Tablename)   // Keys = tablens kolonner, Values = værdier vi skrevet ind, Tablename = tabel navn på tablen vi bruger
        {
            keys = String.Join(", ", Keys);     // for lavet alle Keys værdier, til at stå efter hinanden ved hjælp af string.Join
            values = "@" + string.Join(", @", Keys);    // for alle Keys værdier, til at stå efter hinanden men samtidig få et @ foran

            query = "INSERT INTO " + Tablename + "(" + keys + ") VALUES (" + values + ")";     // Bruger Tablename, key, og values for at gøre at der ikke nemmer kommer SQL-injection 
            //Console.WriteLine(query);
            myConn.Open();  // åbener forbindelsen til databasen
            SqlCommand cmd = new SqlCommand(query, myConn);
            for (int i = 0; i < Keys.Count; i++)
            {
                cmd.Parameters.AddWithValue("@" + Keys[i], Values[i]);  // Values erstatter @Keys variabler med den værdier   
                //Console.WriteLine(Values[i]);
            }
            cmd.ExecuteNonQuery();  // den køre cmd'en ???
            myConn.Close(); // lukker forbindelsen til databasen
        }

        /// <summary>
        /// Viser det indhold der er i den tabel man har vælgt 
        /// </summary>
        /// <param name="Keys"></param>
        /// <param name="Tablename"></param>
        public void Show(List<string> Keys, string Tablename, string join) // Keys = tablens kolonner, Tablename = tabel navn på tablen vi bruger, join = hvis man skal bruge join's i en SQL sætning
        {
            keys = String.Join(", ", Keys);     // for lavet alle Keys værdier, til at stå efter hinanden ved hjælp af string.Join
            for (int i = 0; i < Keys.Count; i++)    // for at få alle de værdier vi har i Keys
            {
                values = Tablename + "." + Keys[i] + ",";   // for at sætte tablenavn.kolonnenavn 
                total = total + values;     // for at sætte alle tablenavn.kolonnenavn efter hinanden
            }
            //Console.WriteLine(total);

            if (Tablename == "Postnummer" || Tablename == "Storrelse")  // hvis tabelnavn er Postnummer eller Storrelse så gå ind
            {
                query = "SELECT " + keys + " FROM " + Tablename;
            }
            else    // hvis tabelnavn er noget anden
            {
                //query = "SELECT kunde.Fornavn, Kunde.Fornavn, Kunde.Mobil, Kunde.VejNavn, Postnummer.Postnr, Postnummer.ByNavn FROM Kunde INNER JOIN Postnummer ON Kunde.Postnummer_id = Postnummer.Postnr";
                query = "SELECT " + total + join;
                //Console.WriteLine(query);
            }

            myConn.Open();  // for at åbne forbindelse til database
            SqlCommand cmd = new SqlCommand(query, myConn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ArrayList arrayList = new ArrayList();  // laver en liste til alle værdierne
                for (int i = 0; i < reader.FieldCount; i++)     // for at finde ud af hvilken datatype værdierne har fra SQL
                {
                    string dataType = reader.GetDataTypeName(i);
                    if (dataType == "int")          // hvis værdierne er en int
                    {
                        arrayList.Add(reader.GetInt32(i));
                        //Console.WriteLine(dataType);
                    }
                    else if (dataType == "nvarchar" || dataType == "varchar")   // hvis værdierne er en nvarchar eller varchar
                    {
                        arrayList.Add(reader.GetString(i));
                        //Console.WriteLine(dataType);
                    }
                    else if (dataType == "date" || dataType == "datetime")  // hvis værdierne er en date eller datetime
                    {
                        arrayList.Add(reader.GetDateTime(i));
                        //Console.WriteLine(dataType);
                    }
                }

                switch (Tablename)  // finder den case tabelnavnet før til
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

        public void Update(List<string> Keys, ArrayList Values, ArrayList gValues, string tabelnavn, int tal, string antal) /* ikke begundt på i nu*/
        {
            int count;
            string total1 = "";
            string query1;

            if (antal == "alle")
            {
                count = Keys.Count;
                for (int i = 0; i < count; i++)
                {
                    //total = total + tabelnavn + "." +  Keys[i] + " = " + gValues[i];
                    total1 = total1 + Keys[i] + " = '" + Values[i] + "'";
                    
                    if(i < Keys.Count-1)
                    {
                        //total = total + ",";
                        total1 = total1 + ",";
                    }
                }
                //Console.WriteLine(total);
                //Console.WriteLine(total1);

                switch (tabelnavn)
                {
                    case "Postnummer":
                        query = "UPDATE " + tabelnavn + " SET " + total1 + " WHERE " + tabelnavn + "." + Keys[0] + " = " + gValues[0];
                        break;
                    case "Storrelse":
                        break;
                    case "Kunde":
                        break;
                    case "Kage":
                        break;
                }
                Console.WriteLine(query);

                myConn.Open();
                SqlCommand cmd = new SqlCommand(query, myConn);
                cmd.ExecuteNonQuery();  // den køre cmd'en ???
                myConn.Close();
            }
            else
            {

            }
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
        } 
    }
}
