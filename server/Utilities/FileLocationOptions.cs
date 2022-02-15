using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthShore.Utilities
{
    public class FileLocationOptions
    {
        public const string FileLocations = "FileLocations";

        public string Easements { get; set; }
        public string ApVouchers { get; set; }
        public string EngineeringCounterLogs { get; set; }
        public string MeterApplications { get; set; }
        public string RecordDrawings { get; set; }
        public string SideSewerPermits { get; set; }
        public string BaseMap { get; set; }
        public string SewerBuildout { get; set; }
        public string Video { get; set; }
    }
}
