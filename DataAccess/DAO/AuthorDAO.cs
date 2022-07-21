using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.Models;

namespace FEBook.DataAccess.DAO
{
    public class AuthorDAO
    {
        private static AuthorDAO instance = null;
        private static readonly object instanceLock = new object();
        public static AuthorDAO Instance {
            get {
                lock (instanceLock) {
                    if (instance == null) {
                        instance = new AuthorDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Author> GetAuthorList() {
            var authors = new List<Author>();
            try {
                using var context = new EbookManagementContext();
                authors = context.Authors.Where(s => s.DeleteStatus == false).ToList();

            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return authors;
        }

        public IEnumerable<Author> GetAuthorDeletedList()
        {
            var authors = new List<Author>();
            try
            {
                using var context = new EbookManagementContext();
                authors = context.Authors.Where(s => s.DeleteStatus == true).ToList();
                return authors;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Author GetAuthorByID(int authorID) {
            Author author = null;
            try {
                using var context = new EbookManagementContext();
                author = context.Authors.SingleOrDefault(a => a.AuthorId == authorID);

            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return author;
        }

        public void Create (Author author) {
            try {
                Author _author = GetAuthorByID(author.AuthorId);
                //ID not collapse 
                if (_author == null) {
                    using var context = new EbookManagementContext();
                    context.Authors.Add(author);
                    context.SaveChanges();

                } else { 
                    throw new Exception("The Author is already exist");
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void Edit (Author author) {
            try {
                Author _author = GetAuthorByID(author.AuthorId);
                if (_author != null) {
                    using var context = new EbookManagementContext();
                    context.Authors.Update(author);
                    context.SaveChanges();
                } else {
                    throw new Exception("The Author does not exist");

                } 
            } catch (Exception ex) {
                throw new Exception (ex.Message);
            }
        }

        public void Delete(int authorID) {
            try {
                Author _author = GetAuthorByID(authorID);
                if(_author !=null ) {
                    using var context = new EbookManagementContext();
                    context.Authors.Remove(_author);
                    context.SaveChanges();
                } else {
                throw new Exception ("The Author does not exist");
                }
            } catch (Exception ex) {
                throw new Exception (ex.Message);
            }
        }

        public void DeleteOnce(int authorID){
            try{
                Author _author = GetAuthorByID(authorID);
                if (_author != null)
                {
                    using var context = new EbookManagementContext();
                    _author.DeleteStatus=true;
                    context.Authors.Update(_author);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The _author does not not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal void Restore(int authorID)
        {
            try{
                Author _author = GetAuthorByID(authorID);
                if (_author != null)
                {
                    using var context = new EbookManagementContext();
                    _author.DeleteStatus=false;
                    context.Authors.Update(_author);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The _author does not not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        
    }
}