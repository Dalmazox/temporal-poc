using Temporalio.Activities;

namespace TemporalSharp.Worker.Workflows.Activities;

public class SaleInvestment
{
    [Activity]
    public async Task<decimal> SaleAsync(Guid InvestmentAccount)
    {
        await Task.Delay(100);

        return 112m;
    }

}