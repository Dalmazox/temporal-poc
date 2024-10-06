using Temporalio.Activities;

namespace TemporalSharp.Worker.Workflows.Activities;

public class ChangeLimit
{
  [Activity]
  public async Task<bool> ChangeAsync(Guid CreditCardAccount)
  {
    await Task.Delay(200);

    return true;
  }
}