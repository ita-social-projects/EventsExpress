using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace EventsExpress.Test.ServiceTests.TestClasses.Photo
{
    internal static class PhotoHelpers
    {
        public static FormFile GetPhoto(string filePath, MemoryStream stream)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            string base64 = Convert.ToBase64String(bytes);
            string fileName = Path.GetFileName(filePath);
            stream.Write(Encoding.UTF8.GetBytes(base64), 0, base64.Length);
            var file = new FormFile(stream, 0, stream.Length, string.Empty, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = GetContentType(fileName),
            };
            return file;
        }

        public static string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
