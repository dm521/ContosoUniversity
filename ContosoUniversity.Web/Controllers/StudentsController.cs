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
            var studentId = 0;
            if (ModelState.IsValid)
            {
                studentId = await _studentService.CreateStudent(model.FirstName, model.LastName, model.EnrollmentDate);
            }
            if(studentId > 0)
            {
                return RedirectToAction("Index");
            }
            return Json(new { result = "error" });
        }

        //Get Student/Delete
        public async Task<IActionResult>Delete(int id)
        {
            var student = await _studentService.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }            
            return View(student);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>DeleteConfirmed(int id)
        {
            var student = await _studentService.GetStudentById(id);

            if (student == null)
            {
                return RedirectToAction("Index");
            }

            if (await _studentService.DeleteStudent(id))
            {
                return RedirectToAction("Index");
            }
            return Json(new { result = "error" });
        }
    }
}
