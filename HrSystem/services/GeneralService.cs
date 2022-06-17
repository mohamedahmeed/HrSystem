using System.Collections.Generic;
using System.Linq;
using GraduationProject.Models;
using GraduationProject.Services;
namespace HrSystem.services
{
    public class GeneralService : GraduationProject.Services.IRepository<GeneralSetting>
    {
        HRSystem dp;
        public GeneralService(HRSystem _dp)
        {
            dp = _dp;
        }

        public int Delete(int id)
        {
            GeneralSetting gs = dp.General_Settings.FirstOrDefault(x => x.ID == id);
            dp.General_Settings.Remove(gs);
            int row = dp.SaveChanges();
            return row;
        }

        public List<GeneralSetting> GetAll()
        {
            return dp.General_Settings.ToList();
        }

        public GeneralSetting GetById(int id)
        {
            GeneralSetting gs=dp.General_Settings.FirstOrDefault(x=>x.ID==id);
             return gs;
        }

        public GeneralSetting GetByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public int Insert(GeneralSetting entity)
        {
            dp.General_Settings.Add(entity);
            int row=dp.SaveChanges();
            return row;
        }

        public int Update(int id, GeneralSetting entity)
        {
            GeneralSetting gs = dp.General_Settings.FirstOrDefault(x => x.ID == id);
            gs.ID=entity.ID;
            gs.Extra=entity.Extra;
            gs.Discount=entity.Discount;
            gs.Dayoff_1=entity.Dayoff_1;
            gs.Dayoff_2=entity.Dayoff_2;
            int row=dp.SaveChanges();
            return row;
        }
    }
}
