using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FEBook.Models;

namespace EbookProject.DataAccess.Repository
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
            AccountDAO.Instance.Remove(AccountID);
        }
    }
}