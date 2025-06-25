using System;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceAPI.Data;
using PersonalFinanceAPI.DTOs;
using PersonalFinanceAPI.Models;
using SQLitePCL;

namespace PersonalFinanceAPI.Services;

public class ExpenseService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Expense> AddExpense(Expense expense)
    {
        expense.Date ??= DateTime.Now;

        _context.Add(expense);
        await _context.SaveChangesAsync();
        return expense;
    }

    public async Task<ExpenseDTO?> GetExpense(int id)
    {
        var expense = await _context.Expenses
            .Include(x => x.Group)
            .FirstOrDefaultAsync(e => e.Id == id);

        return expense is not null ? new ExpenseDTO(expense) : null;
    }

    public async Task<ExpenseDTO[]> GetExpenses()
    {
        var expenses = _context.Expenses.Include(x => x.Group).ToArray();
        var expensesDTO = expenses.Select(x => new ExpenseDTO(x)).ToArray();

        return expensesDTO;
    }
}
