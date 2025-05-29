using System;

namespace PersonalFinanceAPI.Models;

public class Expense
{
    public int Id { get; set; }
    public decimal Value { get; set; }
    public DateTime Date { get; set; }
    public int GroupID { get; set; }

    public Group? Group { get; set; }
}
