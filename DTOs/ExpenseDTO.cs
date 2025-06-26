using System;
using System.Data;
using System.Text.RegularExpressions;
using PersonalFinanceAPI.Models;

namespace PersonalFinanceAPI.DTOs;

// Response DTOs - what gets returned to clients
public record ExpenseResponse(
    int Id,
    decimal Value,
    DateTime Date,
    string Description,
    int GroupId,
    string GroupName
)
{
    public ExpenseResponse(Expense expense)
        : this (
            expense.Id, 
            expense.Value, 
            expense.Date ?? DateTime.Now, 
            expense.Description,
            expense.GroupID, 
            expense.Group?.Name ?? "-")
    { }
}


// Request DTOs - what clients send
public record CreateExpenseRequest(
    decimal Value,
    DateTime? Date,
    int GroupId,
    string Description
)
{
      public DateTime EffectiveDate => Date ?? DateTime.Now;
}

public record UpdateExpenseRequest(
    decimal Value,
    DateTime Date,
    int GroupId
);