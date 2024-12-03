namespace TodoListApp.Services.Interfaces
{
    public interface IInsertAsync<T> where T : class
    {
       Task<T> Add(T entity);
       Task<IEnumerable<T>> AddRange(IEnumerable<T> Collection);
    }
}
