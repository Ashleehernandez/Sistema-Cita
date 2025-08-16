

namespace Sistema_Citas.Application.Interface.Genery
{
   public interface  IGeneryRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task<T> UpdateAsyncc(int id, T updatedEntity);
        Task<T> Delete(int id);


    }
}
