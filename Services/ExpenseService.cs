using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceAPI.Data;
using PersonalFinanceAPI.DTOs;
using PersonalFinanceAPI.DTOs.Generics;
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

    public async Task<PagedResponse<ExpenseResponse>> GetExpenses(ListExpensesResquest request)
    {
        var query = _context.Expenses.Include(x => x.Group).AsQueryable();

        if (request.StartDate.HasValue)
            query = query.Where(x => x.Date >= request.StartDate.Value);

        if (request.EndDate.HasValue)
            query = query.Where(x => x.Date <= request.EndDate.Value);

        var totalRecords = await query.CountAsync();

        var expenses = await query
                .OrderByDescending(x => x.Date)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(e => new ExpenseResponse(e))
                .ToListAsync();

        var response = new PagedResponse<ExpenseResponse>(
            expenses,
            request.Page,
            request.PageSize,
            totalRecords
        );

        return response;
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
