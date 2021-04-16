using A1.Data;
using A1.Models;
using A1.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolCommunityContext _context;

        public StudentsController(SchoolCommunityContext context)
        {
            _context = context;
        }

        // GET: StudentsController
        public ActionResult Index(int? id)
        {
            return View(new StudentsViewModel
            {
                Students = _context.Students.ToList(),
                Selected = id.HasValue ? _context.Students.Include(s => s.Membership).ThenInclude(m => m.Community).Where(s => s.ID == id.Value).First() : null
            });
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var student = _context.Students.Where(n => n.ID == id).First();
            return View(student);
        }

        public ActionResult Create()
        {
            return View(new Student());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var student = _context.Students.Where(s => s.ID == id).First();
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            _context.Students.Update(student);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var student = _context.Students.Where(n => n.ID == id).First();
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Student student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult EditMemberships(int id)
        {
            var student = _context.Students.Include(s => s.Membership).ThenInclude(m => m.Community).Where(c => c.ID == id).First();
            var communities = _context.Communities.OrderBy(n => n.Title).ToList();
            return View(new StudentMembershipsViewModel
            {
                Student = student,
                Communities = communities
            });
        }

        [ValidateAntiForgeryToken]
        [Produces("text/html")]
        public ActionResult AddMemberships(CommunityMembership membership)
        {
            _context.CommunityMemberships.Add(membership);
            _context.SaveChanges();
            return Content("<script>history.back()</script>");
        }

        [ValidateAntiForgeryToken]
        [Produces("text/html")]
        public ActionResult RemoveMemberships(CommunityMembership membership)
        {
            _context.CommunityMemberships.Remove(membership);
            _context.SaveChanges();
            return Content("<script>history.back()</script>");
        }
    }
}
