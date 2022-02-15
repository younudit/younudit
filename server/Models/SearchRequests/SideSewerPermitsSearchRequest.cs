using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace NorthShore.Models
{
    public class SideSewerPermitsSearchRequest
    {
        [JsonPropertyName("starting_account_number")]//@AcctNbr1 - int
        public int? StartingAccountNumber { get; set; }

        [JsonPropertyName("ending_account_number")]//@AcctNbr2 - int
        public int? EndingAccountNumber { get; set; }

        [JsonPropertyName("starting_permit_number")]//@PermitNbr1 - int
        public int? StartingPermitNumber { get; set; }

        [JsonPropertyName("ending_permit_number")]//@PermitNbr2 - int
        public int? EndingPermitNumber { get; set; }

        [JsonPropertyName("base_map")] // @BaseMap - varchar(5)
        public String? BaseMap { get; set; }

        [JsonPropertyName("site_address")]//@SiteAddr - varchar(255)
        public String? SiteAddress { get; set; }

        [JsonPropertyName("site_address_op")]//@SiteAddrCompare - int
        public int? SiteAddressOp { get; set; }

        [JsonPropertyName("parcel")]//@Parcel - varchar(255)
        public String? Parcel { get; set; }

        [JsonPropertyName("parcel_op")]//@ParcelCompare - int
        public int? ParcelOp { get; set; }


        public List<SqlParameter> BuildParameters()
        {
            List<SqlParameter> sp = new()
            {
                new SqlParameter() { ParameterName = "@PermitNbr1", SqlDbType = SqlDbType.Int, Value = StartingPermitNumber ?? null },
                new SqlParameter() { ParameterName = "@PermitNbr2", SqlDbType = SqlDbType.Int, Value = EndingPermitNumber ?? null },
                new SqlParameter() { ParameterName = "@AcctNbr1", SqlDbType = SqlDbType.Int, Value = StartingAccountNumber ?? null },
                new SqlParameter() { ParameterName = "@AcctNbr2", SqlDbType = SqlDbType.Int, Value = EndingAccountNumber ?? null },
                new SqlParameter() { ParameterName = "@SiteAddrCompare", SqlDbType = SqlDbType.Int, Value = SiteAddressOp ?? null },
                new SqlParameter() { ParameterName = "@SiteAddr", SqlDbType = SqlDbType.VarChar, Value = SiteAddress ?? null },
                new SqlParameter() { ParameterName = "@BaseMap", SqlDbType = SqlDbType.VarChar, Value = BaseMap ?? null },
                new SqlParameter() { ParameterName = "@ParcelCompare", SqlDbType = SqlDbType.Int, Value = ParcelOp ?? null },
                new SqlParameter() { ParameterName = "@Parcel", SqlDbType = SqlDbType.VarChar, Value = Parcel ?? null },
            };

            return sp;
        }


    }
}