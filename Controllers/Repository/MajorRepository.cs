using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.Controllers.DAO;
using FEBook.Models;

namespace FEBook.Controllers.Repository
{
    public class MajorRepository : IMajorReposity
    {
        public IEnumerable<Major> GetMajors() {
            return MajorDAO.Instance.GetMajorList();
        }

        public Major GetMajorByID(int MajorId) {
            return MajorDAO.Instance.GetMajorByID(MajorId);
        }

    }

}