using WebAPIScratch;

var server = new TcpServer(5000);

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

server.Start(router);
