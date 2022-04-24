using ImmoGlobal.Database;
using System.Collections.Generic;

namespace ImmoGlobal.MainClasses
{
  internal class Account
  {
    
    public int AccountId { get; set; }
    public string AccountNumber { get; set; }
    public string? AccountDescription { get; set; }
    public ICollection<PaymentRecord>? PaymentRecord { get; set; }

    public string Balance
    {
      get => (IncomeAmount - ExpenseAmount).ToString("0.00");
    }

    private double IncomeAmount
    {
      get
      {
        double income = 0;
        foreach (var item in DbController.GetIncomeToAccountDB(this))
        {
          income += item.IncomeAmount?? 0;
        }
        return income;
      }
    }

    private double ExpenseAmount
    {
      get
      {
        double income = 0;
        foreach (var item in DbController.GetExpenseToAccountDB(this))
        {
          income += item.ExpenseAmount?? 0;
        }
        return income;
      }
    }
  }
}
