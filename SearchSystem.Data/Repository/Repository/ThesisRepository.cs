using SearchSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchSystem.Data;
using System.Data.Entity;
using SearchSystem.Data.Repository.Interface;

namespace SearchSystem.Data.Repository.Repository
{
    public class ThesisRepository : GenericRepository<Thesis>, IThesisRepository
    {
        private DataContext _db;
  
        public ThesisRepository()
            : this(new DataContext())
        {

        }

        public ThesisRepository(DataContext db)
        {
            this._db = db;
        }

        public virtual string GetThesisDescription(int thesisId)
        {
            return _db.Thesis.Where(t => t.ThesisId == thesisId).Select(d => d.ThesisDescription).Single();
        }

        //Get Thesis
        public virtual Thesis GetThesisByTitle(string thesisTitle)
        {
            return _db.Thesis.Where(thesis => thesis.ThesisTitle.Contains(thesisTitle)).Single();
        }

        public virtual Thesis GetThesisByID(int ThesisId)
        {
            return _db.Thesis.Include(thesis => thesis.Keywords)
                             .Include(thesis => thesis.Professor)
                             .Where(thesis => thesis.ThesisId == ThesisId)
                             .SingleOrDefault();
        }

        public virtual List<Thesis> GetAllThesesWithKeywordsFromDepartment(string departmentName)
        {
            return _db.Professor.AsNoTracking()
                                .Where(prof => prof.DepartmentName == departmentName)
                                .SelectMany(prof => prof.Theses)
                                .Include(thesis => thesis.Keywords)
                                .ToList<Thesis>();
        }

        public virtual IQueryable<Thesis> GetAllThesesFromDepartmentByStatusAndType(string departmentName, string status, string type)
        {
            return _db.Thesis.AsNoTracking()
                             .Include(thesis => thesis.Professor)
                             .Where(thesis => thesis.Professor.DepartmentName == departmentName &&
                                              thesis.Status == status &&
                                              thesis.Type == type);
        }
        

        public virtual int GetThesesCountFromDepartment(string departmentName)
        {
            return _db.Professor.Where(prof => prof.DepartmentName == departmentName)
                                .SelectMany(prof => prof.Theses)
                                .Count();
        }

        //Edit Thesis
        public virtual void EditThesis(string title, 
                        string description, 
                        string status,
                        string type,
                        string complexity,
                        string studentName, 
                        long studentFakNo, 
                        string reviewerName, 
                        DateTime AwardedOn, 
                        int thesisId)
        {
                Thesis EditedThesis = GetThesisByID(thesisId);

                EditedThesis.ThesisTitle = title;
                EditedThesis.ThesisDescription = description;
                EditedThesis.Status = status;
                EditedThesis.Type = type;
                EditedThesis.Complexity = complexity;
                EditedThesis.StudentName = studentName;
                EditedThesis.StudentFakNo = studentFakNo;
                EditedThesis.ReviewerName = reviewerName;
                EditedThesis.АwardedOn = AwardedOn;

            _db.SaveChanges();
        }

        //Thesis Keywords
        public virtual void AddKWToThesis(Thesis Thesis, IEnumerable<int> Keywords)
        {
            //Dobavqme vsqka duma kam profila na temata
            foreach (var keywordID in Keywords)
            {
                Keyword keyword = _db.Keyword.Find(keywordID);
                Thesis.Keywords.Add(keyword);
            }
            _db.SaveChanges();
        }

        //Scroll between theses
        public virtual int NextOrFirstThesis(int currentThesisId)
        {
            IQueryable<Thesis> theses = _db.Thesis.Where(th => th.ThesisId > currentThesisId);
            if (theses.Any())
            {
                return theses.First().ThesisId;
            }
            else
            {
                return _db.Thesis.Min(th => th.ThesisId);
            }
        }

        public virtual int PreviousOrLastThesis(int currentThesisId)
        {
            IQueryable<Thesis> theses = _db.Thesis.Where(th => th.ThesisId < currentThesisId);
            if (theses.Any())
            {
                return theses.First().ThesisId;
            }
            else
            {
                return _db.Thesis.Max(th=>th.ThesisId);
            }
        }

        public IEnumerable<int> GetThesisKeywordsIds(int thesisId)
        {
            return _db.Thesis.Where(thesis => thesis.ThesisId == thesisId)
                             .SelectMany(thesis => thesis.Keywords)
                             .Select(keyword => keyword.KeywordId)
                             .ToList();
        }

        public void ClearKeywords(Thesis thesis)
        {
            thesis.Keywords.Clear();
        }

        public IEnumerable<Thesis> SearchThesesByQueryString(string queryString)
        {
            IEnumerable<Thesis> theses = this.DbSet.AsNoTracking().Include(t => t.Professor)
                                                   .Where(t => t.ThesisTitle.Contains(queryString) ||
                                                            t.ThesisDescription.Contains(queryString) ||
                                                            t.Type.Contains(queryString) ||
                                                            t.Complexity.Contains(queryString) ||
                                                            t.StudentName.Contains(queryString) ||
                                                            t.ReviewerName.Contains(queryString) ||
                                                            t.Professor.FirstName.Contains(queryString) ||
                                                            t.Professor.LastName.Contains(queryString));

            return theses;
        }
    }
}
