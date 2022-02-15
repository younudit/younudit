using Microsoft.AspNetCore.Mvc;
using NorthShore.Utilities;
using Microsoft.Extensions.Options;
using System.Net.Mime;
using System;
using NorthShore.Models;
using System.IO;

namespace NorthShore.Controllers
{
    [Route("record-drawings")]
    [ApiController]
    public class RecordDrawingsSearchController : ControllerBase
    {
        private readonly ConnectionStringOptions _connection_strings;
        private readonly StoredProcedureNameOptions _stored_procedures;
        private readonly FileLocationOptions _file_location_options;

        public RecordDrawingsSearchController(
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
        public object RecordDrawingsSearch(RecordDrawingsSearchRequest request)
        {
            StoredProcedure sp = new StoredProcedure(
                _connection_strings.ScannedImages,
                _stored_procedures.RecordDrawings
            );

            return sp.Run(request.BuildParameters());
        }

        [HttpPost]
        [Route("file")]
        public FileResult RecordDrawingFile(RecordDrawingsFileRequest request)
        {
            FileResult fr = null;

            try
            {
                var fileName = $"{request.FileName}";
                var filepath = $"{request.getFullPath(_file_location_options.RecordDrawings)}";
                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                fr = File(fileBytes, MediaTypeNames.Application.Octet, fileName);

            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an issue: '{e}'");
            }

            return fr;
        }

        [HttpGet, Route("get-file/{file=file}")]
        public FileResult RecordDrawingFileGet(String file)
        {
            FileResult fr = null;

            try
            {
                var filepath = Path.Combine(_file_location_options.RecordDrawings, file);
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
