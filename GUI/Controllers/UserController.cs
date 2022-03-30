using MyApp.DAO;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MyApp.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            UserDAO userDAO = new UserDAO(Properties.Settings.Default.ConnectionString);
            List<UserDTO> userList = userDAO.GetAll();

            ViewBag.Data = userList;
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserDTO userDTO)
        {
            try
            {
                UserDAO userDAO = new UserDAO(Properties.Settings.Default.ConnectionString);
                userDAO.AddUser(userDTO);

                return Redirect("index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}