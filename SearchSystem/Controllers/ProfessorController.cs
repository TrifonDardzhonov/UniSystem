using SearchSystem.Data;
using SearchSystem.Entities;
using SearchSystem.Models;
using SearchSystem.Models.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchSystem.Controllers
{
    public class ProfessorController : Controller
    {
        private UnitOfWork _db;

        public ProfessorController()
        {
            this._db = new UnitOfWork();
        }

        [HttpGet]
        public ActionResult Index(int currentId, string type)
        {
            Professor professor = this._db.ProfessorRepository.GetProfessorById(currentId);

            if(professor==null)
            {
                if (type == "Next")
                {
                    int nextProfessorId = this._db.ProfessorRepository.NextOrFirstProfessor(currentId);
                    return RedirectToAction("Index", new { currentId = nextProfessorId, type = "Next" });
                }
                else
                {
                    int previousProfessorId = this._db.ProfessorRepository.PreviousOrLastProfessor(currentId);
                    return RedirectToAction("Index", new { currentId = previousProfessorId, type = "Previous" });
                }
            }
            return View(professor);
        }

        public ActionResult GetThesisDescription(int thesisId)
        {
            string fullThesisDescription = this._db.ThesisRepository.GetThesisDescription(thesisId);
            return Content(fullThesisDescription);
        }

        public ActionResult ProfessorsFromDepartment(string departmentName)
        {
            IQueryable<Professor> professors = this._db.ProfessorRepository.GetAllProfessorsWithKeywordsFromDepartment(departmentName);
            return View("~/Views/Shared/_ProfessorsListPartial.cshtml", professors);
        }

        [HttpGet]
        public ActionResult SearchProfessorsByQueryString(string queryString)
        {
            IEnumerable<Professor> professors = this._db.ProfessorRepository.SearchProfessorsByQueryString(queryString);
            return View("~/Views/Shared/_ProfessorsListPartial.cshtml", professors);
        }

	}
}