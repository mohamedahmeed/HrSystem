using System.Collections.Generic;

namespace GraduationProject.Services
{
    public interface IRepository<t>
    {
        public List<t> GetAll();
        public t GetById(int id);
        public int Insert(t Newobj);
        public int Update(int id, t Newobj);
        public int Delete(int id);
        public t GetByName(string name);
    }
}
