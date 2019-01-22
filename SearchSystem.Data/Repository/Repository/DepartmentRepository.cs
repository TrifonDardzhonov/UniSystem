using SearchSystem.Data.Models.Statistics;
using SearchSystem.Data.Repository.Interface;
using System.Data.Entity;
using SearchSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchSystem.Data.Enums;

namespace SearchSystem.Data.Repository.Repository
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private DataContext _db;
    
        public DepartmentRepository()
            : this(new DataContext())
        {

        }

        public DepartmentRepository(DataContext db)
        {
            this._db = db;
        }

        public virtual int GetDepartmentIdByName(string departmentName)
        {
            return _db.Department.Where(dep => dep.Name == departmentName)
                                 .Single().DepartmentId;
        }

        public virtual IEnumerable<BasicStatisticsViewModel> GetDepartmentsStatistics()
        {
           return _db.Professor
                        .AsNoTracking()
                        .Include(prof=>prof.Theses)
                        .GroupBy(prof => prof.DepartmentName)
                        .Select(profg => new BasicStatisticsViewModel
                        {
                            DepartmentName = profg.Key,

                            ProfessorsCount = profg.Count(),

                            FreeBachelorThesesCount = profg.Sum(t => t.Theses.Where(th => th.Status == ThesisStatusEnum.Free &&
                                                                                          th.Type == ThesisTypeEnum.Bachelor).Count()),

                            BusyBachelorThesesCount = profg.Sum(t => t.Theses.Where(th => th.Status == ThesisStatusEnum.Busy &&
                                                                                          th.Type == ThesisTypeEnum.Bachelor).Count()),

                            AwardedBachelorThesesCount = profg.Sum(t => t.Theses.Where(th => th.Status == ThesisStatusEnum.Awarded &&
                                                                                             th.Type == ThesisTypeEnum.Bachelor).Count()),

                            FreeMasterThesesCount = profg.Sum(t => t.Theses.Where(th => th.Status == ThesisStatusEnum.Free &&
                                                                                        th.Type == ThesisTypeEnum.Master).Count()),

                            BusyMasterThesesCount = profg.Sum(t => t.Theses.Where(th => th.Status == ThesisStatusEnum.Busy &&
                                                                                        th.Type == ThesisTypeEnum.Master).Count()),

                            AwardedMasterThesesCount = profg.Sum(t => t.Theses.Where(th => th.Status == ThesisStatusEnum.Awarded &&
                                                                                           th.Type == ThesisTypeEnum.Master).Count()),
                        }).ToList<BasicStatisticsViewModel>();
        }
    }
}
