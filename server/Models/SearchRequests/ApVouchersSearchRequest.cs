using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace NorthShore.Models
{
    public class ApVouchersSearchRequest
    {
        [JsonPropertyName("check_number_start")] // @CheckNum1
        public String? CheckNumberStart { get; set; }

        [JsonPropertyName("check_number_end")] // @CheckNum2
        public String? CheckNumberEnd { get; set; }

        [JsonPropertyName("vendor")] // @Vendor
        public String? Vendor { get; set; }

        [JsonPropertyName("vendor_op")] // @VendorCompare
        public int? VendorOp { get; set; }

        [JsonPropertyName("invoice_date_start")] // @InvoiceDate1
        [DataType(DataType.Date)]
        public DateTime? InvoiceDateStart { get; set; }

        [JsonPropertyName("invoice_date_end")] // @InvoiceDate2
        [DataType(DataType.Date)]
        public DateTime? InvoiceDateEnd { get; set; }

        [DataType(DataType.Date)]
        [JsonPropertyName("payment_date_start")]  // @PaymentDate1
        public DateTime? PaymentDateStart { get; set; }

        [JsonPropertyName("payment_date_end")]  // @paymentDate2
        [DataType(DataType.Date)]
        public DateTime? PaymentDateEnd { get; set; }

        [JsonPropertyName("payment_amount_start")] // @paymentAmount1
        [DataType(DataType.Date)]
        public decimal? PaymentAmountStart { get; set; }

        [JsonPropertyName("payment_amount_end")] // @paymentAmount2
        [DataType(DataType.Date)]
        public decimal? PaymentAmountEnd { get; set; }


        public List<SqlParameter> BuildParameters()
        {
            List<SqlParameter> sp = new()
            {
                new SqlParameter() { ParameterName = "@CheckNum1", SqlDbType = SqlDbType.Int, Value = CheckNumberStart ?? null },
                new SqlParameter() { ParameterName = "@CheckNum2", SqlDbType = SqlDbType.Int, Value = CheckNumberEnd ?? null },
                new SqlParameter() { ParameterName = "@VendorCompare", SqlDbType = SqlDbType.Int, Value = VendorOp ?? null },
                new SqlParameter() { ParameterName = "@Vendor", SqlDbType = SqlDbType.VarChar, Value = Vendor ?? null },
                new SqlParameter() { ParameterName = "@InvoiceDate1", SqlDbType = SqlDbType.DateTime, Value = InvoiceDateStart ?? null },
                new SqlParameter() { ParameterName = "@InvoiceDate2", SqlDbType = SqlDbType.DateTime, Value = InvoiceDateEnd ?? null },
                new SqlParameter() { ParameterName = "@PaymentDate1", SqlDbType = SqlDbType.DateTime, Value = PaymentDateStart ?? null },
                new SqlParameter() { ParameterName = "@paymentDate2", SqlDbType = SqlDbType.DateTime, Value = PaymentDateEnd ?? null },
                new SqlParameter() { ParameterName = "@paymentAmount1", SqlDbType = SqlDbType.Money, Value = PaymentAmountStart ?? null },
                new SqlParameter() { ParameterName = "@paymentAmount2", SqlDbType = SqlDbType.Money, Value = PaymentAmountEnd ?? null },
            };

            return sp;
        }


    }
}