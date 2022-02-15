using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace NorthShore.Utilities
{
    public class StoredProcedure
    {        
        public string ConnectionString { get; set; }
        public string StoredProcedureName { get; set; }

        public StoredProcedure(string connection_string, string proc_name)
        {
            ConnectionString = connection_string;
            StoredProcedureName = proc_name;
        }

        public string Run(List<SqlParameter> parameters)
        {
            return DataSetToList(ExecuteProcedure(parameters));
        }

        private DataSet ExecuteProcedure(List<SqlParameter> parameters)
        {
            try
            {
                DataSet result = null;
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (var command = sqlConnection.CreateCommand())
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(command))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = StoredProcedureName;
                            if (parameters != null)
                            {
                                command.Parameters.AddRange(parameters.ToArray());
                            }
                            result = new DataSet();
                            sda.Fill(result);
                        }
                    }
                }
                return result;
            } 
            catch (Exception exception) {
                throw exception;
            }
        }

        public static string DataSetToList(DataSet resultDataSet)
        {
            string jsonString = string.Empty;

            jsonString = JsonConvert.SerializeObject(resultDataSet.Tables[0]);

            return jsonString;
        }
    }
}
