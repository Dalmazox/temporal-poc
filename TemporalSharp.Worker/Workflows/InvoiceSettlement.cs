using Microsoft.Extensions.Logging;
using Temporalio.Workflows;
using TemporalSharp.Worker.Workflows.Activities;

namespace TemporalSharp.Worker.Workflows;

public record WorkflowArgs(Guid InvestmentAccount, Guid CreditCardAccount);

[Workflow]
public class InvoiceSettlement
{
  [WorkflowRun]
  public async Task<bool> RunAsync(WorkflowArgs args)
  {
    var options = new ActivityOptions
    {
      StartToCloseTimeout = TimeSpan.FromMilliseconds(500)
    };

    var valueSold = await Workflow.ExecuteActivityAsync<SaleInvestment, decimal>(
      act => act.SaleAsync(args.InvestmentAccount), options);

    Workflow.Logger.LogInformation("Value sold: {value}", valueSold);

    var changeLimitSuccess = await Workflow.ExecuteActivityAsync<ChangeLimit, bool>(
      act => act.ChangeAsync(args.CreditCardAccount), options);

    Workflow.Logger.LogInformation("Change limit result: {result}", changeLimitSuccess);

    var invoiceAdjustmentId = await Workflow.ExecuteActivityAsync<CreateAdjustment, Guid>(
      act => act.CreateAsync(args.CreditCardAccount), options);

    Workflow.Logger.LogInformation("Adjustment created: {adjustmentId}", invoiceAdjustmentId);

    return true;
  }
}