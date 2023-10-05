using exercise.api.Data;
using exercise.api.Models;
using System.Linq.Expressions;

namespace exercise.api.Repository
{
    public class Repository : IRepository
    {
        public bool AddEmployee(Employee employee)
        {
            using (var db = new DataContext())
            {
                try
                {
                    db.employees.Add(employee);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    
                    return false;
                }
            }
        }

        public bool DeleteEmployee(int id)
        {
            using (var db = new DataContext())
            {
                var target = db.employees.FirstOrDefault(e => e.id == id);
                if (target != null)
                {
                    db.employees.Remove(target);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public Employee GetEmployee(int id)
        {
            Employee result= null;
            using (var db = new DataContext())
            {
                try
                {
                    result = db.employees.FirstOrDefault(e => e.id == id);
                    return result;
                }

                catch (Exception ex)
                {
                    return result;
                };
            }
        }

        public IEnumerable<Employee> GetEmployees()
        {
            using (var db = new DataContext())
            {
                return db.employees.ToList();
            }
            
            //acces the database from here.
        }

        public bool UpdateEmployee(Employee employee)
        {
            using (var db = new DataContext())
            {
                var existingEmployee = db.employees.FirstOrDefault(e => e.id==employee.id);
                if(existingEmployee != null)
                {
                    existingEmployee.salaryGrade = employee.salaryGrade;
                    existingEmployee.name = employee.name;
                    existingEmployee.jobName = employee.jobName;
                    existingEmployee.department = employee.department;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
