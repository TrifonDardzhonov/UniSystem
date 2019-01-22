using SearchSystem.Data.Models.Statistics;
using SearchSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchSystem.Data.Repository.Interface
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        int GetDepartmentIdByName(string departmentName);

        IEnumerable<BasicStatisticsViewModel> GetDepartmentsStatistics(); 
    }
}
