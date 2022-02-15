using Microsoft.AspNetCore.Mvc;
using NorthShore.Utilities;
using Microsoft.Extensions.Options;
using System.Net.Mime;
using System;
using System.IO;
using NorthShore.Models;

namespace NorthShore.Controllers
{
    [Route("engineering-counter-logs")]
    [ApiController]
    public class EngineeringCounterLogsSearchController : ControllerBase
    {
        private readonly ConnectionStringOptions _connection_strings;
        private readonly StoredProcedureNameOptions _stored_procedures;
        private readonly FileLocationOptions _file_location_options;

        public EngineeringCounterLogsSearchController(
            IOptions<ConnectionStringOptions> connectionStrings,
            IOptions<StoredProcedureNameOptions> storedProcedures,
            IOptions<FileLocationOptions> fileLocationOptions
            )
        {
            _connection_strings = connectionStrings.Value;
            _stored_procedures = storedProcedures.Value;
            _file_location_options = fileLocationOptions.Value;
        }

        [HttpPost]
        [Route("search")]
        public object EngineeringCounterLogsSearch(EngineeringCounterLogsSearchRequest request)
        {
            StoredProcedure sp = new StoredProcedure(
                _connection_strings.ScannedImages,
                _stored_procedures.EngineeringCounterLogs
            );

            return sp.Run(request.BuildParameters());
        }

        [HttpPost]
        [Route("file")]
        public FileResult EngineeringCounterLogFile(EngineeringCounterLogsFileRequest request)
        {
            FileResult fr = null;

            try
            {
                var fileName = $"{request.FileName}";
                var filepath = $"{request.getFullPath(_file_location_options.EngineeringCounterLogs)}";
                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                fr = File(fileBytes, MediaTypeNames.Application.Octet, fileName);

            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an issue: '{e}'");
            }

            return fr;
        }

        [HttpGet, Route("get-file/")]
        public FileResult EngineeringCounterLogFileGet(String path, String file)
        {
            FileResult fr = null;

            try
            {
                var filepath = Path.Combine(_file_location_options.EngineeringCounterLogs, path, file);
                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                fr = File(fileBytes, MediaTypeNames.Application.Octet, file);
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an issue: '{e}'");
            }

            Response.ContentType = "application/pdf";

            return fr;
        }
    }
}
