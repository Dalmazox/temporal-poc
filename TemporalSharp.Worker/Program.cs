using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Temporalio.Extensions.Hosting;
using TemporalSharp.Worker.Services;
using TemporalSharp.Worker.Workflows;
using TemporalSharp.Worker.Workflows.Activities;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
  .AddTemporalClient("localhost:7233", "default");
builder.Services
  .AddHostedTemporalWorker("invoice-settlement")
  .AddScopedActivities<SaleInvestment>()
  .AddScopedActivities<ChangeLimit>()
  .AddScopedActivities<CreateAdjustment>()
  .AddWorkflow<InvoiceSettlement>();

builder.Services.TryAddScoped<IInvoiceService, InvoiceService>();

await builder.Build().RunAsync();
