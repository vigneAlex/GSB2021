using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSBFraisModel.Data
{
    public class Dbal
    {
        private MySqlConnection connection;

        public Dbal(string database = "gsb_frais", string uid = "root", string password = "root", string server = "localhost")
        {
            Initialize(database, uid, password, server);
        }


        private void Initialize(string database, string uid, string password, string server)
        {
            string connectionString;
            connectionString = "SERVER= " + server + " ;" + " DATABASE= " + database + " ;" + " UID=" + uid + " ;" + " PASSWORD= " + password + " ;";
            connection = new MySqlConnection(connectionString);
        }



        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }



        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }



        //CURQuery: Create, Update, Delete query execution method
        private void CUDQuery(string query)
        {
            if (OpenConnection())
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }


        //Insert statement
        public void Insert(string query)
        {
            query = "INSERT INTO " + query; // tablename (field1, field2) VALUES('value 1', 'value 2')";
            CUDQuery(query);
        }

        

        public DataRow SelectByComposedPk2(string table, string keyname1, string keyValue1, string keyname2, string keyValue2)
        {
            string query = "SELECT * FROM " + table + " where " + keyname1 + " = '" + keyValue1 + "'AND " + keyname2 + " = '" + keyValue1 + "'";
            DataSet dataset = RQuery(query);
            return dataset.Tables[0].Rows[0];
        }
        public DataTable SelectByComposedFk2(string table, string keyname1, string keyValue1, string keyname2, string keyValue2)
        {
            string query = "SELECT * FROM " + table + " where " + keyname1 + " = '" + keyValue1 + "'AND " + keyname2 + " = '" + keyValue2 + "'";
            DataSet dataset = RQuery(query);
            return dataset.Tables[0];
        }

        //Update statement
        public void Update(string query)
        {
            query = "UPDATE " + query;
            CUDQuery(query);
        }

        //Delete statement
        public void Delete(string query)
        {
            query = "DELETE FROM " + query;

            CUDQuery(query);
        }


        //RQuery: Read query method (to execute SELECT queries)
        private DataSet RQuery(string query)
        {
            DataSet dataset = new DataSet();
            //Open connection
            if (OpenConnection())
            {
                //Add query data in a DataSet
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                adapter.Fill(dataset);
                CloseConnection();
            }
            return dataset;
        }



        public DataTable SelectAll(string table)
        {
            string query = "SELECT * FROM " + table;
            DataSet dataset = RQuery(query);
            return dataset.Tables[0];
        }

        public DataTable Select(string query)
        {
            query = "SELECT " + query;
            DataSet dataset = RQuery(query);
            return dataset.Tables[0];
        }

        public DataRow SelectById(string table, string id)
        {
            string query = "SELECT * FROM " + table + " where id='" + id + "'";
            DataSet dataset = RQuery(query);
            return dataset.Tables[0].Rows[0];
        }

        public DataRow SelectById2(string table, string id, string mois)
        {
            string query = "SELECT * FROM " + table + " WHERE idVisiteur = '" + id + "' AND mois = '" + mois + "'";
            DataSet dataset = RQuery(query);
            return dataset.Tables[0].Rows[0];
        }



        public DataTable SelectByField(string table, string fieldTestCondition)
        {
            string query = "SELECT * FROM " + table + " where " + fieldTestCondition;
            DataSet dataset = RQuery(query);
            return dataset.Tables[0];
        }
    }
}
