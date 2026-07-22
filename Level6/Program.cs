using System.Text;
using WebAPIScratch;
using WebAPIScratch.Interfaces;
using WebAPIScratch.Services;

var server = new TcpServer(5000);

// Service Container
var container = new ServiceContainer();

// Service Registration
container.Register<IUserRepository>(() => new UserRepository());

// Routes
var router = new Router();
router.Map(
    "GET",
    "/",
    (req, s) =>
    {
        HttpResponseWriter.WriteResponse(s, 200, "OK", "Welcome!");
    }
);
router.Map(
    "GET",
    "/users",
    (req, s) =>
    {
        IUserRepository repo = container.Resolve<IUserRepository>();

        // All Users
        var users = repo.GetAll();

        var response = new StringBuilder();
        response.Append("User List:\n\n");
        foreach (var user in users)
        {
            response.Append($"{user.Id}: {user.Name} is {user.Age} years old.\n");
        }

        HttpResponseWriter.WriteResponse(s, 200, "OK", response.ToString());
    }
);

// Middlewares
RequestDelegate logger(RequestDelegate next) =>
    (request, stream) =>
    {
        Console.WriteLine("================================");
        Console.WriteLine($"Logger Middleware: {request.Method} {request.Path}");
        Console.WriteLine("================================");
        next(request, stream);
    };
RequestDelegate auth(RequestDelegate next) =>
    (request, stream) =>
    {
        // if (!request.Headers.ContainsKey("Authorization"))
        // {
        //     HttpResponseWriter.WriteResponse(stream, 401, "Unauthorized", "Missing token.");
        //     return;
        // }

        next(request, stream);
    };

var pipeline = new Pipeline().Use(logger).Use(auth).Build(finalHandler: router.Dispatch);

server.Start(pipeline);
