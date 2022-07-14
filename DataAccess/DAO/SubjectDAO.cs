using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBook.DataAccess.Repository;
using FEBook.Models;

namespace FEBook.DataAccess.DAO
{
    public class SubjectDAO
    {
        private static SubjectDAO instance = null;
        private static readonly object instanceLock = new object();
        public static SubjectDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SubjectDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Subject> GetSubjectList()
        {
            var Subjects = new List<Subject>();
            try
            {
                using var context = new EbookManagementContext();
                Subjects = context.Subjects.Where(s => s.DeleteStatus == false).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //System.Console.WriteLine(Subjects.Count);
            return Subjects;
        }

        

        public IEnumerable<Subject> GetSubjectDeletedList()
        {
            var Subjects = new List<Subject>();
            try
            {
                using var context = new EbookManagementContext();
                Subjects = context.Subjects.Where(s => s.DeleteStatus == true).ToList();
                return Subjects;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Subject GetSubjectByID(int SubjectId)
        {
            Subject Subject = null;
            try
            {
                using var context = new EbookManagementContext();
                Subject = context.Subjects.SingleOrDefault(p => p.SubjectId == SubjectId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Subject;
        }

        public void Create(Subject Subject)
        {
            try
            {
                Subject _Subject = GetSubjectByID(Subject.SubjectId);
                //ID not collapse
                if (_Subject == null)
                {
                    using var context = new EbookManagementContext();
                    context.Subjects.Add(Subject);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Subject is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Edit(Subject subject)
        {
            try
            {
                Subject _Subject = GetSubjectByID(subject.SubjectId);
                if (_Subject != null)
                {
                    using var context = new EbookManagementContext();
                    context.Subjects.Update(subject);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Subject does not not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int SubjectId)
        {
            try
            {
                Subject _Subject = GetSubjectByID(SubjectId);
                if (_Subject != null)
                {
                    using var context = new EbookManagementContext();
                    context.Subjects.Remove(_Subject);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Subject does not not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOnce(int subjectId){
            try{
                Subject _Subject = GetSubjectByID(subjectId);
                if (_Subject != null)
                {
                    using var context = new EbookManagementContext();
                    _Subject.DeleteStatus=true;
                    context.Subjects.Update(_Subject);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Subject does not not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}