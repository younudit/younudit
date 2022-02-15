using System;
using System.IO;
using System.Text.Json.Serialization;

namespace NorthShore.Models
{
    public class BaseMapFileRequest
    {
        [JsonPropertyName("file_name")]
        public String FileName { get; set; }

        public String getFullPath(String NetworkLocation)
        {
            return Path.Combine(NetworkLocation, FileName);
        }
    }
}