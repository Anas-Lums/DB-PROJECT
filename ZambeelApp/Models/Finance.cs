using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class Finance
{
    public int FinanceTransactionId { get; set; }

    public int StudentId { get; set; }

    public string? TransactionType { get; set; }

    public decimal? Amount { get; set; }

    public DateOnly? IssueDate { get; set; }

    public DateOnly? DueDate { get; set; }

    public bool? IsPaid { get; set; }

    public virtual Student Student { get; set; } = null!;
}
