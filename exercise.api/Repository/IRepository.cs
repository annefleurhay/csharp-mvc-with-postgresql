using exercise.api.Models;

namespace exercise.api.Repository
{
    public interface IRepository
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployee(int id);
        
        bool AddEmployee(Employee employee);
        bool DeleteEmployee(int id);
        bool UpdateEmployee(Employee employee);

    }
}
