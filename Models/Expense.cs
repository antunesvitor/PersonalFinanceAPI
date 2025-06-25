using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceAPI.Models;

public class Expense
{
    public int Id { get; set; }
    public decimal Value { get; set; }
    public DateTime? Date { get; set; }
    [Required]
    public required string Description { get; set; }
    public int GroupID { get; set; }

    public Group? Group { get; set; }
}
