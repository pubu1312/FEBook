using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.Models;

namespace FEBook.DAO
{
    public class SubjectDAO
    {
        private static SubjectDAO instance = null;
        private static readonly object instanceLock = new object();
        public static SubjectDAO Instance {
            get {
                lock (instanceLock) {
                    if (instance == null) {
                        instance = new SubjectDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Subject> GetSubjectList() {
            var Subjects = new List<Subject>();
            try {
                using var context = new EbookManagementContext();
                Subjects = context.Subjects.ToList();
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            //System.Console.WriteLine(Subjects.Count);
            return Subjects;
        }

        public Subject GetSubjectByID(int SubjectId) {
            Subject Subject = null;
            try {
                using var context = new EbookManagementContext();
                Subject = context.Subjects.SingleOrDefault(p => p.SubjectId == SubjectId);
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return Subject;
        }

        public void Create(Subject Subject) {
            try {
                Subject _Subject = GetSubjectByID(Subject.SubjectId);
                //ID not collapse
                if (_Subject == null) {
                    using var context = new EbookManagementContext();
                    context.Subjects.Add(Subject); 
                    context.SaveChanges();
                } else {
                    throw new Exception("The Subject is already exist.");
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void Edit(Subject Subject) {
            try {
                Subject _Subject = GetSubjectByID(Subject.SubjectId);
                if (_Subject != null) {
                    using var context = new EbookManagementContext();
                    context.Subjects.Update(Subject);
                    context.SaveChanges();
                } else {
                    throw new Exception("The Subject does not not exist.");
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}