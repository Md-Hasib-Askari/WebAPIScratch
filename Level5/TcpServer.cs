using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebAPIScratch;

public class TcpServer(int port)
{
    public void Start(RequestDelegate pipeline)
    {
        var listener = new TcpListener(IPAddress.Any, port);
        listener.Start();

        Console.WriteLine("Listening on port 5000");

        while (true)
        {
            // Blocks here until a client connects
            TcpClient client = listener.AcceptTcpClient();
            HandleClient(client, pipeline);
        }
    }

    private static void HandleClient(TcpClient client, RequestDelegate pipeline)
    {
        // The two-way pipe of bytes to and from the client
        NetworkStream stream = client.GetStream();

        var rawData = HttpRequestReader.ReadRawRequest(stream);
        var parsedRequest = HttpRequestReader.ParseRequest(rawData);

        // Middleware Pipeline
        pipeline(parsedRequest, stream);

        client.Close();
    }
}
