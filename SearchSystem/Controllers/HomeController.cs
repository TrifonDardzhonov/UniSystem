using SearchSystem.Data;
using SearchSystem.Data.Models.Statistics;
using SearchSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchSystem.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork _db;

        public HomeController()
        {
            this._db = new UnitOfWork();
        }

        public ActionResult Index()
        {
            StatisticsViewModel model = new StatisticsViewModel();
            model.Departments = _db.DepartmentRepository.GetDepartmentsStatistics();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}