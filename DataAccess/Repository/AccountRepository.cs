using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.DataAccess.DAO;
using FEBook.Models;

namespace FEBook.DataAccess.Repository
{
     public class AccountRepository : IAccountRepository
    {
        public IEnumerable<Account> GetAccounts()
        {
            return AccountDAO.Instance.GetAccountList();
        }             

        public Account GetAccountByID(int AccountID)
        {
            return AccountDAO.Instance.GetAccountByID(AccountID);
        }

        public Account GetAccountByEmail(string Email)
        {
            return AccountDAO.Instance.GetAccountByEmail(Email);
        }
      
        public void InsertAccount(Account Account)
        {
            AccountDAO.Instance.AddNew(Account);
        }

        public void UpdateAccount(Account Account)
        {
            AccountDAO.Instance.Update(Account);
        }

        public void DeleteAccount(int AccountID)
        {
            AccountDAO.Instance.Delete(AccountID);
        }
    }
}