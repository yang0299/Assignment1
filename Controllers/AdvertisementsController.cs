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
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace A1.Controllers
{
    public class AdvertisementsController : Controller
    {
        private readonly SchoolCommunityContext _context;
        private readonly BlobServiceClient _blobClient;
        private readonly string blobContainerName = "communityadvs";

        public AdvertisementsController(SchoolCommunityContext context, BlobServiceClient blobClient)
        {
            _context = context;
            _blobClient = blobClient;
        }

        [HttpGet]
        public ActionResult Index(string id)
        {
            return View(_context.Communities.Include(c => c.Advertisements).First(c => c.ID == id));
        }

        [HttpGet]
        public ActionResult Create(string id)
        {
            return View(_context.Communities.First(c => c.ID == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "invalid image file");
                return View(_context.Communities.First(c => c.ID == id));
            }

            var blobKey = Guid.NewGuid().ToString();

            var container = _blobClient.GetBlobContainerClient(this.blobContainerName);
            container.CreateIfNotExists(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
            var client = container.GetBlobClient(blobKey);
            client.Upload(file.OpenReadStream(), new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                }
            });
            var adv = new Advertisement
            {
                BlobKey = blobKey,
                Url = client.Uri.AbsoluteUri,
                FileName = file.FileName,
                Community = _context.Communities.First(c => c.ID == id)
            };
            _context.Advertisements.Add(adv);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { id = id });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var advertisement = _context.Advertisements.Include(c => c.Community).Where(n => n.ID == id).First();
            return View(advertisement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Advertisement arg)
        {
            var advertisement = _context.Advertisements.Include(v=>v.Community).First(n => n.ID == arg.ID);

            var client = _blobClient.GetBlobContainerClient(blobContainerName);
            client.DeleteBlobIfExists(advertisement.BlobKey);

            var communityId = advertisement.Community.ID;
            _context.Advertisements.Remove(advertisement);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index),new { id = communityId });
        }
    }
}
