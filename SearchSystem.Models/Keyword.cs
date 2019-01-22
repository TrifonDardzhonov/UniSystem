using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchSystem.Entities
{
    public class Keyword
    {
        private ICollection<Professor> _professors; 
        private ICollection<Thesis> _theses;

        public Keyword()
        {
            this._professors = new HashSet<Professor>();
            this._theses = new HashSet<Thesis>();
        }

        public int KeywordId { get; set; }
        public string Value { get; set; }

        public int FieldId { get; set; }
        public virtual Field Field { get; set; }

        public virtual ICollection<Professor> Professors
        {
            get { return _professors; }
            set { this._professors = value; }
        }
        public virtual ICollection<Thesis> Theses
        {
            get { return _theses; }
            set { this._theses = value; }
        }
    }
}
