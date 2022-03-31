using AdminInterface.Data;
using AdminInterface.Models;
using LazZiya.ImageResize;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Net.Http;

namespace AdminInterface.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;       // Provide info about the web hosting

        public ProjectsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }

        [Route("[controller]/index")]
        public IActionResult Index()
        {
            Project project = new Project();        // Create object of Project class

            // Fetch data from API
            HttpResponseMessage? responseTask = GlobalVariables.client.GetAsync("projects").Result;

            if (responseTask == null)           // check response
            {
                return NotFound();
            }

            // Store result value of responsTask
            var result = responseTask.Content.ReadFromJsonAsync<List<Project>>().Result;

            return View(result);
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)         // error if it´s not founds
            {
                return NotFound();
            }
            else
            {
                // Call Get method from URI link and return a project
                var responseTask = GlobalVariables.client.GetAsync("projects/" + id.ToString()).Result;
                return View(responseTask.Content.ReadAsAsync<Project>().Result);
            }
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Url,ImageFile,CreatedBy,CreateAt")] Project project)
        {
            // Just show admin's namn
            project.CreatedBy = User.Identity.Name.Split("@")[0];

            // Check if input value is valid
            if (ModelState.IsValid)
            {
                // If image exists
                if (project.ImageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;                                      // Get absolute avalue of root path
                    string fileName = Path.GetFileNameWithoutExtension(project.ImageFile.FileName);         //Get file just filename
                    string extension = Path.GetExtension(project.ImageFile.FileName);                       // Get the fime extension
                    fileName = fileName + DateTime.Now.ToString("yyyyMMddssff") + extension;

                    // Output paht
                    string path = Path.Combine(wwwRootPath + "/images/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))          //Create an instans of file
                    {
                        await project.ImageFile.CopyToAsync(fileStream);                    // Copy content of uploaded file to fileStream
                    }

                    // Set file namne
                    project.ImageName = fileName;

                    //// Mask image
                    //CreateNewImg(fileName);
                    // Resize image
                    CreateImageFile(fileName);
                }

                var responseTask = GlobalVariables.client.PostAsJsonAsync("projects", project).Result;
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }
        // Mssk image
        private void CreateNewImg(string fileName)
        {

            string wwwRootPath = _hostEnvironment.WebRootPath;
            using (var img = Image.FromFile(wwwRootPath + "/images/" + fileName))
            {
                img.Mask(new Rectangle(0, 0, 300, 300), ImageFrameShape.Ellipse)
                .SaveAs(Path.Combine(wwwRootPath + "/images/thumb_" + fileName));

            }

        }
        // Resize image
        private void CreateImageFile(string fileName)
        {

            string wwwRootPath = _hostEnvironment.WebRootPath;

            using (var img = Image.FromFile(Path.Combine(wwwRootPath + "/images/" + fileName)))
            {
                img.Scale(600, 350).SaveAs(Path.Combine(wwwRootPath + "/images/thumb_" + fileName));
            }
        }

        // GET: Projects/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            Project project = new Project();
            if (id == null)         // error if it´s not founds
            {
                return NotFound();
            }
            else
            {
                // Call Get method from URI link and return a job
                var responseTask = GlobalVariables.client.GetAsync("projects/" + id.ToString()).Result;
                return View(responseTask.Content.ReadAsAsync<Project>().Result);
            }
        }

        // POST: Edit an item

        [HttpPost, ActionName("Edit")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Url,ImageFile,CreatedBy,CreateAt")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // If image exists
                if (project.ImageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;                                      // Get absolute avalue of root path
                    string fileName = Path.GetFileNameWithoutExtension(project.ImageFile.FileName);         //Get file just filename
                    string extension = Path.GetExtension(project.ImageFile.FileName);                       // Get the fime extension
                    fileName = fileName + DateTime.Now.ToString("yyyyMMddssff") + extension;

                    // Output paht
                    string path = Path.Combine(wwwRootPath + "/images/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))          //Create an instans of file
                    {
                        await project.ImageFile.CopyToAsync(fileStream);                    // Copy content of uploaded file to fileStream
                    }

                    Project prj = new Project()
                    {
                        Id = id,
                        Name = project.Name,
                        Url = project.Url,
                        ImageName = fileName,
                        Description = project.Description
                    };
                    // Set file namne

                    // Resize image
                    CreateImageFile(fileName);
                }

                var responsTask = GlobalVariables.client.PutAsJsonAsync<Project>("projects/" + project.Id, project).Result;
                if (responsTask.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            else
            {
                return NotFound();
            }
            return NotFound();
        }

        // GET: delete item in detail
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)         // error if it´s not founds
            {
                return NotFound();
            }
            else
            {
                // Call Get method from URI link and return a course
                var responseTask = GlobalVariables.client.GetAsync("projects/" + id.ToString()).Result;
                return View(responseTask.Content.ReadAsAsync<Project>().Result);
            }
        }

        // POST: Projects/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var responseTask = GlobalVariables.client.DeleteAsync("projects/" + id.ToString()).Result;
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}