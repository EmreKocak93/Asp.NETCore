using CoreDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents
{
	public class CommentsList:ViewComponent
	{
		 public IViewComponentResult Invoke()
		{
			var commentvalues = new List<UserComment>
			{
				new UserComment
				{
					ID=1,
					UserName="Emre"
				},
					new UserComment
				{
					ID=2,
					UserName="Mesut"
				},
						new UserComment
				{
					ID=3,
					UserName="Sinan"
				},
			};
			
			return View(commentvalues);
		}
	}
}
