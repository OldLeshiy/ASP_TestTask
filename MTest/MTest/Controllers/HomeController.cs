using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MTest.Models;
using System.Data.Entity;

namespace MTest.Controllers
{
    public class HomeController : Controller
    {
        EmployeeContext db = new EmployeeContext();
        
        public ActionResult Index(string position,int page = 1)
        {
            int pageSize = 3;
            position = position ?? "Все";

            IQueryable<Employee> employees = db.Employees;            
            if (position == "Активен")
            {
                employees = employees.Where(e => e.Status == true);
            }
            if (position == "Неактивен")
            {
                employees = employees.Where(e => e.Status == false);
            }
            if (position == "Все")
            {
                employees = employees.Where(e => (e.Status==false || e.Status == true));
            }
            
            IEnumerable<Employee> employeePerPage =
                employees.OrderBy(e=>e.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            
            PageInfo pageInfo =
                new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = employees.Count()};
            
            IndexViewModel ivm = new IndexViewModel
            {   PageInfo = pageInfo,
                Employees = employeePerPage,
                StatusList = new SelectList((new List<string>() { "Все","Активен","Неактивен"})),
                PositionNow = position
            };
            
            return View(ivm);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Employee e = db.Employees.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }   
            return View(e);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmedDelete(int id)
        {
            Employee e = db.Employees.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            db.Employees.Remove(e);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Employee e = db.Employees.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            return View(e);
        }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}