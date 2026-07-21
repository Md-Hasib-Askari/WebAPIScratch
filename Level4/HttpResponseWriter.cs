using System.Net.Sockets;
using System.Text;

namespace WebAPIScratch;

public class HttpResponseWriter
{
    public static void WriteResponse(
        NetworkStream stream,
        int statusCode,
        string statusText,
        string body
    )
    {
        var headers = new StringBuilder();

        // Response Signature
        headers.Append($"HTTP/1.1 {statusCode} {statusText}\r\n");

        // Response headers
        headers.Append("Content-Type: text/plain\r\n");
        headers.Append($"Content-Length: {Encoding.UTF8.GetByteCount(body)}\r\n");
        headers.Append("Connection: close\r\n");
        headers.Append("\r\n");

        byte[] headerBytes = Encoding.UTF8.GetBytes(headers.ToString());
        byte[] bodyBytes = Encoding.UTF8.GetBytes(body);

        stream.Write(headerBytes, 0, headerBytes.Length);
        stream.Write(bodyBytes, 0, bodyBytes.Length);

        stream.Flush();
    }
}
