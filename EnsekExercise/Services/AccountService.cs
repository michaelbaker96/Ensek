using EnsekExercise.Data;
using EnsekExercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekExercise.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        public void CreateAccount(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            _context.Accounts.Add(account);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _context.Accounts.ToList();
        }

        public Account GetAccountById(int id)
        {
            return _context.Accounts.FirstOrDefault(plat => plat.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
