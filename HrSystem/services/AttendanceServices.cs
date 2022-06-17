using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GraduationProject.Services
{
    public class AttendanceServices : IRepository<Attendance_Leaving>
    {
        private readonly HRSystem hrSystem;

        public AttendanceServices(HRSystem _HrSystem)
        {
            hrSystem = _HrSystem;
        }
        public int Delete(int id)
        {
            Attendance_Leaving old = hrSystem.Attendance_Leavings.FirstOrDefault(d => d.ID == id);
            hrSystem.Attendance_Leavings.Remove(old);
            int raw = hrSystem.SaveChanges();
            return raw;
        }

        public List<Attendance_Leaving> GetAll()
        {
            List<Attendance_Leaving> Attendance = hrSystem.Attendance_Leavings.Include(e=>e.Emp).ToList();
            return Attendance;
        }

        public Attendance_Leaving GetById(int id)
        {
            return hrSystem.Attendance_Leavings.FirstOrDefault(d => d.ID == id);

        }

        public Attendance_Leaving GetByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public int Insert(Attendance_Leaving Newobj)
        {
            hrSystem.Attendance_Leavings.Add(Newobj);
            int raw = hrSystem.SaveChanges();
            return raw;
        }

        public int Update(int id, Attendance_Leaving Newobj)
        {
            Attendance_Leaving old = hrSystem.Attendance_Leavings.FirstOrDefault(d => d.ID == id);
            old.Emp_ID = Newobj.Emp_ID;
            old.Date = Newobj.Date;
            old.AttendanceTime = Newobj.AttendanceTime;
            old.LeavingTime = Newobj.LeavingTime;
            int raw = hrSystem.SaveChanges();
            return raw;
        }
    }
}
