var builder = DistributedApplication.CreateBuilder(args);

//builder.AddAzureProvisioning();
var cache = builder.AddRedis("cache");
var sql = builder.AddSqlServer("todoContext");
var messaging = builder.AddRabbitMQ("RabbitMQConnection");

var todoApi = builder.AddProject<Projects.Todo_API>("todo-api")
                        .WithReference(messaging)
                        .WithReference(sql);

builder.AddProject<Projects.AspireWebApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(todoApi)
    .WithReference(messaging);

builder.AddProject<Projects.Exercises_API>("exercise-api");

builder.Build().Run();