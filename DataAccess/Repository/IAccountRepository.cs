using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.Models;
namespace FEBook.DataAccess.Repository
{
    public interface IAccountRepository
    {
        public IEnumerable<Account> GetAccounts();

        Account GetAccountByID(int AccountID);
        Account GetAccountByEmail(string Email);
        void InsertAccount(Account Account);
        void DeleteAccount(int AccountID);
        void UpdateAccount(Account Account);
    }
}