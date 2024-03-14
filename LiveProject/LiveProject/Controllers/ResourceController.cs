using LiveProject.Models;
using LiveProject.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiveProject.Controllers
{
    public class ResourceController : Controller
    {
        public GenericUnitOfWork _unitofWork = new GenericUnitOfWork();

        public List<SelectListItem> Gettechnology()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitofWork.GetRepositoryInstance<technologyGroup>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.techGroupId.ToString(), Text = item.techGroupName });
            }
            return list;
        }

        public List<SelectListItem> Gettechnologi()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitofWork.GetRepositoryInstance<Technology>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.techId.ToString(), Text = item.techName });
            }
            return list;
        }


        public List<SelectListItem> GetRole()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitofWork.GetRepositoryInstance<Role>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.roleId.ToString(), Text = item.roleName });
            }
            return list;
        }

        public List<SelectListItem> GetDesignation()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitofWork.GetRepositoryInstance<Designation>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.designId.ToString(), Text = item.desigName });
            }
            return list;
        }

        public ActionResult Resource()
        {
            return View(_unitofWork.GetRepositoryInstance<Resource>().GetProduct());
        }


        [HttpGet]
        public ActionResult ResourceAdd()
        {
            ViewBag.technologyList = Gettechnology();
            ViewBag.roleList = GetRole();
            ViewBag.designationList = GetDesignation();
            ViewBag.technologiesList = Gettechnologi();
            ViewBag.technologiList = Gettechnologi();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResourceAdd(Resource resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException("Some Method received a null argument!");
            }
            else
            {
                ViewBag.technologyList = Gettechnology();
                ViewBag.roleList = GetRole();
                ViewBag.designationList = GetDesignation();
                ViewBag.technologiesList = Gettechnologi();
                ViewBag.technologiList = Gettechnologi();
                _unitofWork.GetRepositoryInstance<Resource>().Add(resource);
                return RedirectToAction("Resource");
            }
           
        }


        //Edit Project
        public ActionResult ResourceEdit(int id)
        {
            ViewBag.technologyList = Gettechnology();
            ViewBag.roleList = GetRole();
            ViewBag.designationList = GetDesignation();
            ViewBag.technologiesList = Gettechnologi();
            ViewBag.technologiList = Gettechnologi();
            return View(_unitofWork.GetRepositoryInstance<Resource>().GetFirstorDefault(id));
        }
        [HttpPost]

        public ActionResult ResourceEdit(Resource resource)
        {
            _unitofWork.GetRepositoryInstance<Resource>().Update(resource);
            return RedirectToAction("Resource");
        }

        public ActionResult ResourceDelete(int id)
        {
            var resource = _unitofWork.GetRepositoryInstance<Resource>().GetFirstorDefault(id);
            if (resource != null)
            {
                _unitofWork.GetRepositoryInstance<Resource>().Delete(resource);
                return RedirectToAction("Resource");
            }
            else
            {

                return RedirectToAction("Resource");
            }
        }

        public ActionResult allocation()
        {
            return View(_unitofWork.GetRepositoryInstance<Resource>().GetProduct());

        }
    }
}