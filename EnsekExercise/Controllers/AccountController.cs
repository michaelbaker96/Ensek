using EnsekExercise.Models;
using EnsekExercise.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekExercise.Controllers
{
    [ApiController]
    [Route("accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
       
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAccounts()
        {
            var accounts = _accountService.GetAllAccounts();

            return Ok(accounts);
        }

        [HttpGet("{id}", Name = "GetAccountById")]
        public ActionResult<Account> GetAccountById(int id)
        {
            var account = _accountService.GetAccountById(id);

            if (account != null)
            {
                return Ok(account);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Account> CreateAccount(Account account)
        {
            _accountService.CreateAccount(account);
            _accountService.SaveChanges();
                  
            return CreatedAtRoute(nameof(GetAccountById), new { Id = account.Id }, account);
        }
    }
}
