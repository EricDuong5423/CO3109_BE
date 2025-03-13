namespace CO3109_BE.Repository
{
    public interface IRepository<T> where T : class
    {
        //Take all data
        Task<IEnumerable<T>> GetAllAsync();
        //Take data by id
        Task<T?> GetByIdAsync(String id);
        //Create data
        Task CreateAsync(T entity);
        //Update data
        Task UpdateAsync(String id, T entity);
        //Delete data
        Task DeleteAsync(String id);
    }
}
