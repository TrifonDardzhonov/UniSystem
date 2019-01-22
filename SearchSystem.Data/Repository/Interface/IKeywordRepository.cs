using SearchSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchSystem.Data.Repository.Interface
{
    public interface IKeywordRepository : IGenericRepository<Keyword>
    {
        IList<Keyword> GetAllKeywordsFromDepartment(int departmentId);

        IQueryable<KeywordBasicModel> GetUserKeywordsAndFields(IEnumerable<int> Keywords);
    }
}
