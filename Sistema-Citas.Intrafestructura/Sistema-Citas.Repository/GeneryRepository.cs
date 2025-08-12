

using Microsoft.EntityFrameworkCore;
using Sistema_Citas.Application.Interface.Genery;
using Sistema_Citas.Intrafestructura.Sistema_Citas_DBContext;

namespace Sistema_Citas.Intrafestructura.Sistema_Citas.Repository
{
    public class GeneryRepository<T> : IGeneryRepository<T> where T : class
    {
        private readonly DBContext _context;
        private readonly DbSet<T> _dbSet;
        public GeneryRepository(DBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>() ?? throw new ArgumentNullException(nameof(_dbSet));
        }
        public async Task AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the entity.", ex);

            }
        }

        public async Task<T> Delete(T entity)
        {
            var existingEntity = await _dbSet.FindAsync(entity);
            if (existingEntity == null)
            {
                throw new Exception("La entidad no existe en la base de datos");
            }
            try
            {

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;

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
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all entities.", ex);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                {
                    throw new Exception("Entity not found.");
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the entity by ID.", ex);

            }
        }

        public async Task<T> UpdateAsyncc(T entity)
        {
            try
            {
                var existingEntity = await _dbSet.FindAsync(entity);
                if (existingEntity == null)
                {
                    throw new Exception("La entidad no existe en la base de datos");
                }
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return existingEntity;


            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the entity.", ex);

            }
        }
    }
}
