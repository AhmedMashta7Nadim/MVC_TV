namespace TV.Repositry.Serves
{
    public interface IRepositryAllModel<T, V> where T : class
    {

        Task<List<V>> GetAllAsync();
        Task<List<V>> GetAllAsyncP(int pagenumber);
        Task<V> GetById(Guid id);
        Task<T> GetByIdT(Guid id);
        Task<T> Add(T value); // virtual 
        Task<T> Puts(Guid id,T value);
        Task<T> DetailsAsync(Guid? id);
        Task<T> DeleteAsync(Guid id);
        Task<List<T>> GetAllTAsync();
        Task<T> DeletedSoft(Guid id);
    }
}
