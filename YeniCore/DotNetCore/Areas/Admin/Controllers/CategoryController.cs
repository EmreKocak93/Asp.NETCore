using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using X.PagedList.Extensions;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        CategoryManager cm=new CategoryManager(new EfCategoryRepository());
        public IActionResult Index(int page=1)
        {
            var values = cm.GetList().ToPagedList(page,3);
            return View(values);
        }
        [HttpGet]
        public IActionResult AddCategory() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category p)
        {
            CategoryValidatior cv= new CategoryValidatior();
            ValidationResult result = cv.Validate(p);
            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();



            if (result.IsValid)
            {
                p.CategoryStatus = true;
                cm.TAdd(p);
                return RedirectToAction("Index", "Category");
            }
            else
            {
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }
            ViewBag.Cv = categoryvalues;
            return View();
        }
        public IActionResult CategoryDelete(int id)
        {
            var categoryvalue = cm.TGetById(id);
            cm.TDelete(categoryvalue);
            return RedirectToAction("Index");
        }
    }
}
