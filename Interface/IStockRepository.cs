using learn_c__api.models;

namespace learn_c__api.Interface
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateStock(Stock stockModel);
        //Task<Stock?> DeleteStock(int id);
        Task<Stock?> DeleteAsync(int id);
    }
}
