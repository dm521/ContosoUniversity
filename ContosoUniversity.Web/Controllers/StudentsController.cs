using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Application.Interfaces;
using Microsoft.Extensions.Options;
using ContosoUniversity.Web.ViewModels;
using ContosoUniversity.Dto;
using ContosoUniversity.Web.Data;
using System.Diagnostics;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ContosoUniversity.Web.Controllers
{
    public class StudentsController : Controller
    {

        //private readonly SchoolContext _context;

        /*public StudentsController(SchoolContext context)
        {
            _context = context;
        }*/

        #region Inject
        private IStudentService _studentService;
        private IEnrollmentService _enrollmentService;
        private ICourseService _courseService;
        #endregion

        public StudentsController(IStudentService studentService,IEnrollmentService enrollmentService,ICourseService courseService)
        {
            _studentService = studentService;
            _enrollmentService = enrollmentService;
            _courseService = courseService;
        }

        public async Task<ActionResult> Index()
        {
            var students = await _studentService.GetStudents();
            return View(students);   
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = new StudentViewModel();

            var students = await _studentService.GetStudentById(id);

            return View(students);

            
        }

        //Get Students/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetStudentById(id);

            

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

         [HttpPost,ActionName("Edit")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult>EditPost(int id,StudentDto student)
         {
            if(await _studentService.EditStudent(id,student.FirstName,student.LastName,student.EnrollmentDate))
            {
                return RedirectToAction("Index");
            }
            return Json(new { result = "error" });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentDto model)
        {
            /*Students student = new Students()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                EnrollmentDate = model.EnrollmentDate
            };*/
            var studentId = await _studentService.CreateStudent(model.FirstName, model.LastName, model.EnrollmentDate);
            if(studentId > 0)
            {
                return RedirectToAction("Index");
            }
            return Json(new { result = "error" });
        }


        //private readonly SchoolContext _context;

        /*public StudentsController(SchoolContext context)
        {
            _context = context;
        }*/

        // GET: /<controller>/
        /*public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var students = from s in _context.Students
                          select s;

            if(!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString) || s.FirstName.Contains(searchString));
            }

            switch(sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Student>.CreateAsync(students.AsNoTracking(), page ?? 1, pageSize));
        
            //return View();
            //return View(await students.AsNoTracking().ToListAsync());
        }*/

        /*
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var student = await _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if(student == null)
            {
                return NotFound();
            }

            return View(student);
        }*/

        //Get Students/Create
        /*public IActionResult Create()
        {
            return View();
        }*/

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentDate,FirstName,LastName")] Student student)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes." + "Try again, and if the problem persists" + "see your system administrator.");
            }
            return View(student);
        }*/

        //Get Students/Edit
        /*public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.SingleOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }*/

        /*[HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>EditPost(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Students.SingleOrDefaultAsync(s => s.ID == id);
            if(await TryUpdateModelAsync<Student>(studentToUpdate, "", s => s.FirstName, s=>s.LastName, s=>s.EnrollmentDate))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch(DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }*/

        //Get Students/Delte
        /*public async Task<IActionResult>Delete(int ?id, bool? saveChangesError = false)
        {
            if(id==null)
            {
                return NotFound();
            }
            var student = await _context.Students.AsNoTracking().SingleOrDefaultAsync(m => m.ID == id);

            if(student == null)
            {
                return NotFound();
            }

            if(saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }
            return View(student);
        }*/

        /*[HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>DeleteConfirmed(int id)
        {
            var student = await _context.Students.AsNoTracking().SingleOrDefaultAsync(m => m.ID == id);

            if(student == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch(DbUpdateException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }*/

    }
}
