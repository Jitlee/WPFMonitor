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
        public DataSetData GetDataSetData(string ConnStr, string SQL, out CustomException ServiceError)
        {
            try
            {
                DataSet ds = GetDataSet(ConnStr, SQL);
                ServiceError = null;
                return DataSetData.FromDataSet(ds);
            }
            catch (Exception ex)
            {
                ServiceError = new CustomException(ex);
            }
            return null;
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