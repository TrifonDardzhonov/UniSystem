using SearchSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchSystem.Data.Repository.Interface
{
    public interface IProfessorRepository : IGenericRepository<Professor>
    {
        void EditProfile(string FirstName, 
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
                        int ProfessorId);

        Professor GetProfessorById(int professorId);

        Professor GetProfessorByUserId(string userId);

        int GetProfessorIDByUserId(string userId);

        IQueryable<Professor> GetAllProfessorsWithKeywordsFromDepartment(string departmentName);

        IEnumerable<Thesis> GetProfessorTheses(int professorId);

        int GetProfessorsCountFromDepartment(string departmentName);

        void AddKWToProfessor(Professor professor, IEnumerable<int> Keywords);
        
        int NextOrFirstProfessor(int currentProfessorId);

        int PreviousOrLastProfessor(int currentProfessorId);

        int GetDepartmentIdByProfessorUserId(string userId);

        IEnumerable<int> GetProfessorKeywordsIds(int professorId);

        void ClearKeywords(Professor professor);

        IEnumerable<Professor> SearchProfessorsByQueryString(string queryString);
    }
}
