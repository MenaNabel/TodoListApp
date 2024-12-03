namespace TodoListApp.Services.Interfaces
{
    public interface IRead<TEntity> where TEntity : class 
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Filter(Func<TEntity , bool> FilterCondition);
    }
}
