var builder = DistributedApplication.CreateBuilder(args);

//builder.AddAzureProvisioning();
var cache = builder.AddRedis("cache");
var sql = builder.AddSqlServer("todoContext");
var messaging = builder.AddRabbitMQ("RabbitMQConnection");
var mongo = builder.AddMongoDB("mongo")
                    .WithDataVolume()
                    .WithMongoExpress();

var apiService = builder.AddProject<Projects.AspireWebApp_ApiService>("apiservice");

var todoApi = builder.AddProject<Projects.Todo_API>("todo-api")
                        .WithReference(messaging)
                        .WithReference(sql);

var notes = builder.AddProject<Projects.Notes_API>("notes-api")
    .WithReference(mongo);

builder.AddProject<Projects.AspireWebApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(todoApi)
    .WithReference(notes)
    .WithReference(apiService)
    .WithReference(messaging);

builder.Build().Run();