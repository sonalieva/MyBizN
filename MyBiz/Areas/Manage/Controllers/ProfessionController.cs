using Microsoft.AspNetCore.Mvc;
using MyBiz.DAL;
using MyBiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBiz.Areas.Manage.Controllers
{
    [Area("manage")]
    public class ProfessionController : Controller
    {
        private readonly AppDbContext _context;
        public ProfessionController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = _context.Professions.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Profession profession)
        {
            if (!ModelState.IsValid)
                return View();
            if (_context.Professions.Any(x => x.Name == profession.Name))
            {
                ModelState.AddModelError("Name", "This Profession is already exists");
                return View();
            }
            _context.Professions.Add(profession);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Edit(int id,Profession profession)
        {
            Profession existProfession = _context.Professions.FirstOrDefault(x => x.Id == id);
            if (existProfession == null)
                return RedirectToAction("error", "dashboard");
            if(_context.Professions.Any(x=>x.Id != id && x.Name == profession.Name))
            {
                ModelState.AddModelError("Name", "This Profession is already exists");
                return View();
            }
            _context.Professions.Add(profession);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
