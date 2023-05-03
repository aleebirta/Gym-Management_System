using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scss_gymmanagement
{
    
    internal class Functions
    {
        private SqlConnection Con;
        private SqlCommand Cmd;
        private DataTable dt;
        private string ConStr;
        private SqlDataAdapter sda;

        public Functions() 
        {
            ConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Alexandra\OneDrive\Desktop\facultate\An 2\semestrul 2\Limbaje de programare II\proiect\FitnessDb.mdf;Integrated Security=True;Connect Timeout=30";
            Con = new SqlConnection(ConStr);
            Cmd = new SqlCommand();
            Cmd.Connection = Con;
        }
        public SqlConnection GetConnection()
        {
            return Con;
        }

        public void OpenConnection()
        {
            if (Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }
        }

        public void CloseConnection()
        {
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
            }
        }

        public int setData(string Query)
        {
            int cnt = 0;
            if (Con.State == ConnectionState.Closed) 
            {
                Con.Open();
            }
            Cmd.CommandText = Query;
            cnt = Cmd.ExecuteNonQuery();
            Con.Close();
            return cnt;
        }
        public DataTable GetData(string Query) 
        {
            dt = new DataTable();
            sda = new SqlDataAdapter(Query, ConStr);
            sda.Fill(dt);
            return dt;
        }
    }
}
