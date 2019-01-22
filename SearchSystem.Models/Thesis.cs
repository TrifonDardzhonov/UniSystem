using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchSystem.Entities
{
    public class Thesis
    {
        private ICollection<Keyword> _keywords;
       
        public Thesis()
        {
            this._keywords = new HashSet<Keyword>();
        }

        public int ThesisId { get; set; }

        [Display(Name = "Име на темата")]
        [DataType(DataType.MultilineText)]
        public string ThesisTitle { get; set; }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string ThesisDescription { get; set; }

        [Display(Name = "Публикувана в системата")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Заетост")]
        public string Status { get; set; }

        [Display(Name = "Тип")]
        public string Type { get; set; }

        [Display(Name = "Сложност")]
        public string Complexity { get; set; }

        [Display(Name = "Име и Фамилия (студент)")]
        public string StudentName { get; set; }

        [Display(Name = "Факултетен номер (студент)")]
        public long StudentFakNo { get; set; }

        [Display(Name = "Име и Фамилия (рецензент)")]
        public string ReviewerName { get; set; }

        [Display(Name = "Защитена на")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime АwardedOn { get; set; }

        public int ProfessorId { get; set; }
        public virtual Professor Professor { get; set; }

        public virtual ICollection<Keyword> Keywords
        {
            get { return this._keywords; }
            set { this._keywords = value; }
        }
    }
}
