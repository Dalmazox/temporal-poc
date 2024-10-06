using Temporalio.Activities;
using TemporalSharp.Worker.Services;

namespace TemporalSharp.Worker.Workflows.Activities;

public class CreateAdjustment(IInvoiceService invoiceService)
{
  [Activity]
  public Task<Guid> CreateAsync(Guid creditCardAccount)
  {
    return invoiceService.CreateAdjustmentAsync(creditCardAccount);
  }
}