using System.Text;
using WebAPIScratch;
using WebAPIScratch.Common;
using WebAPIScratch.Controllers;
using WebAPIScratch.Core;
using WebAPIScratch.Interfaces;
using WebAPIScratch.Services;

var server = new TcpServer(5000);

// Service Container
var container = new ServiceContainer();

// Service Registration
container.Register<IUserRepository>(() => new UserRepository());
container.Register<ILogger>(() => new Logger());

// Controllers
var userController = new UserController(
    container.Resolve<IUserRepository>(),
    container.Resolve<ILogger>()
);

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

// router.Map("GET", "/users", userController.GetUsers);

// Controller Registration
ControllerRegistrar.RegisterControllerRoutes(router, userController);

// Middlewares
RequestDelegate logger(RequestDelegate next) =>
    (request, stream) =>
    {
        // Instead of Console.WriteLine, we are using
        // ILogger from service container
        var logger = container.Resolve<ILogger>();

        logger.Info("================================");
        logger.Info($"Logger Middleware: {request.Method} {request.Path}");
        logger.Info("================================");
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
