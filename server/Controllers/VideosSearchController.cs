using Microsoft.AspNetCore.Mvc;
using NorthShore.Models;
using NorthShore.Utilities;
using Microsoft.Extensions.Options;
using System.Net.Mime;
using System;
using System.IO;

namespace NorthShore.Controllers
{
    [Route("videos")]
    [ApiController]
    public class VideosSearchController : ControllerBase
    {
        private readonly FileLocationOptions _file_location_options;

        public VideosSearchController(
            IOptions<FileLocationOptions> fileLocationOptions
            )
        {
            _file_location_options = fileLocationOptions.Value;
        }

        [HttpPost]
        [Route("file")]
         public FileResult VideoFile(VideosFileRequest request)
        {
            FileResult fr = null;

            try
            {
                var fileName = $"{request.FileName}";
                var filepath = $"{request.getFullPath(_file_location_options.Video)}";
                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                fr = File(fileBytes, MediaTypeNames.Application.Octet, fileName);

            } catch(Exception e){
                Console.WriteLine($"There was an issue: '{e}'");
            }

            return fr;
        }

        [HttpGet, Route("get-file/")]
        public FileResult VideoFileGet(string? path, String file)
        {
            FileResult fr = null;

            try
            {
                var filepath = (path == null || path == "") ?
                    Path.Combine(_file_location_options.Video, file)
                    : Path.Combine(_file_location_options.Video, path, file);
                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                fr = File(fileBytes, MediaTypeNames.Application.Octet, file);
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an issue: '{e}'");
            }

            return fr;
        }
    }
}
