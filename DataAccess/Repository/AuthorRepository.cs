using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.DataAccess.DAO;
using FEBook.Models;

namespace FEBook.DataAccess.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        public IEnumerable<Author> GetAuthors() {
            return AuthorDAO.Instance.GetAuthorList();
        }

        public Author GetAuthorByID(int AuthorId) {
            return AuthorDAO.Instance.GetAuthorByID(AuthorId);
        }
        public void CreateAuthor(Author author) {
            AuthorDAO.Instance.Create(author);
        }
        public void EditAuthor(Author author) {
            AuthorDAO.Instance.Edit(author);
        }
        public void DeleteAuthor(int AuthorId) {
            AuthorDAO.Instance.Delete(AuthorId);
        }
    }
}