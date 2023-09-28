namespace WebAPI.Contracts
{
    public interface ICrud<T>
    {
        public Task<T> SaveRecord(T entity);
        public Task<T> GetRecordById(long id);
        public Task<T> UpdateRecord(T entity);
        public Task<T> DeleteRecord(long id);
    }
}
