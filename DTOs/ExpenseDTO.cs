using System;
using System.Text.RegularExpressions;
using PersonalFinanceAPI.Models;

namespace PersonalFinanceAPI.DTOs;

public class ExpenseDTO(Expense expense)
{
    public int Id { get; set; } = expense.Id;
    public decimal Value { get; set; } = expense.Value;
    public DateTime? DateTime { get; set; } = expense.Date;
    public string? GroupName { get; set; } = expense.Group?.Name;
}
