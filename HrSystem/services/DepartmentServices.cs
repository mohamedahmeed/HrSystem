using GraduationProject.Models;
using GraduationProject.Services;
using System.Collections.Generic;
using System.Linq;

namespace HrSystem.Services
{
    public class DepartmentServices : IRepository<Department>
    {
        private readonly HRSystem hrSystem;

        public DepartmentServices(HRSystem _HrSystem)
        {
            hrSystem = _HrSystem;
        }
        public int Delete(int id)
        {
            Department old = hrSystem.Departments.FirstOrDefault(d => d.ID == id);
            hrSystem.Departments.Remove(old);
            int raw = hrSystem.SaveChanges();
            return raw;
        }

        public List<Department> GetAll()
        {
            List<Department> departments = hrSystem.Departments.ToList();
            return departments;
        }

        public Department GetById(int id)
        {
            return hrSystem.Departments.FirstOrDefault(d => d.ID == id);

        }

        public Department GetByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public int Insert(Department Newobj)
        {
            hrSystem.Departments.Add(Newobj);
            int raw = hrSystem.SaveChanges();
            return raw;
        }

        public int Update(int id, Department Newobj)
        {
            Department old = hrSystem.Departments.FirstOrDefault(d => d.ID == id);
            old.Name = Newobj.Name;
            int raw = hrSystem.SaveChanges();
            return raw;
        }
    }
}
