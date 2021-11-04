using EnsekExercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsekExercise.Services
{
    public interface IAccountService
    {
        bool SaveChanges();

        IEnumerable<Account> GetAllAccounts();

        Account GetAccountById(int id);

        void CreateAccount(Account account);
    }
}
