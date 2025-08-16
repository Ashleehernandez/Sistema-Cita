

namespace Sistema_Citas.Application.Interface.Genery
{
    public interface IGeneryService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task<T> UpdateAsync(int id, T updatedEntity);
        Task<T> DeleteAsync(int id);
    }
   
}
