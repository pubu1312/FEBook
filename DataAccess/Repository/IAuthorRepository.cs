using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.Models;

namespace FEBook.DataAccess.Repository
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAuthors();
        Author GetAuthorByID(int AuthorId);
        void CreateAuthor(Author author);
        void EditAuthor(Author author);
        void DeleteAuthor(int author);
        
    }
}