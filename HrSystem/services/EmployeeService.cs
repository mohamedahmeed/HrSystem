using GraduationProject.Models;
using GraduationProject.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HrSystem.services
{
    public class EmployeeService :IRepository<Employee>
    {
        HRSystem dp;
        public EmployeeService(HRSystem _dp)
        {
            dp = _dp;
        }
        public List<Employee> GetAll()
        {
            List<Employee> employees = dp.Employees.Include(s => s.Dept).ToList();
            return employees;
        }

        public Employee GetById(int id)
        {
            Employee employee = dp.Employees.Include(s => s.Dept).FirstOrDefault(x => x.ID == id);
            return employee;
        }
        public Employee GetByName(string name)
        {
            Employee employee = dp.Employees.Include(s => s.Dept).FirstOrDefault(s => s.Name == name);
            return employee;
        }
        public int Delete(int id)
        {
            Employee employee = dp.Employees.Include(s => s.Dept).FirstOrDefault(s => s.ID == id);
            dp.Remove(employee);
            int row = dp.SaveChanges();
            return row;
        }
        public int Insert(Employee e)
        {
            dp.Employees.Add(e);
            int row = dp.SaveChanges();
            return row;
        }
        public int Update(int id, Employee e)
        {
            Employee employee = dp.Employees.Include(s => s.Dept).FirstOrDefault(s => s.ID == id);
            employee.Name = e.Name;
            employee.ID = e.ID;
            employee.Address = e.Address;
            employee.Phone = e.Phone;
            employee.BirthDay = e.BirthDay;
            employee.Salary = e.Salary;
            employee.Gender = e.Gender;
            employee.AttendanceTime = e.AttendanceTime;
            employee.LeavingTime = e.LeavingTime;
            employee.ContractDate = e.ContractDate;
            employee.NationalityID = e.NationalityID;
            employee.Nationality = e.Nationality;
            employee.Dept_ID = e.Dept_ID;
           
            int row = dp.SaveChanges();
            return row;
        }
    }
}
