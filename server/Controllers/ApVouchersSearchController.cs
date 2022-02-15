using Microsoft.AspNetCore.Mvc;
using NorthShore.Utilities;
using Microsoft.Extensions.Options;
using System.Net.Mime;
using System;
using System.IO;
using NorthShore.Models;

namespace NorthShore.Controllers
{
    [Route("ap-vouchers")]
    [ApiController]
    public class ApVouchersSearchController : ControllerBase
    {
        private readonly ConnectionStringOptions _connection_strings;
        private readonly StoredProcedureNameOptions _stored_procedures;
        private readonly FileLocationOptions _file_location_options;

        public ApVouchersSearchController(
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
        public object ApVouchersSearch(ApVouchersSearchRequest request)
        {
            StoredProcedure sp = new StoredProcedure(
                _connection_strings.ScannedImages,
                _stored_procedures.ApVouchers
            );

            return sp.Run(request.BuildParameters());
        }

        [HttpPost]
        [Route("file")]
        public FileResult ApVoucherFile(ApVouchersFileRequest request)
        {
            FileResult fr = null;

            try
            {
                var fileName = $"{request.FileName}";
                var filepath = $"{request.getFullPath(_file_location_options.ApVouchers)}";
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
        public FileResult ApVouchersFileGet(String path, String file)
        {

            FileResult fr = null;

            try
            {
                var filepath = Path.Combine(_file_location_options.ApVouchers, path, file);
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
