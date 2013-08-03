using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace WPFMonitor.DAL.GetData
{
    public class GetData
    {
        public DataTable GetDataSetData(string ConnStr, string SQL)
        {
            if (string.IsNullOrEmpty(ConnStr))
                return null;
            if (string.IsNullOrEmpty(SQL))
                return null;

            try
            {
                DataSet ds = GetDataSet(ConnStr, SQL);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataSet GetDataSet(string connectionString, string SQL)
        {
            DataSet ds;
            SqlConnection Connection = new SqlConnection(connectionString);
            try
            {
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(SQL);
                adapter.SelectCommand.Connection = Connection;

                ds = new DataSet();
                adapter.Fill(ds);

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                Connection.Close();
            }
            return ds;
        }
    }
}