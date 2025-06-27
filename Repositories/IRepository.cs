using RazorPagesTodoList.Models;

namespace RazorPagesTodoList.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        List<T> GetAll();
        T? GetById(int id);
        void Create(T entity);
        void Delete(int id);
        void SaveChanges();
    }
}
