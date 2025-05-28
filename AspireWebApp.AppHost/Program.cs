var builder = DistributedApplication.CreateBuilder(args);

//builder.AddAzureProvisioning();
var cache = builder.AddRedis("cache");
var todoContext = builder.AddSqlServer("todoContext");
var exerciseContext = builder.AddSqlServer("exerciseContext");
var messaging = builder.AddRabbitMQ("RabbitMQConnection");

var todoApi = builder.AddProject<Projects.Todo_API>("todo-api")
                        .WithReference(messaging)
                        .WithReference(todoContext);

builder.AddProject<Projects.AspireWebApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(todoApi)
    .WithReference(exerciseContext)
    .WithReference(messaging);

builder.AddProject<Projects.Exercises_API>("exercise-api")
    .WithReference(exerciseContext)
    .WithReference(messaging);

builder.Build().Run();