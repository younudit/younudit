using Microsoft.AspNetCore.Mvc;
using NorthShore.Utilities;
using Microsoft.Extensions.Options;
using System.Net.Mime;
using System;
using NorthShore.Models;
using System.IO;

namespace NorthShore.Controllers
{
    [Route("sewer-buildout")]
    [ApiController]
    public class SewerBuildoutSearchController : ControllerBase
    {
        private readonly FileLocationOptions _file_location_options;

        public SewerBuildoutSearchController(
            IOptions<FileLocationOptions> fileLocationOptions
            )
        {
            _file_location_options = fileLocationOptions.Value;
        }

        [HttpPost]
        [Route("file")]
        public FileResult RecordDrawingFile(SewerBuildoutFileRequest request)
        {
            FileResult fr = null;

            try
            {
                var fileName = $"{request.FileName}";
                var filepath = $"{request.getFullPath(_file_location_options.SewerBuildout)}";
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
                var filepath = Path.Combine(_file_location_options.SewerBuildout, file);
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
