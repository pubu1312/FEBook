using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.Models;

namespace FEBook.Controllers.DAO
{
    public class MajorDAO
    {
        private static MajorDAO instance = null;
        private static readonly object instanceLock = new object();
        public static MajorDAO Instance {
            get {
                lock (instanceLock) {
                    if (instance == null) {
                        instance = new MajorDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Major> GetMajorList() {
            var Majors = new List<Major>();
            try {
                using var context = new EbookManagementContext();
                Majors = context.Majors.ToList();
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            //System.Console.WriteLine(Majors.Count);
            return Majors;
        }

        public Major GetMajorByID(int majorId) {
            Major major = null;
            try {
                using var context = new EbookManagementContext();
                major = context.Majors.SingleOrDefault(p => p.MajorId == majorId);
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return major;
        }
    }
}