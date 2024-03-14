using LiveProject.Models;
using LiveProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiveProject.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Default
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Technology>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.techId.ToString(), Text = item.techName });

            }
            return list;
        }
        public List<SelectListItem> GetResource()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Resource>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.resId.ToString(), Text = item.resName });

            }
            return list;
        }
        public ActionResult ProjectAdd()
        {
            ViewBag.Categorylist = GetCategory();
            ViewBag.Resourcelist = GetResource();
            return View();
        }
        [HttpPost]
        public ActionResult ProjectAdd(Project tbl)
        {
            if (tbl == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.GetRepositoryInstance<Project>().Add(tbl);
                _unitOfWork.SaveChanges();
                return RedirectToAction("GetAllProject");
            }
            return View();

        }
        public ActionResult GetAllProject()
        {
            ViewBag.email = Session["email"];
            return View(_unitOfWork.GetRepositoryInstance<Project>().GetProduct());
        }
    }
}