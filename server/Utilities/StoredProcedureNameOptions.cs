using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthShore.Utilities
{
    public class StoredProcedureNameOptions
    {
        public const string StoredProcedureNames = "StoredProcedureNames";

        public string Easements { get; set; }
        public string ApVouchers { get; set; }
        public string EngineeringCounterLogs { get; set; }
        public string MeterApplications { get; set; }
        public string RecordDrawings { get; set; }
        public string SideSewerPermits { get; set; }
    }
}
