using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.Models;

namespace FEBook.DataAccess.Repository
{
    public interface ISubjectRepository
    {
        IEnumerable<Subject> GetSubjects();
        IEnumerable<Subject> GetDeletedSubjects();
        Subject GetSubjectByID(int subjectID);
        void CreateSubject(Subject subject);
        void EditSubject(Subject subject);
        void DeleteSubject(int subject);
        void DeleteOnce(int subject);
        void Restore(int subject);
    }
}