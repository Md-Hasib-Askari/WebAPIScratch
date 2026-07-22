using WebAPIScratch;

var server = new TcpServer(5000);

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
        HttpResponseWriter.WriteResponse(s, 200, "OK", "User List");
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
        if (!request.Headers.ContainsKey("Authorization"))
        {
            HttpResponseWriter.WriteResponse(stream, 401, "Unauthorized", "Missing token.");
            return;
        }

        next(request, stream);
    };

var pipeline = new Pipeline().Use(logger).Use(auth).Build(finalHandler: router.Dispatch);

server.Start(pipeline);
