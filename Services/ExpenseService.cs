using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceAPI.Data;
using PersonalFinanceAPI.DTOs;
using PersonalFinanceAPI.Models;

namespace PersonalFinanceAPI.Services;

public class ExpenseService(AppDbContext context, GroupService groupService)
{
    private readonly AppDbContext _context = context;
    private readonly GroupService groupService = groupService;

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
            .FirstOrDefaultAsync(x => x.Id == id);

        return expense is not null ? new ExpenseResponse(expense) : null;
    }

    public async Task<ExpenseResponse[]> GetExpenses()
    {
        var expenses = _context.Expenses.Include(x => x.Group).ToArray();
        var expensesDTO = expenses.Select(x => new ExpenseResponse(x)).ToArray();

        return expensesDTO;
    }

    public async Task<bool> DeleteExpenseById(int id)
    {
        int rowsAffected = await _context.Expenses.Where(x => x.Id == id).ExecuteDeleteAsync();

        return rowsAffected > 0;
    }

    public async Task<bool> AddExpenseToGroup(int expenseId, int groupId)
    {
        var expenseDb = await _context.Expenses.FirstOrDefaultAsync(x => x.Id == expenseId);
        var group = await this.groupService.GetGroup(groupId);

        if (expenseDb is null || group is null) return false;

        expenseDb.GroupID = groupId;

        int rowsAffected = await _context.SaveChangesAsync();

        return rowsAffected > 0;
    }
    
    public async Task<bool> UpdateExpense(int expenseId, CreateExpenseRequest expense)
    {
        var expenseDb = await _context.Expenses.FirstOrDefaultAsync(x => x.Id == expenseId);

        if (expenseDb is null) return false;

        expenseDb.Description = expense.Description;
        expenseDb.GroupID = expense.GroupId;
        
        int rowsAffected = await _context.SaveChangesAsync();

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAll()
    {
        #if DEBUG
            // Only allow in development
            int rowsAffected = await context.Expenses.ExecuteDeleteAsync();

            return rowsAffected > 0;
        #else
            throw new InvalidOperationException("Bulk delete not allowed in production");
        #endif
    }
}
