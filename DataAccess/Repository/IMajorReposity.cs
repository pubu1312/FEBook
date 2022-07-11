using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.Models;

namespace FEBook.DataAccess.Repository
{
    public interface IMajorReposity
    {
        IEnumerable<Major> GetMajors();

        Major GetMajorByID(int MajorId);

        void CreateMajor(Major major);
        void EditMajor(Major major);
        void DeleteMajor(int major);
        
    }
}