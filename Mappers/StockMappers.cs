using learn_c__api.Dtos.Stock;
using learn_c__api.models;

namespace learn_c__api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
               Id = stockModel.Id,
               Symbol = stockModel.Symbol,
               CompanyName = stockModel.CompanyName,
               Purchase = stockModel.Purchase,
               LastDiv = stockModel.LastDiv,
               Industry = stockModel.Industry,
               MarketCap = stockModel.MarketCap
            };
        }
    }
}
