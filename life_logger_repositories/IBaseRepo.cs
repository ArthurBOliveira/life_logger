using life_logger_models;

namespace life_logger_repositories
{
    public interface IBaseRepo<T> where T : BaseModel
    {
        T Create(T item);
        T Select(Guid id);
        IEnumerable<T> SelectMany();
        IEnumerable<T> SelectFilter(string filter);
        T Update(T item);
        void Delete(Guid id);
    }
}
