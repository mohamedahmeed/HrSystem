using GraduationProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace GraduationProject.Services
{
    public class HolidayServices : IRepository<OfficialHolidays>
    {
        private readonly HRSystem hrSystem;

        public HolidayServices(HRSystem _HrSystem)
        {
            hrSystem = _HrSystem;
        }
        public int Delete(int id)
        {
            OfficialHolidays old = hrSystem.OfficialHolidays.FirstOrDefault(d => d.ID == id);
            hrSystem.OfficialHolidays.Remove(old);
            int raw = hrSystem.SaveChanges();
            return raw;
        }

        public List<OfficialHolidays> GetAll()
        {
            List<OfficialHolidays> Holidays = hrSystem.OfficialHolidays.ToList();
            return Holidays;
        }

        public OfficialHolidays GetById(int id)
        {
            return hrSystem.OfficialHolidays.FirstOrDefault(d => d.ID == id);

        }

        public OfficialHolidays GetByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public int Insert(OfficialHolidays Newobj)
        {
            hrSystem.OfficialHolidays.Add(Newobj);
            int raw = hrSystem.SaveChanges();
            return raw;
        }

        public int Update(int id, OfficialHolidays Newobj)
        {
            OfficialHolidays old = hrSystem.OfficialHolidays.FirstOrDefault(d => d.ID == id);
            old.Name = Newobj.Name;
            old.Date = Newobj.Date;
            int raw = hrSystem.SaveChanges();
            return raw;
        }
    }
}
