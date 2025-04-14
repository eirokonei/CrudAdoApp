using Microsoft.AspNetCore.Mvc;
using CrudAdoApp.Models;
using CrudAdoApp.Data;

namespace CrudAdoApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDAL _dal;

        public StudentController(StudentDAL dal)
        {
            _dal = dal;
        }

        public IActionResult Index() => View(_dal.GetAllStudents());

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _dal.InsertStudent(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public IActionResult Edit(int id) => View(_dal.GetStudentById(id));

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _dal.UpdateStudent(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public IActionResult Delete(int id)
        {
            _dal.DeleteStudent(id);
            return RedirectToAction("Index");
        }
    }
}
