﻿using CoreDemo.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WriterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult WriterList()
        {
            var jsonWriters=JsonConvert.SerializeObject(writers);
            return Json(jsonWriters);
        }
        public IActionResult GetWriterByID(int writerid)
        {
            var findwriter=writers.FirstOrDefault(x=>x.Id== writerid);
            var jsonWriter = JsonConvert.SerializeObject(findwriter);
            return Json(jsonWriter);
        }

        [HttpPost]
        public IActionResult AddWriter(WriterClass w)
        {
            writers.Add(w);
            var jsonwriters=JsonConvert.SerializeObject(w);
            return Json(jsonwriters);
        }
        public IActionResult DeleteWriter(int id)
        {
            var writer = writers.FirstOrDefault(x => x.Id == id);
            writers.Remove(writer);
            return Json(writer);
        }
        public IActionResult UpdateWriter(WriterClass w)
        {
            var writer=writers.FirstOrDefault(x=>x.Id==w.Id);
            writer.Name=w.Name;
            var jsonwriter=JsonConvert.SerializeObject(w);
            return Json(jsonwriter);
        }

        public static List<WriterClass> writers = new List<WriterClass>
        {
            new WriterClass
            {
                Id= 1,
                Name="Ayşe"
            },
             new WriterClass
            {
                Id= 2,
                Name="Ahmet"
            },
              new WriterClass
            {
                Id= 3,
                Name="Buse"
            }
        };
    }
}
