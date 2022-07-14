using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.DataAccess.DAO;
using FEBook.DataAccess.Repository;
using FEBook.Models;

namespace FEBook.DataAccess.Repository{
    public class SubjectRepository : ISubjectRepository{
        public IEnumerable<Subject> GetSubjects() {
            return SubjectDAO.Instance.GetSubjectList();
        }

        public IEnumerable<Subject> GetDeletedSubjects(){
            return SubjectDAO.Instance.GetSubjectDeletedList();
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

        public void DeleteSubject(int SubjectId) {
            SubjectDAO.Instance.Delete(SubjectId);
        }

        public void DeleteOnce(int SubjectId){
            SubjectDAO.Instance.DeleteOnce(SubjectId);
        }
    }
}