using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Hexacta.Core.Tools.Utilities
{
    public static class FileHttpResponseMessageHelper
    {
        public static HttpResponseMessage GetHttpResponse(string content, string fileName, string contentDisposition)
        {
            HttpContent httpContent = new StringContent(content);
            return GetHttpResponse(httpContent, content.Length, fileName, contentDisposition);
        }
        public static HttpResponseMessage GetHttpResponse(byte[] content, string fileName, string contentDisposition)
        {
            MemoryStream stream = new MemoryStream(content);
            return GetHttpResponse(stream, fileName, contentDisposition);
        }

        private static HttpResponseMessage GetHttpResponse(Stream stream, string fileName, string contentDisposition)
        {
            stream.Position = 0;
            HttpContent httpContent = new StreamContent(stream);
            return GetHttpResponse(httpContent, stream.Length, fileName, contentDisposition);
        }

        public static HttpResponseMessage GetHttpResponse(HttpContent httpContent, long length, string fileName, string contentDisposition = ContentDisposition.InLine)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = httpContent };
            var contentType = MimeMapping.GetMimeMapping(Path.GetExtension(System.IO.Path.GetExtension(fileName)));
            if (string.IsNullOrEmpty(contentType))
            {
                contentType = "application/octet-stream";
            }
            response.Content.Headers.ContentRange = new ContentRangeHeaderValue(int.MaxValue);
            response.Content.Headers.Add("Access-Control-Allow-Headers", "Range");
            response.Content.Headers.Add("Access-Control-Expose-Headers", "Accept-Ranges, Content-Encoding, Content-Length, Content-Range, Content-Disposition, Filename");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            response.Content.Headers.ContentLength = length;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue(contentDisposition);
            response.Content.Headers.ContentDisposition.FileName = Uri.EscapeDataString(System.IO.Path.GetFileName(fileName));
            response.Content.Headers.Add("Filename", response.Content.Headers.ContentDisposition.FileName);
            return response;
        }
        public struct ContentDisposition
        {
            public const string InLine = "inline";
            public const string Attachment = "attachment";
        }

    }
}
