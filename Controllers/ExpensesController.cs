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
        public async Task<ExpenseDTO[]> GetGroups()
        {
            return await _service.GetExpenses();
        }

        [HttpGet("{id:int}")]
        public async Task<ExpenseDTO> GetGroup(int id)
        {
            return await _service.GetExpense(id);
        }

        [HttpPost()]
        public async Task<Expense> AddExpense([FromBody] Expense expense)
        {
            var expenseDB = await _service.AddExpense(expense);
            return expenseDB;
        }
    }
}
