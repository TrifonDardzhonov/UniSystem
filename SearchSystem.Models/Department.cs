using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchSystem.Entities
{
    public class Department
    {
        private ICollection<Field> _fields;

        public Department()
        {
            this._fields = new HashSet<Field>();
        }

        public int DepartmentId { get; set; }

        [Display(Name = "Катедра")]
        public string Name { get; set; }

        public virtual ICollection<Field> Fields
        {
            get { return _fields; }
            set { this._fields = value; }
        }
    }
}
