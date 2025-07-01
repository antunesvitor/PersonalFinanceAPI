using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceAPI.DTOs;
using PersonalFinanceAPI.Models;
using PersonalFinanceAPI.Services;

namespace PersonalFinanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController(ExpenseService service) : ControllerBase
    {
        private readonly ExpenseService _service = service;

        [HttpGet]
        public async Task<ExpenseResponse[]> GetGroups()
        {
            return await _service.GetExpenses();
        }

        [HttpGet("{id:int}")]
        public async Task<ExpenseResponse> GetGroup(int id)
        {
            return await _service.GetExpense(id);
        }

        [HttpPost()]
        public async Task<Expense> AddExpense([FromBody] CreateExpenseRequest expense)
        {
            var expenseDB = await _service.AddExpense(expense);
            return expenseDB;
        }

        [HttpPost("add-many")]
        public async Task<Expense[]> AddExpenses([FromBody] CreateExpenseRequest[] expenses)
        {
            var expensesDB = await _service.AddManyExpenses(expenses);
            return expensesDB;
        }

        [HttpDelete("{id:int}")]
        public async Task<IResult> DeleteExpense(int id)
        {
            var success = await _service.DeleteExpenseById(id);
            return success ? Results.Ok() : Results.NotFound("Record not found");
        }


        [HttpDelete()]
        public async Task<IResult> DeleteAllExpenses()
        {
            var success = await _service.DeleteAll();
            return success ? Results.Ok() : Results.NotFound("There is no record in database");
        }
        
        [HttpPut("{expenseId:int}/add-to-group/{groupId:int}")]
        public async Task<IResult> AddExpenseToGroup([FromRoute] int expenseId, [FromRoute] int groupId)
        {
            bool succesUpdate = await _service.AddExpenseToGroup(expenseId, groupId);
            
            if (!succesUpdate)
                return Results.BadRequest("Invalid expenseID or groupID");

            var updatedExpense = await _service.GetExpense(expenseId);

            return Results.Ok(updatedExpense);
        }
    }
}
