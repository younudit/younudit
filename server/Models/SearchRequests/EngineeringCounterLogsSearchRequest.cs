using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace NorthShore.Models
{
    public class EngineeringCounterLogsSearchRequest
    {

        [JsonPropertyName("starting_log_number")] //@LogNbr1 - decimal(18, 2)
        public decimal? StartingLogNumber { get; set; }

        [JsonPropertyName("ending_log_number")] //@LogNbr2 - decimal(18, 2)
        public decimal? EndingLogNumber { get; set; }

        [JsonPropertyName("base_map")] //@BaseMap - varchar(4)
        public String? BaseMap { get; set; }

        [JsonPropertyName("start_date")] //@FirstDate1 - datetime
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName("end_date")] //@FirstDate2 - datetime
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [JsonPropertyName("status")] //@Status - varchar(20)
        public String? Status { get; set; }

        public List<SqlParameter> BuildParameters()
        {
            List<SqlParameter> sp = new()
            {
                new SqlParameter() { ParameterName = "@LogNbr1", SqlDbType = SqlDbType.Decimal, Value = StartingLogNumber ?? null },
                new SqlParameter() { ParameterName = "@LogNbr2", SqlDbType = SqlDbType.Decimal, Value = EndingLogNumber ?? null },
                new SqlParameter() { ParameterName = "@BaseMap", SqlDbType = SqlDbType.VarChar, Value = BaseMap ?? null },
                new SqlParameter() { ParameterName = "@FirstDate1", SqlDbType = SqlDbType.DateTime, Value = StartDate ?? null },
                new SqlParameter() { ParameterName = "@FirstDate2", SqlDbType = SqlDbType.DateTime, Value = EndDate ?? null },
                new SqlParameter() { ParameterName = "@Status", SqlDbType = SqlDbType.VarChar, Value = Status ?? null },
            };

            return sp;
        }


    }
}