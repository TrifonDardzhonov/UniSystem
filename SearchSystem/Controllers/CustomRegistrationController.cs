using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    [Authorize]
    public class CustomRegistrationController : Controller
    {
        private UnitOfWork _db;

        public CustomRegistrationController()
        {
            this._db = new UnitOfWork();
        }

        public ActionResult Index()
        {
            return View();
        }

        //Choose from witch department is logged professor
        public ActionResult ChooseDepartment()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegisterProfessor(string departmentName)
        {
            HttpContext.Session.Add("departmentId", this._db.DepartmentRepository.GetDepartmentIdByName(departmentName));
            ViewBag.departmentName = departmentName;//TO put it in professor

            ProfessorWithKeywordsViewModel professor = new ProfessorWithKeywordsViewModel();
            professor.Keywords = this._db.KeywordRepository.GetAllKeywordsFromDepartment((int)HttpContext.Session["departmentId"]);

            return View(professor);
        }

        [HttpPost]
        public ActionResult RegisterProfessor(ProfessorWithKeywordsViewModel model)
        {
            if (ModelState.IsValid && model.CheckboxList.Count() > 0)
            {
                Professor professor = model.Professor.ConvertToNewProfessor(User.Identity.GetUserId());
                this._db.ProfessorRepository.Add(professor);
                this._db.ProfessorRepository.SaveChanges();

                //After we create new professor we get him and add keywords to his profile
                this._db.ProfessorRepository.AddKWToProfessor((Professor)this._db.ProfessorRepository.GetProfessorByUserId(User.Identity.GetUserId()), model.CheckboxList);

                //Add role 'Professor' to professors
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
                roleManager.Create(new IdentityRole("Professor"));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
                userManager.AddToRole(User.Identity.GetUserId(), "Professor");

                return RedirectToAction("ProfessorProfile", "ProfessorProfile");
            }
            model.Keywords = this._db.KeywordRepository.GetAllKeywordsFromDepartment((int)HttpContext.Session["departmentId"]);
            ViewBag.SelectedKeywords = model.CheckboxList;
            ViewBag.departmentName = model.Professor.DepartmentName;
        
            return View(model);
        }
	}
}