using SearchSystem.Data.Repository;
using SearchSystem.Data.Repository.Interface;
using SearchSystem.Data.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchSystem.Data
{
    public class UnitOfWork
    {
        private DataContext _db;
        public IKeywordRepository KeywordRepository { get; set; }
        public IProfessorRepository ProfessorRepository { get; set; }
        public IThesisRepository ThesisRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        public UnitOfWork()
            : this(new DataContext())
        {
        }

        public UnitOfWork(DataContext db)
        {
            this._db = db;
            this.KeywordRepository = new KeywordRepository(this._db);
            this.ProfessorRepository = new ProfessorRepository(this._db);
            this.ThesisRepository = new ThesisRepository(this._db);
            this.DepartmentRepository = new DepartmentRepository(this._db);
        }
    }
}
