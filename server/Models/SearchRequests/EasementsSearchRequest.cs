using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json.Serialization;

namespace NorthShore.Models
{    public class EasementsSearchRequest
    {
        [JsonPropertyName("recording_number_op")] // @RecordingNumberCompare - int
        public int? RecordingNumberOp { get; set; }

        [JsonPropertyName("recording_number")] // @RecordingNumber - varchar(50)
        public String RecordingNumber { get; set; }

        [JsonPropertyName("reference_op")] // @RecordDrawingReferenceCompare - int
        public int? ReferenceOp { get; set; }

        [JsonPropertyName("reference")] // @RecordDrawingReference - varchar(50)
        public String? Reference { get; set; }

        [JsonPropertyName("project_op")] // @ProjectNameCompare - int
        public int? ProjectOp { get; set; }

        [JsonPropertyName("project")] // @ProjectName - varchar(50)
        public String? Project { get; set; }

        [JsonPropertyName("base_map")] // @BaseMap - varchar(5)
        public String? BaseMap { get; set; }

        [JsonPropertyName("water")] // @Water - int
        public bool? Water { get; set; }

        [JsonPropertyName("sewer")] // @Sewer - int
        public bool? Sewer { get; set; }

        [JsonPropertyName("encroachment")] // @Encroachment - int
        public bool? Encroachment { get; set; }

        public List<SqlParameter> BuildParameters()
        {
            List<SqlParameter> sp = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@RecordingNumberCompare", SqlDbType = SqlDbType.Int, Value = RecordingNumberOp ?? null},
                new SqlParameter() {ParameterName = "@RecordingNumber", SqlDbType = SqlDbType.VarChar, Value = RecordingNumber ?? null},
                new SqlParameter() {ParameterName = "@ProjectNameCompare", SqlDbType = SqlDbType.Int, Value = ProjectOp ?? null},
                new SqlParameter() {ParameterName = "@ProjectName", SqlDbType = SqlDbType.VarChar, Value = Project ?? null},
                new SqlParameter() {ParameterName = "@RecordDrawingReferenceCompare", SqlDbType = SqlDbType.Int, Value = ReferenceOp ?? null},
                new SqlParameter() {ParameterName = "@RecordDrawingReference", SqlDbType = SqlDbType.VarChar, Value = Reference ?? null},
                new SqlParameter() {ParameterName = "@BaseMap", SqlDbType = SqlDbType.VarChar, Value = BaseMap ?? null},
                new SqlParameter() {ParameterName = "@Water", SqlDbType = SqlDbType.Int, Value = Water ?? null},
                new SqlParameter() {ParameterName = "@Sewer", SqlDbType = SqlDbType.Int, Value = Sewer ?? null},
                new SqlParameter() {ParameterName = "@Encroachment", SqlDbType = SqlDbType.Int, Value = Encroachment ?? null},
            };

            return sp;
        }

    }
}
