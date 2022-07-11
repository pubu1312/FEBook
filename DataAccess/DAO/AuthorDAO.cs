using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.Models;

namespace FEBook.DataAccess.DAO
{
    public class AuthorDAO
    {
        // private static AuthorDAO instance = null;
        // private static readonly object instanceLock = new object();
        // public static AuthorDAO Instance {
        //     get {
        //         lock (instanceLock) {
        //             if (instance == null) {
        //                 instance = new AuthorDAO();
        //             }
        //             return instance;
        //         }
        //     }
        // }

        // public IEnumerable<Author> GetAuthorList() {
        //     var Authors = new List<Author>();
        //     try {
        //         using var context = new EbookManagementContext();
        //         Authors = context.Authors.ToList();
        //     } catch (Exception ex) {
        //         throw new Exception(ex.Message);
        //     }
        //     //System.Console.WriteLine(Authors.Count);
        //     return Authors;
        // }

        // public Author GetAuthorByID(int AuthorId) {
        //     Author Author = null;
        //     try {
        //         using var context = new EbookManagementContext();
        //         Author = context.Authors.SingleOrDefault(p => p.AuthorId == AuthorId);
        //     } catch (Exception ex) {
        //         throw new Exception(ex.Message);
        //     }
        //     return Author;
        // }

        // public void Create(Author author) {
        //     try {
        //         Author _Author = GetAuthorByID(author.AuthorId);
        //         //ID not collapse
        //         if (_Author == null) {
        //             using var context = new EbookManagementContext();
        //             context.Authors.Add(author); 
        //             context.SaveChanges();
        //         } else {
        //             throw new Exception("The Author is already exist.");
        //         }
        //     } catch (Exception ex) {
        //         throw new Exception(ex.Message);
        //     }
        // }

        // public void Edit(Author author) {
        //     try {
        //         Author _Author = GetAuthorByID(author.AuthorId);
        //         if (_Author != null) {
        //             using var context = new EbookManagementContext();
        //             context.Authors.Update(author);
        //             context.SaveChanges();
        //         } else {
        //             throw new Exception("The Author does not not exist.");
        //         }
        //     } catch (Exception ex) {
        //         throw new Exception(ex.Message);
        //     }
        // }

        // public void Delete(int AuthorId) {
        //     try {
        //         Author _Author = GetAuthorByID(AuthorId);
        //         if (_Author != null) {
        //             using var context = new EbookManagementContext();
        //             context.Authors.Remove(_Author);
        //             context.SaveChanges();
        //         } else {
        //             throw new Exception("The Author does not not exist.");
        //         }
        //     } catch (Exception ex) {
        //         throw new Exception(ex.Message);
        //     }
        // }
    }
}