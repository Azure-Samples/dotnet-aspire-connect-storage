var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AspireStorage>("aspirestorage");

builder.AddProject<Projects.AspireStorage_Worker>("aspirestorage.worker");

builder.Build().Run();
