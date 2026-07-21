using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebAPIScratch;

public class TcpServer(int port)
{
    public void Start()
    {
        var listener = new TcpListener(IPAddress.Any, port);
        listener.Start();

        Console.WriteLine("Listening on port 5000");

        while (true)
        {
            // Blocks here until a client connects
            TcpClient client = listener.AcceptTcpClient();
            HandleClient(client);
        }
    }

    private static void HandleClient(TcpClient client)
    {
        // The two-way pipe of bytes to and from the client
        NetworkStream stream = client.GetStream();

        byte[] buffer = new byte[4096];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);

        string rawData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine(rawData);

        client.Close();
    }
}
