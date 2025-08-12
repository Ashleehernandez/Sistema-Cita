

namespace Sistema_Citas.Application.Interface.Genery
{
   public interface  IGeneryRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task<T> UpdateAsyncc(T entity);
        Task<T> Delete(T entity);


    }
}
