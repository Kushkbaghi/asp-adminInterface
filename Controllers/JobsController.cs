#nullable disable

using AdminInterface.Data;
using AdminInterface.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminInterface.Controllers
{
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET:all jobs
        [Authorize]
        public async Task<IActionResult> Index()
        {
            Job job = new Job();            // Create object of Job class

            var responsTask = GlobalVariables.client.GetAsync("jobs").Result;

            return View(responsTask.Content.ReadFromJsonAsync<List<Job>>().Result);
        }

        // GET: a job
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsTaslk = GlobalVariables.client.GetAsync("jobs/" + id.ToString()).Result;
            if (responsTaslk == null)
            {
                return NotFound();
            }

            return View(responsTaslk.Content.ReadFromJsonAsync<Job>().Result);
        }

        // GET: create list
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: create new job
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Place,Start,End,CreatedBy,CreateAt")] Job job)
        {
            job.CreatedBy = User.Identity.Name.Split("@")[0];
            if (ModelState.IsValid)
            {
                var responsTaslk = GlobalVariables.client.PostAsJsonAsync("jobs/", job).Result;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: update list
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsTaslk = GlobalVariables.client.GetAsync("jobs/" + id.ToString()).Result;
            if (responsTaslk == null)
            {
                return NotFound();
            }
            return View(responsTaslk.Content.ReadAsAsync<Job>().Result);
        }

        // POST: update a job
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Place,Start,End")] Job job)
        {
            if (id != job.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var responsTaslk = GlobalVariables.client.PutAsJsonAsync("jobs/" + id.ToString(), job).Result;
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        // GET: Delete item detail
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var responsTaslk = GlobalVariables.client.GetAsync("jobs/" + id.ToString()).Result;
            if (responsTaslk == null)
            {
                return NotFound();
            }

            return View(responsTaslk.Content.ReadFromJsonAsync<Job>().Result);
        }

        // POST: Jobs/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var responsTaslk = GlobalVariables.client.DeleteAsync("jobs/" + id.ToString()).Result;
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Job.Any(e => e.Id == id);
        }
    }
}