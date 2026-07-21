using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebAPIScratch;

public class TcpServer(int port)
{
    public void Start(Router router)
    {
        var listener = new TcpListener(IPAddress.Any, port);
        listener.Start();

        Console.WriteLine("Listening on port 5000");

        while (true)
        {
            // Blocks here until a client connects
            TcpClient client = listener.AcceptTcpClient();
            HandleClient(client, router);
        }
    }

    private static void HandleClient(TcpClient client, Router router)
    {
        // The two-way pipe of bytes to and from the client
        NetworkStream stream = client.GetStream();

        var rawData = HttpRequestReader.ReadRawRequest(stream);
        var parsedRequest = HttpRequestReader.ParseRequest(rawData);

        // Request Signature
        Console.WriteLine($"{parsedRequest.Method} {parsedRequest.Path} {parsedRequest.Version}");

        // Headers
        foreach (var kvp in parsedRequest.Headers)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        // Newline separating body from headers
        Console.WriteLine();

        // Finally request body
        Console.WriteLine(parsedRequest.Body);

        router.Dispatch(parsedRequest, stream);

        client.Close();
    }
}
