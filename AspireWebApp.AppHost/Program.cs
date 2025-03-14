var builder = DistributedApplication.CreateBuilder(args);

//builder.AddAzureProvisioning();
var cache = builder.AddRedis("cache");
var sql = builder.AddSqlServer("todoContext");
var messaging = builder.AddRabbitMQ("RabbitMQConnection");

var apiService = builder.AddProject<Projects.AspireWebApp_ApiService>("apiservice");

var todoApi = builder.AddProject<Projects.Todo_API>("todo-api")
                        .WithReference(messaging)
                        .WithReference(sql);

builder.AddProject<Projects.AspireWebApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(todoApi)
    .WithReference(apiService)
    .WithReference(messaging);

builder.Build().Run();