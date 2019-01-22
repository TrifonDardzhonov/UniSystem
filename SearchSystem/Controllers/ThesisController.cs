using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SearchSystem.Models;
using SearchSystem.Data;
using SearchSystem.Entities;
using SearchSystem.Models.Constants;
using System.Collections;

namespace SearchSystem.Controllers
{

    public class ThesisController : Controller
    {
        private UnitOfWork _db;

        public ThesisController()
        {
            this._db = new UnitOfWork();
        }

        public ActionResult Index(int currentId, string type)
        {
            Thesis thesis = this._db.ThesisRepository.GetThesisByID(currentId);

            if(thesis==null)
            {
                if (type == "Next")
                {
                    int nextThesisId = this._db.ThesisRepository.NextOrFirstThesis(currentId);//Return next or first
                    return RedirectToAction("Index", new { currentId = nextThesisId, type = "Next"});
                }
                else
                {
                    int previousThesisId = this._db.ThesisRepository.PreviousOrLastThesis(currentId);//return previous or last
                    return RedirectToAction("Index", new { currentId = previousThesisId, type = "Previous" });
                }
            }

            return View(thesis);
        }
        
        public ActionResult SearchReviewers(int thesisId, int professorId)
        {
            //Get current thesis
            Thesis thesis = this._db.ThesisRepository.GetThesisByID(thesisId);

            //Get all professors from thesis department
            IEnumerable<Professor> professors = this._db.ProfessorRepository.GetAllProfessorsWithKeywordsFromDepartment(thesis.Professor.DepartmentName);
            
            //Make array for all professors from thesis department
            ProfessorRankModel[] ProfessorsRanks = new ProfessorRankModel[professors.Count()];
            for (int i = 0; i < ProfessorsRanks.Length; i++)
            {
                ProfessorsRanks[i] = new ProfessorRankModel();
            }

            int currentProfessor = 0;
            int fullMatch = 0; //full match between user keyword and professor keyword
            int diferentFromZero = 0;//full match or with same node
            double professorRank = 0.0f;

            foreach (Professor professor in professors)
            {
                foreach (Keyword professorKeyword in professor.Keywords)
                {
                    //foreach every keywords from thesis
                    foreach (Keyword thesisKeyword in thesis.Keywords)
                    {
                        //compare if they are equal (if compare return 0 they are equal)
                        if (string.Compare(professorKeyword.Value, thesisKeyword.Value) == 0)
                        {
                            professorRank += 1.0f;
                            fullMatch++;
                            diferentFromZero++;
                        }
                        //compare if they have same node
                        else if (professorKeyword.FieldId == thesisKeyword.FieldId)
                        {
                            professorRank += 0.5f;
                            diferentFromZero++;
                        }
                        //if they dont equal and they dont have same node
                        else
                        {
                            professorRank += 0.0f;
                        }
                    }
                }

                if (fullMatch == thesis.Keywords.Count() || fullMatch == professor.Keywords.Count)
                {
                    professorRank = 1.0f * 100;
                    ProfessorsRanks[currentProfessor].Rank = professorRank;
                    ProfessorsRanks[currentProfessor].ProfessorId = professor.ProfessorId;
                    ProfessorsRanks[currentProfessor].FirstName = professor.FirstName;
                    ProfessorsRanks[currentProfessor].LastName = professor.LastName;
                    ProfessorsRanks[currentProfessor].Subjects = professor.Subjects;
                }
                else
                {
                    professorRank = Math.Round((professorRank / diferentFromZero) * 100, 2); 
                    ProfessorsRanks[currentProfessor].Rank = professorRank;
                    ProfessorsRanks[currentProfessor].ProfessorId = professor.ProfessorId;
                    ProfessorsRanks[currentProfessor].FirstName = professor.FirstName;
                    ProfessorsRanks[currentProfessor].LastName = professor.LastName;
                    ProfessorsRanks[currentProfessor].Subjects = professor.Subjects;
                }

                //For every professor
                fullMatch = 0;
                professorRank = 0.0f;
                diferentFromZero = 0;
                currentProfessor++;
            }

            IEnumerable<ProfessorRankModel> sorted = ProfessorsRanks
                .OrderByDescending(o => o.Rank)
                .Where(i => i.ProfessorId != professorId)
                .Where(r => double.IsNaN(r.Rank) == false)
                .Take(ProfessorConstraints.FIRST_N_PROFESSORS);

            ViewBag.ID = thesisId;
            return View(sorted);
        }
        
        public ActionResult ThesesFromDepartmentByQuery(string departmentName, string status, string type)
        {
            IEnumerable<Thesis> theses = this._db.ThesisRepository.GetAllThesesFromDepartmentByStatusAndType(departmentName, status, type);
            return View("~/Views/Shared/_ThesesListPartial.cshtml", theses);
        }

        [HttpGet]
        public ActionResult SearchThesesByQueryString(string queryString)
        {
            IEnumerable<Thesis> theses = this._db.ThesisRepository.SearchThesesByQueryString(queryString);
            return View("~/Views/Shared/_ThesesListPartial.cshtml", theses);
        }
	}
}