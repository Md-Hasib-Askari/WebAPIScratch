using System.Net.Sockets;
using System.Text;
using WebAPIScratch.Common;
using WebAPIScratch.Domain;
using WebAPIScratch.Interfaces;

namespace WebAPIScratch.Controllers;

public class UserController(IUserRepository repo, ILogger logger)
{
    public void GetUsers(HttpRequest request, NetworkStream stream)
    {
        // Example of logger usage with DI
        logger.Info("User request for Users List!\n");

        // All Users
        var users = repo.GetAll();

        var response = new StringBuilder();
        response.Append("User List:\n\n");
        foreach (var user in users)
        {
            response.Append($"{user.Id}: {user.Name} is {user.Age} years old.\n");
        }

        HttpResponseWriter.WriteResponse(stream, 200, "OK", response.ToString());
    }
}
