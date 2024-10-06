using Microsoft.Extensions.Logging;

namespace TemporalSharp.Worker.Services;

public interface IInvoiceService
{
  Task<Guid> CreateAdjustmentAsync(Guid CreditCardAccount);
}

public class InvoiceService(ILogger<IInvoiceService> logger) : IInvoiceService
{
  public Task<Guid> CreateAdjustmentAsync(Guid CreditCardAccount)
  {
    logger.LogInformation("Creating adjustment for account {account}", CreditCardAccount);

    var adjustmentId = Guid.NewGuid();

    logger.LogInformation("Adjustment {adjustment} created for account {account}", adjustmentId, CreditCardAccount);

    return Task.FromResult(adjustmentId);
  }
}