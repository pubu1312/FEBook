using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.Models;

namespace FEBook.DataAccess.DAO
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

        public void Create(Major major) {
            try {
                Major _Major = GetMajorByID(major.MajorId);
                //ID not collapse
                if (_Major == null) {
                    using var context = new EbookManagementContext();
                    context.Majors.Add(major); 
                    context.SaveChanges();
                } else {
                    throw new Exception("The Major is already exist.");
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void Edit(Major major) {
            try {
                Major _Major = GetMajorByID(major.MajorId);
                if (_Major != null) {
                    using var context = new EbookManagementContext();
                    context.Majors.Update(major);
                    context.SaveChanges();
                } else {
                    throw new Exception("The Major does not not exist.");
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int majorId) {
            try {
                Major _Major = GetMajorByID(majorId);
                if (_Major != null) {
                    using var context = new EbookManagementContext();
                    context.Majors.Remove(_Major);
                    context.SaveChanges();
                } else {
                    throw new Exception("The Major does not not exist.");
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}