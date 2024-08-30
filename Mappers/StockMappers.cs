﻿using learn_c__api.Dtos.Stock;
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
        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName  = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }
       
    }
}
