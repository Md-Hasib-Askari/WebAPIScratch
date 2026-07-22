using System.Net.Sockets;
using System.Text;

namespace WebAPIScratch;

public record HttpRequest(
    string Method,
    string Path,
    string Version,
    Dictionary<string, string> Headers,
    string Body
);

public class HttpRequestReader
{
    public static string ReadRawRequest(NetworkStream stream, int bufferLength = 8192)
    {
        var buffer = new byte[bufferLength];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        return Encoding.UTF8.GetString(buffer, 0, bytesRead);
    }

    public static HttpRequest ParseRequest(string raw)
    {
        var lines = raw.Split("\r\n");

        // First line looks like: GET /path HTTP/1.1
        var startLine = lines[0].Split(' ');
        string method = startLine[0];
        string path = startLine[1];
        string version = startLine[2];

        var headers = new Dictionary<string, string>();

        int i = 1;
        while (i < lines.Length && lines[i] != "")
        {
            var parts = lines[i].Split(':', 2);
            headers[parts[0].Trim()] = parts[1].Trim();
            i++;
        }

        // Everything after the blank line is the body
        string body = string.Join("\r\n", lines.Skip(i + 1));

        return new HttpRequest(method, path, version, headers, body);
    }
}
