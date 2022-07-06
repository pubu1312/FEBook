using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.DAO;
using FEBook.Controllers.Repository;
using FEBook.Models;

namespace FEBook.Controllers.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        public IEnumerable<Subject> GetSubjects() {
            return SubjectDAO.Instance.GetSubjectList();
        }

        public Subject GetSubjectByID(int SubjectId) {
            return SubjectDAO.Instance.GetSubjectByID(SubjectId);
        }

        public void CreateSubject(Subject Subject) {
            SubjectDAO.Instance.Create(Subject);
        }

        public void EditSubject(Subject Subject) {
            SubjectDAO.Instance.Edit(Subject);
        }
    }
}