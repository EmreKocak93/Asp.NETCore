using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    public class RegisterController : Controller
    {
		WriterManager wm = new WriterManager(new EfWriterRepository());
		WriterValidatior wv= new WriterValidatior();
		[HttpGet]
		public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
		public IActionResult Index(Writer p)
		{
			ValidationResult result = wv.Validate(p);
            if (result.IsValid)
            {
                p.WriterStatus = true;
                p.WriterAbout = "Deneme Test";
                wm.TAdd(p);
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }
            return View();
		
		}
	}
}
