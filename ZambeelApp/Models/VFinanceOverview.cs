using System;
using System.Collections.Generic;

namespace ZambeelApp.Models;

public partial class VFinanceOverview
{
    public string Name { get; set; } = null!;

    public string? TransactionType { get; set; }

    public decimal? Amount { get; set; }

    public bool? IsPaid { get; set; }

    public DateOnly? IssueDate { get; set; }
}
