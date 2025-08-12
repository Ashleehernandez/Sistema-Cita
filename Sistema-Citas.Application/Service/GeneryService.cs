

using Sistema_Citas.Application.Interface.Genery;
using Sistema_Citas.Application.Interface.Repository;

namespace Sistema_Citas.Application.Service
{
    public class GeneryService<T> : IGeneryService<T> where T : class
    {
        private readonly IGeneryRepository<T> _repository;
        private IUsuarioRepository usuarioRepositor;

       
        public GeneryService(IGeneryRepository<T> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository), "The repository cannot be null.");
        }
        public async Task AddAsync(T entity)
        {
            try
            {
                await _repository.AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the entity.", ex);
            }
        }

        public async Task<T> DeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                {
                    throw new Exception("The entity does not exist in the database.");
                }
                return await _repository.Delete(entity);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all entities.", ex);
            }

        }

        public Task<T> GetByIdAsync(int id)
        {
            try
            {
                return _repository.GetByIdAsync(id);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the entity by ID.", ex);
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                return await _repository.UpdateAsyncc(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the entity.", ex);
            }

        }
    }
}
