using System;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceAPI.Data;
using PersonalFinanceAPI.Models;

namespace PersonalFinanceAPI.Services;

public class ExpenseService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Expense> AddExpense(Expense expense) {
        _context.Add(expense);
        await _context.SaveChangesAsync();
        return expense;
    }

    public async Task<Expense> GetExpense(int id){
        var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);

        return expense;
    }

    public async Task<Expense[]> GetExpenses(){
        var expenses = _context.Expenses.ToArray();

        return expenses;
    }
}
