using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json.Serialization;

namespace NorthShore.Models
{
    public class RecordDrawingsSearchRequest
    {
        [JsonPropertyName("type")] //@Type - varchar(1)
        public String? Type { get; set; }

        [JsonPropertyName("base_map")]//@MapNumber - varchar(5)
        public String? BaseMap { get; set; }

        [JsonPropertyName("project")]//@ProjectName - varchar(255)
        public String? Project { get; set; }

        [JsonPropertyName("project_op")]//@ProjectNameCompare - int
        public int? ProjectOp { get; set; }

        public List<SqlParameter> BuildParameters()
        {
            List<SqlParameter> sp = new()
            {
                new SqlParameter() { ParameterName = "@ProjectNameCompare", SqlDbType = SqlDbType.Int, Value = ProjectOp ?? null },
                new SqlParameter() { ParameterName = "@ProjectName", SqlDbType = SqlDbType.VarChar, Value = Project ?? null },
                new SqlParameter() { ParameterName = "@MapNumber", SqlDbType = SqlDbType.VarChar, Value = BaseMap ?? null },
                new SqlParameter() { ParameterName = "@Type", SqlDbType = SqlDbType.VarChar, Value = Type ?? null },
            };

            return sp;
        }


    }
}