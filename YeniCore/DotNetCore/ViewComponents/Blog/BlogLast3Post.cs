using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Blog
{
	public class BlogLast3Post:ViewComponent
	{
		BlogManager bg=new BlogManager(new EfBlogRepository());

		public IViewComponentResult Invoke()
		{
			var values=bg.Get3LastBlog();
			return View(values);
		}
	}
}
