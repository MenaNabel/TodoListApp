using System.Collections.Generic;

namespace TodoListApp.Repository
{
    public interface IDeleteRepository<T> where T : class
    {
        void Delete(T entity);

        void Delete(params T[] entities);

        void Delete(IEnumerable<T> entities);
        bool commit();
    }
}