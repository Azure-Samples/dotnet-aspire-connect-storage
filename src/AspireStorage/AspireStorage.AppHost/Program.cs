using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.AddAzureStorage("Storage");

if (builder.Environment.IsDevelopment())
{
    storage.RunAsEmulator();
}

var blobs = storage.AddBlobs("BlobConnection");
var queues = storage.AddQueues("QueueConnection");

var apiService = builder.AddProject<Projects.AspireStorage_ApiService>("apiservice");

builder.AddProject<Projects.AspireStorage_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithReference(blobs)
    .WaitFor(blobs)
    .WithReference(queues)
    .WaitFor(queues);

builder.AddProject<Projects.AspireStorage_WorkerService>("aspirestorage-workerservice")
    .WithReference(queues)
    .WaitFor(queues);

builder.Build().Run();
