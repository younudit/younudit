using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace NorthShore.Models
{
    public class MeterApplicationsSearchRequest
    {
        [JsonPropertyName("account_number")] //@ARUsrAcctNbr - int
        public int? AccountNumber { get; set; }

        [JsonPropertyName("meter_number")] //@MtrNbr - varchar(50)
        public String? MeterNumber { get; set; }

        [JsonPropertyName("site_address")] //@SiteAddr - varchar(50)
        public String? SiteAddress { get; set; }

        [JsonPropertyName("site_address_op")] //@SiteAddrCompare - int
        public int? SiteAddressOp { get; set; }

        [JsonPropertyName("owner")] //@OwnerName - varchar(50)
        public String? Owner { get; set; }

        [JsonPropertyName("owner_op")] //@OwnerNameCompare - int
        public int? OwnerOp { get; set; }

        public List<SqlParameter> BuildParameters()
        {
            List<SqlParameter> sp = new()
            {
                new SqlParameter() { ParameterName = "@ARUsrAcctNbr", SqlDbType = SqlDbType.Int, Value = AccountNumber ?? null },
                new SqlParameter() { ParameterName = "@MtrNbr", SqlDbType = SqlDbType.VarChar, Value = MeterNumber ?? null },
                new SqlParameter() { ParameterName = "@SiteAddrCompare", SqlDbType = SqlDbType.Int, Value = SiteAddressOp ?? null },
                new SqlParameter() { ParameterName = "@SiteAddr", SqlDbType = SqlDbType.VarChar, Value = SiteAddress ?? null },
                new SqlParameter() { ParameterName = "@OwnerNameCompare", SqlDbType = SqlDbType.Int, Value = OwnerOp ?? null },
                new SqlParameter() { ParameterName = "@OwnerName", SqlDbType = SqlDbType.VarChar, Value = Owner ?? null }
            };

            return sp;
        }


    }
}