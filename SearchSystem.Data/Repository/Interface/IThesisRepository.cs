using SearchSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchSystem.Data.Repository.Interface
{
    public interface IThesisRepository : IGenericRepository<Thesis>
    {
        string GetThesisDescription(int thesisId);

        Thesis GetThesisByTitle(string thesisTitle);

        Thesis GetThesisByID(int ThesisId);

        List<Thesis> GetAllThesesWithKeywordsFromDepartment(string departmentName);

        IQueryable<Thesis> GetAllThesesFromDepartmentByStatusAndType(string departmentName, string status, string type); 

        int GetThesesCountFromDepartment(string departmentName);
            
        void AddKWToThesis(Thesis Thesis, IEnumerable<int> Keywords);

        int NextOrFirstThesis(int currentThesisId);

        int PreviousOrLastThesis(int currentThesisId);

        void EditThesis(string title, 
                        string description, 
                        string status,
                        string type,
                        string complexity,
                        string studentName, 
                        long studentFakNo, 
                        string reviewerName, 
                        DateTime AwardedOn, 
                        int thesisId);

        IEnumerable<int> GetThesisKeywordsIds(int thesisId);

        void ClearKeywords(Thesis thesis);

        IEnumerable<Thesis> SearchThesesByQueryString(string queryString);
    }
}
