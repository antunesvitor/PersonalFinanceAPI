using System;
using System.ComponentModel.DataAnnotations;
using PersonalFinanceAPI.DTOs;

namespace PersonalFinanceAPI.Models;

public class Expense
{
    public int Id { get; set; }
    public decimal Value { get; set; }
    public DateTime? Date { get; set; }
    [Required]
    public string Description { get; set; }
    public int GroupID { get; set; }

    public Group? Group { get; set; }

    public Expense(CreateExpenseRequest request)
    {
        this.Value = request.Value;
        this.Date = request.EffectiveDate;
        this.Description = request.Description;
        this.GroupID = request.GroupId;
    }

    public Expense() {}
}
