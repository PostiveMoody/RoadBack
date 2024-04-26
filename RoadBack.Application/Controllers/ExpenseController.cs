using AutoMapper;
using Denunciation.Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoadBack.DAL.Services.Interfaces;

namespace RoadBack.Application.Controllers
{
    [Route("[controller]")]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;
        private readonly IMapper _mapper;

        public ExpenseController(IExpenseService expenseService, IMapper mapper)
        {
            _expenseService = expenseService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Expense()
        {
            var result = await _expenseService.GetExpensesAsync(1, true);
            if(result == null)
            {
                return View();
            }

            var expense = result.Data.SingleOrDefault();
            if(expense == null)
            {
                ViewBag.Message = "В этом месяце еще не было трат";
                return View();
            }

            ViewBag.Message = expense.Payment.ToString();
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Expense(DTO.Expense model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _expenseService.CreateExpenseAsync(_mapper.Map<Domain.Models.Expense>(model));
            
            return View(model);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateExpense([FromBody] DTO.Expense expense)
        {
            return Json(await _expenseService.CreateExpenseAsync(_mapper.Map<Domain.Models.Expense>(expense)));
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteExpense([FromQuery] Guid id)
        {
            return Json(await _expenseService.DeleteExpenseAsync(id));
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetExpense([FromQuery] Guid id)
        {
            return Json(await _expenseService.GetExpenseByIdAsync(id));
        }

        [HttpPut]
        [Route("put")]
        public async Task<IActionResult> UpdateExpense([FromQuery] DTO.Expense expense)
        {
            var blExpense = _mapper.Map<Domain.Models.Expense>(expense);
            return Json(await _expenseService.UpdateExpenseAsync(blExpense));
        }
    }
}
