using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace CoreDemo.Controllers
{
	[AllowAnonymous]
	public class WriterController : Controller
	{
		WriterManager wm = new WriterManager(new EfWriterRepository());
        UserManager userManager = new UserManager(new EfUserRepository());

        private readonly UserManager<AppUser> _userManager;

        public WriterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

		public IActionResult Index()
		{
			var username = User.Identity.Name;
			Context c=new Context();
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var nameSurname = c.Users.Where(x => x.UserName == username).Select(y => y.NameSurname).FirstOrDefault();
            return View();
		}
	
		public IActionResult Test() 
		{ 
			return View();
		}
		
		public PartialViewResult WriterNavBarPartial()
		{
			return PartialView();
		}
        
        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }
		
		[HttpGet]
		//Burada verileri getirmek için 2 yöntem kullanıldı.Biri normal yöntem diğeri Identity kullanarak.
		//Identity kullanırken async ve Task kullanıldı.
		public async Task<IActionResult> WriterEditProfile() 
		{
		
			var values = await _userManager.FindByNameAsync(User.Identity.Name);
			UserUpdateViewModel model=new UserUpdateViewModel();
            model.mail = values.Email;
            model.namesurname = values.NameSurname;
            model.imageurl = values.ImageUrl;
			model.username = values.UserName;
            return View(model);
		}
        
        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(UserUpdateViewModel model)
        {
			var values = await _userManager.FindByNameAsync(User.Identity.Name);
			values.NameSurname = model.namesurname;
			values.ImageUrl = model.imageurl;
			values.Email = model.mail;
			values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, model.password);
			var result=await _userManager.UpdateAsync(values);
			return RedirectToAction("Index", "Dashboard");
        }
		
		[HttpGet]
		public ActionResult WriterAdd()
		{
			return View();
		}
       
        [HttpPost]
        public ActionResult WriterAdd(AddProfileImage p)
        {
			Writer w=new Writer();
			if (p.WriterImage != null) 
			{
				var extension = Path.GetExtension(p.WriterImage.FileName);
				var newimagename = Guid.NewGuid()+ extension;
				var location=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/WriterImageFiles", newimagename);
				var stream=new FileStream(location, FileMode.Create);
				p.WriterImage.CopyTo(stream);
				w.WriterImage = newimagename;
			}
			w.WriterMail= p.WriterMail;
			w.WriterStatus= true;
			w.WriterName= p.WriterName;
			w.WriterPassword= p.WriterPassword;
			w.WriterAbout= p.WriterAbout;
			wm.TAdd(w);
			return RedirectToAction("Index", "Dashboard");
        }
    }
}
