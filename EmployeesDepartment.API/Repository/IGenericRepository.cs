namespace EmployeesDepartment.API.Services
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<T> GetByIdAsync(int id);
        
        void DeleteAsync(T entity);
        void Insert (T entity);

        

        public Task SaveChangesAsync();

    }
}
