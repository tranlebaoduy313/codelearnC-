using MyApp2.DAO;
using MyApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyApp2.Controllers
{
    public class StudentController : Controller
    {
    
        // GET: Student
        public ActionResult Index()
        {
            StudentDAO studentDAO = new StudentDAO(Properties.Settings.Default.ConnectionString);
            List<StudentDTO> studentList = studentDAO.GetAll();

            ViewBag.Data = studentList;
            return View();
        }

        #region Create Student
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(StudentDTO studentDTO)
        {
            try
            {
                StudentDAO studentDAO = new StudentDAO(Properties.Settings.Default.ConnectionString);
                studentDAO.AddStudent(studentDTO);

                return Redirect("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        #endregion
        [HttpPost]
        public ActionResult Delete(string NameID)
        {
            StudentDAO studentDAO = new StudentDAO(Properties.Settings.Default.ConnectionString);
            studentDAO.DeleteStudent(NameID);
            return View();
        }
        public ActionResult Delete()
        {
            
            return View();
        }
    }
}