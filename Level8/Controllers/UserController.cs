using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebAPIScratch.Common;
using WebAPIScratch.Core;
using WebAPIScratch.Interfaces;

namespace WebAPIScratch.Controllers;

public class UserController(IUserRepository repo, ILogger logger)
{
    [Route("GET", "/users")]
    public void GetUsers(HttpRequest _, NetworkStream stream)
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

    [Route("POST", "/users")]
    public void CreateUser(HttpRequest request, NetworkStream stream)
    {
        var body = request.Body;

        var jsonBody = JsonSerializer.Deserialize<CreateUserDto>(body);
        if (jsonBody is null)
        {
            logger.Error("Invalid body!");
            HttpResponseWriter.WriteResponse(stream, 400, "Bad Request", "Invalid Request");
            return;
        }

        var newUser = repo.Add(jsonBody);
        logger.Info($"New User Added: {newUser.Id}: {newUser.Name} - {newUser.Age} years old.");
        HttpResponseWriter.WriteResponse(stream, 201, "Created", "User Created");
    }
}

public record CreateUserDto(int Id, string Name, double Age);
