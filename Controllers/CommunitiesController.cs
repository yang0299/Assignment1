using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A1.Data;
using A1.Models;
using A1.ViewModels;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace A1.Controllers
{
    public class CommunitiesController : Controller
    {
        private readonly SchoolCommunityContext _context;

        public CommunitiesController(SchoolCommunityContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index(string id)
        {
            return View(new CommunitiesViewModel
            {
                Communities = _context.Communities.ToList(),
                Selected = string.IsNullOrEmpty(id) ? null : _context.Communities.Include(c => c.Membership).ThenInclude(m => m.Student).First(n => n.ID == id)
            });
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var community = _context.Communities.First(n => n.ID == id);
            return View(community);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Community());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Community community)
        {
            var idExists = _context.Communities.Any(n => n.ID == community.ID);
            if (idExists)
            {
                ModelState.AddModelError(nameof(community.ID), "Registration Number already exists");
            }
            var titleExists = _context.Communities.Any(n => n.Title == community.Title);
            if (titleExists)
            {
                ModelState.AddModelError(nameof(community.Title), "Title already exists");
            }
            if (!ModelState.IsValid)
                return View(community);

            _context.Communities.Add(community);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var community = _context.Communities.First(n => n.ID == id);
            return View(community);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Community community)
        {
            if (!ModelState.IsValid)
                return View(community);

            _context.Communities.Update(community);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var community = _context.Communities.Include(c=>c.Advertisements).Where(n => n.ID == id).First();
            return View(community);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Community community)
        {
            _context.Communities.Remove(community);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
