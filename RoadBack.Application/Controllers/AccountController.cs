using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RoadBack.Application.DTO;
using RoadBack.Application.Helpers;
using RoadBack.DAL.Services.Interfaces;

namespace RoadBack.Application.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("create")]
        [ValidationFilter]
        public async Task<IActionResult> CreateAccount([FromBody] Account account)
        {
            var result = await _accountService.CreateAccountAsync(_mapper.Map<Domain.Models.Account>(account));

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Json(result);
        }

        [HttpDelete]
        [Route("delete")]
        [ValidationFilter]
        public async Task<IActionResult> DeleteAccount([FromQuery] Guid id)
        {
            var result = await _accountService.DeleteAccountAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok();
        }

        [HttpGet]
        [Route("get")]
        [ValidationFilter]
        public async Task<IActionResult> GetAccount([FromQuery] Guid id)
        {
            var result = await _accountService.GetAccountByIdAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Json(result);
        }

        [HttpPut]
        [Route("update")]
        [ValidationFilter]
        public async Task<IActionResult> UpdateAccount([FromQuery] Account account)
        {
            var result = await _accountService.UpdateAccountAsync(_mapper.Map<Domain.Models.Account>(account));

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Json(result);
        }
    }
}
