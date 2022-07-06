using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.DAO;
using FEBook.Models;

namespace FEBook.Controllers.Repository
{
    public class MajorRepository : IMajorReposity
    {
        public IEnumerable<Major> GetMajors() {
            return DAO.MajorDAO.Instance.GetMajorList();
        }

        public Major GetMajorByID(int MajorId) {
            return DAO.MajorDAO.Instance.GetMajorByID(MajorId);
        }

        public void CreateMajor(Major major) {
            DAO.MajorDAO.Instance.Create(major);
        }

        public void EditMajor(Major major) {
            DAO.MajorDAO.Instance.Edit(major);
        }

    }

}