using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.Models;

namespace FEBook.Controllers.Repository
{
    public interface ISubjectRepository
    {
        IEnumerable<Subject> GetSubjects();
        Subject GetSubjectByID(int subjectID);
        void CreateSubject(Subject subject);
        void EditSubject(Subject subject);
    }
}