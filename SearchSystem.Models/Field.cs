using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchSystem.Entities
{
    public class Field
    {
        private ICollection<Keyword> _keywords;

        public Field()
        {
            this._keywords = new HashSet<Keyword>();
        }

        public int FieldId { get; set; }
        public string FieldOfStudy { get; set; }

        public virtual Field ParentField { get; set; } 

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<Keyword> Keywords
        {
            get { return _keywords; }
            set { this._keywords = value; }
        }
    }
}
