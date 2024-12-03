namespace TodoListApp.Services.Interfaces
{
    public interface IInsert<T> where T : class
    {
        T Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> Collection);
    }
}
