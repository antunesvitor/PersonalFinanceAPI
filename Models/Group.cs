using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalFinanceAPI.Models;

public class Group
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public ICollection<Expense> Expenses { get; set; } = [];
}
