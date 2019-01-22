using SearchSystem.Data.Repository.Interface;
using SearchSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SearchSystem.Data.Repository.Repository
{
    public class KeywordRepository : GenericRepository<Keyword>, IKeywordRepository
    {
        private DataContext _db;
        
        public KeywordRepository()
            : this(new DataContext())
        {

        }

        public KeywordRepository(DataContext db)
        {
            this._db = db;
        }

        public virtual IList<Keyword> GetAllKeywordsFromDepartment(int departmentId)
        {

            List<Keyword> keywords = _db.Keyword.AsNoTracking()
                                                .Include(keyword=>keyword.Field)
                                                .Where(keyword => keyword.Field.DepartmentId == departmentId)
                                                .ToList<Keyword>();

            return keywords;
        }

        //Vzimame ime i FieldId na dumite vavedeni ot potrebitelq ili ot diplomnata tema
        public virtual IQueryable<KeywordBasicModel> GetUserKeywordsAndFields(IEnumerable<int> Keywords)
        {
            //S toq group by selektirame samo disctinct stoinostite za da nqma povtorenie
            IQueryable<KeywordBasicModel> UserKeywords = (
                from K in _db.Keyword
                where Keywords.Contains(K.KeywordId)
                group K by new { K.FieldId, K.Value } into KeyWord
                select new KeywordBasicModel { FieldId = KeyWord.Key.FieldId, Value = KeyWord.Key.Value })
                .Distinct()
                .AsQueryable();

            return UserKeywords;
        }
    }
}
