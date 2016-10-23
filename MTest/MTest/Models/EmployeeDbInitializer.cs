using System.Data.Entity;


namespace MTest.Models
{
    public class EmployeeDbInitializer : CreateDatabaseIfNotExists<EmployeeContext>
    {
        protected override void Seed(EmployeeContext db)
        {
            db.Employees.Add(new Employee { Name = "Вася", Position = "грузчик", Status = true, Salary = 220 });
            db.Employees.Add(new Employee { Name = "Петя", Position = "маляр", Status = true, Salary = 180 });
            db.Employees.Add(new Employee { Name = "Коля", Position = "учитель", Status = true, Salary = 150 });
 
            base.Seed(db);
        }
    }
}