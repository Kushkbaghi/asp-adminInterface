using AdminInterface.Data;
using AdminInterface.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminInterface.Controllers
{
    public class CoursesController : Controller
    {
        // Link database to the models
        private readonly ApplicationDbContext _conext;

        public CoursesController(ApplicationDbContext context)
        {
            _conext = context;
        }

        // GET: all courses
        [Authorize]
        public async Task<IActionResult> Index()
        {
            Course course = new Course();           // Create object of Course class
            var responsTask = GlobalVariables.client.GetAsync("courses").Result;

            return View(responsTask.Content.ReadFromJsonAsync<List<Course>>().Result);
        }

        // GET: a course
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var responsTask = GlobalVariables.client.GetAsync("courses/" + id.ToString()).Result;

            return View(responsTask.Content.ReadAsAsync<Course>().Result);
        }

        // GET: create new course (Form)
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: CoursesController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Place, Name,Prograssion,Start,End,CreatedBy,CreateAt")] Course course)
        {
            course.CreatedBy = User.Identity.Name.Split("@")[0];
            try
            {
                if (ModelState.IsValid)
                {
                    var responsTask = GlobalVariables.client.PostAsJsonAsync<Course>("courses", course).Result;
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(course);
            }
        }

        // GET: Update list
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var responsTask = GlobalVariables.client.GetAsync("courses/" + id.ToString()).Result;
            return View(responsTask.Content.ReadAsAsync<Course>().Result);
        }

        // POST: Update
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Place, Name,Prograssion,Start,End,CreatedBy,CreateAt")] Course course)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var respons = GlobalVariables.client.PutAsJsonAsync("courses/" + id.ToString(), course).Result;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CoursesController/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var responsTask = GlobalVariables.client.GetAsync("courses/" + id.ToString()).Result;
            return View(responsTask.Content.ReadAsAsync<Course>().Result);
        }

        // POST: CoursesController/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var responsTask = GlobalVariables.client.DeleteAsync("courses/" + id.ToString()).Result;
            return RedirectToAction(nameof(Index));
        }
    }
}