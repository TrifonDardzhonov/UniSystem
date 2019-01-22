using SearchSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SearchSystem.Data.Repository.Interface;

namespace SearchSystem.Data.Repository.Repository
{
    public class ProfessorRepository : GenericRepository<Professor>, IProfessorRepository
    {
        private DataContext _db;

        public ProfessorRepository()
            : this(new DataContext())
        {

        }

        public ProfessorRepository(DataContext db)
        {
            this._db = db;
        }

        public virtual void EditProfile(string FirstName, 
                        string LastName, 
                        string Subjects, 
                        string Education,
                        string Specialisations,
                        string AwardsAndHonors,  
                        string Certifications, 
                        string Patents,
                        string Publications,
                        string ProfesionalInterests, 
                        string OfficeHours, 
                        string Phone, 
                        string Email, 
                        int ProfessorId)
        {
            Professor professor = _db.Professor.Find(ProfessorId);

                professor.FirstName = FirstName;
                professor.LastName = LastName;
                professor.Subjects = Subjects;
                professor.Education = Education;
                professor.Specialisations = Specialisations;
                professor.AwardsAndHonors = AwardsAndHonors;
                professor.Certifications = Certifications;
                professor.Patents = Patents;
                professor.Publications = Publications;
                professor.ProfesionalInterests = ProfesionalInterests;
                professor.OfficeHours = OfficeHours;
                professor.Phone = Phone;
                professor.Email = Email;

            _db.SaveChanges();
        }

        //Get Professors
        public virtual Professor GetProfessorById(int professorId)
        {
            return _db.Professor.Include(prof => prof.Theses)
                                .SingleOrDefault(prof => prof.ProfessorId == professorId);
        }

        public virtual Professor GetProfessorByUserId(string userId)
        {
            return _db.Professor.SingleOrDefault(prof => prof.UserId == userId);
        }

        public virtual int GetProfessorIDByUserId(string userId)
        {
            return _db.Professor.Where(prof => prof.UserId == userId)
                                .Select(prof => prof.ProfessorId)
                                .SingleOrDefault();
        }

        public virtual IQueryable<Professor> GetAllProfessorsWithKeywordsFromDepartment(string departmentName)
        {
            return _db.Professor.AsNoTracking()
                                .Include(prof => prof.Keywords)
                                .Where(prof => prof.DepartmentName == departmentName);
        }

        //Professor Thesis
        public virtual IEnumerable<Thesis> GetProfessorTheses(int professorId)
        {
            return _db.Professor.AsNoTracking()
                                .Where(prof => prof.ProfessorId == professorId)
                                .SelectMany(prof => prof.Theses)
                                .ToList();
        }

        //Count
        public virtual int GetProfessorsCountFromDepartment(string departmentName)
        {
            return _db.Professor.Where(prof => prof.DepartmentName == departmentName)
                                .Count();
        }

        //Professor Keywords
        public virtual void AddKWToProfessor(Professor professor, IEnumerable<int> keywordIds)
        {
            //Dobavqme vsqka duma kam profila na prepodavatelq
            foreach (var keywordID in keywordIds)
            {
                Keyword keyword = _db.Keyword.Find(keywordID);
                professor.Keywords.Add(keyword);
            }
            _db.SaveChanges();
        }

        //Scroll between professors
        public virtual int NextOrFirstProfessor(int currentProfessorId)
        {
            IQueryable<Professor> professors = _db.Professor.Where(prof => prof.ProfessorId > currentProfessorId);
            if (professors.Any())
            {
                return professors.First().ProfessorId;
            }
            else
            {
                return _db.Professor.Min(prof => prof.ProfessorId);
            }
        }

        public virtual int PreviousOrLastProfessor(int currentProfessorId)
        {
            IQueryable<Professor> professors = _db.Professor.Where(prof => prof.ProfessorId < currentProfessorId);
            if (professors.Any())
            {
                return professors.First().ProfessorId;
            }
            else
            {
                return _db.Professor.Max(prof => prof.ProfessorId);
            }
        }

        //Department
        public virtual int GetDepartmentIdByProfessorUserId(string userId)
        {
            Professor professor = GetProfessorByUserId(userId);
            return _db.Department.Where(dep => dep.Name == professor.DepartmentName)
                                 .Single()
                                 .DepartmentId;
        }

        public IEnumerable<int> GetProfessorKeywordsIds(int professorId)
        {
            return _db.Professor.Where(prof => prof.ProfessorId == professorId)
                                .SelectMany(prof => prof.Keywords)
                                .Select(keyword => keyword.KeywordId)
                                .ToList();
        }

        public void ClearKeywords(Professor professor)
        {
            professor.Keywords.Clear();
        }

        public IEnumerable<Professor> SearchProfessorsByQueryString(string queryString)
        {
            IEnumerable<Professor> professors = this.DbSet.AsNoTracking().Where(prof => prof.FirstName.Contains(queryString) ||
                                                                                      prof.LastName.Contains(queryString) ||
                                                                                      prof.Subjects.Contains(queryString) ||
                                                                                      prof.Education.Contains(queryString) ||
                                                                                      prof.ProfesionalInterests.Contains(queryString) ||
                                                                                      prof.Email.Contains(queryString) ||
                                                                                      prof.OfficeHours.Contains(queryString));

            return professors;
        }
    }
}
