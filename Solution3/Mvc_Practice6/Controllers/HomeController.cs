using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Mvc_Practice6.Models;
using Newtonsoft.Json;

namespace Mvc_Practice6.Controllers
{
    public class HomeController : Controller
    {

        private Management db = new Management();
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }

        public PartialViewResult EmployeeSection()
        {
            Thread.Sleep(3000);
            List<Employee> employees = db.Employees.ToList();
            return PartialView("_EmployeeSection", employees);
        }


        public PartialViewResult DepartmentSection()
        {
            Thread.Sleep(3000);
            List<Department> departments = db.Departments.ToList();
            return PartialView("_DepartmentSection", departments);
        }



        public void CreateEmployee(Employee employee)
        {

            Employee emp = db.Employees.Add(employee);
            db.SaveChanges();

        }


      
        [HttpGet]
        public JsonResult GetEmpById(int id)
        {
            var dept = db.Departments.ToList();

            ViewBag.departments = new SelectList(dept, "Id", "Name");

            Employee employee = db.Employees.Single(emp => emp.Id == id);
            string res = JsonConvert.SerializeObject(employee, Formatting.Indented,
                                                                                    new JsonSerializerSettings
                                                                                    {
                                                                                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                                                                    });
            //  JsonResult result = new JsonResult();
            //  result.Data = res;
            return Json(res, JsonRequestBehavior.AllowGet);



        }

        public JsonResult LoadDropDownList()
        {
            var dept = db.Departments.ToList();

            string res = JsonConvert.SerializeObject(dept, Formatting.Indented, new JsonSerializerSettings
                                                                                    {
                                                                                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                                                                    });
           
            return Json(res, JsonRequestBehavior.AllowGet);
        }
      
        [HttpGet]
        public JsonResult GetAllEmployee()
        {

            IEnumerable<Employee> employee = db.Employees.ToList();

            string res = JsonConvert.SerializeObject(employee, Formatting.Indented,
                                                                 new JsonSerializerSettings
                                                                 {
                                                                     PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                                                 });
           
            return Json(res,JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public void DeleteEmp(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
        }

        [HttpPost]
        public void EditEmp(Employee emp)
        {
            db.Entry(emp).State = EntityState.Modified;
            db.SaveChanges();
        }


       //---------------------------------------------------------------------departmentcode begins------------------------------------------------

        public void CreateDepartment(Department department)
        {

            Department dept = db.Departments.Add(department);
            db.SaveChanges();

        }

        public PartialViewResult GetAllEmpDept(int id)
        {
            IEnumerable<Employee> employees = db.Employees.Where(emp => emp.DepartmentId == id).ToList();

            return PartialView("GetAllEmpDept",employees);
        }


        public void DeleteDept(int id)
        {
            Department dept = db.Departments.Find(id);
            db.Departments.Remove(dept);
            db.SaveChanges();
        }

        public void EditDept(Department dept)
        {
            db.Entry(dept).State = EntityState.Modified;
            db.SaveChanges();
        }


        public JsonResult GetDeptById(int id)
        {
          
            Department department = db.Departments.Single(dept => dept.Id == id);
            string res = JsonConvert.SerializeObject(department, Formatting.Indented,
                                                                                    new JsonSerializerSettings
                                                                                    {
                                                                                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                                                                    });
            //  JsonResult result = new JsonResult();
            //  result.Data = res;
            return Json(res, JsonRequestBehavior.AllowGet);



        }

    }
}