using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceAPI.Data;
using PersonalFinanceAPI.DTOs;
using PersonalFinanceAPI.Models;
using SQLitePCL;

namespace PersonalFinanceAPI.Services;

public class ExpenseService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<Expense> AddExpense(CreateExpenseRequest request)
    {
        var newExpense = new Expense(request);

        _context.Add(newExpense);
        await _context.SaveChangesAsync();
        return newExpense;
    }

        public async Task<Expense[]> AddManyExpenses(CreateExpenseRequest[] request)
    {
        var expenses = request.Select(x => new Expense(x)).ToArray();

        _context.AddRange(expenses);
        await _context.SaveChangesAsync();
        return expenses;
    }

    public async Task<ExpenseResponse?> GetExpense(int id)
    {
        var expense = await _context.Expenses
            .Include(x => x.Group)
            .FirstOrDefaultAsync(x=> x.Id == id);

        return expense is not null ? new  ExpenseResponse(expense) : null;
    }

    public async Task<ExpenseResponse[]> GetExpenses()
    {
        var expenses = _context.Expenses.Include(x => x.Group).ToArray();
        var expensesDTO = expenses.Select(x => new ExpenseResponse(x)).ToArray();

        return expensesDTO;
    }
}
