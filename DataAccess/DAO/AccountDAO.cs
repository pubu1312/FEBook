using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.Models;
namespace FEBook.DataAccess.DAO
{
    public class AccountDAO
    {
     private static AccountDAO instance = null;
        private static readonly object instanceLock = new object();
        public static AccountDAO Instance { 
            get{
                lock (instanceLock){
                    if (instance == null){
                        instance = new AccountDAO();
                    }
                    return instance;
                }
            }
         }
    
    public IEnumerable<Account> GetAccountList(){
        var Accounts = new List<Account>();
        try{
            using var context = new EbookManagementContext();
            Accounts = context.Accounts.ToList();
        }
        catch(Exception ex){
            throw new Exception(ex.Message);
        }
        return Accounts;
    }
    public Account GetAccountByID(int AccountID){ 
        Account Account = null;
        try{
            using var context = new EbookManagementContext();
            Account = context.Accounts.SingleOrDefault(c => c.UserId == AccountID);
        }
        catch(Exception ex){
            throw new Exception(ex.Message);
        }
            return Account;
     }
    
    public Account GetAccountByEmail(string Email){ 
        Account Account = null;
        try{
            using var context = new EbookManagementContext();
            foreach(var account in context.Accounts) {
                if (account.Email == Email) return account;
            }
            System.Console.WriteLine("Not found");
        }
        catch(Exception ex){
            throw new Exception(ex.Message);
        }
            return Account;
    }

    public Account LoginAccount(string email, string password){ 
        Account account = null;
        try{
            using var context = new EbookManagementContext();
            account = context.Accounts.SingleOrDefault(c => c.Email == email);
            if (!account.Passwords.Equals(password)) account = null;
        }
        catch(Exception ex){
            throw new Exception(ex.Message);
        }
        return account;
     }
     public void AddNew(Account Account){  
        try{
            Account _Account = GetAccountByID(Account.UserId);
            if(_Account == null){
                using var context = new EbookManagementContext();
                context.Accounts.Add(Account);
                context.SaveChanges();
            }
            else {
                throw new Exception ("The Account is already exist.");
            }
        }
        catch(Exception ex){
            throw new Exception(ex.Message);
        }
     }
     public void Update(Account Account){
        try{
             Account _Account = GetAccountByID(Account.UserId);
            if(_Account != null){
                using var context = new EbookManagementContext();
                context.Accounts.Update(Account);
                context.SaveChanges();
            }
            else {
                throw new Exception ("The Account does not already exist.");
            }
        }
        catch(Exception ex){
            throw new Exception(ex.Message);
        }
     }
     public void Remove(int AccountID){
        try{
           Account Account = GetAccountByID(AccountID);
            if(Account != null){
                using var context = new EbookManagementContext();
                context.Accounts.Remove(Account);
                context.SaveChanges();
            }
            else {
                throw new Exception ("The Account does not already exist.");
            }
        }
        catch (Exception ex){
        throw new Exception(ex.Message);
           }
        }
     }
}