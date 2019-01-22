using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchSystem.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SearchSystem.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public IDbSet<Professor> Professor { get; set; }
        public IDbSet<Thesis> Thesis { get; set; }
        public IDbSet<Keyword> Keyword { get; set; }
        public IDbSet<Field> Field { get; set; }
        public IDbSet<Department> Department { get; set; }
    }
}
