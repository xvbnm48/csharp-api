﻿using learn_c__api.Data;
using learn_c__api.Interface;
using learn_c__api.models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace learn_c__api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public Task<List<Stock>> GetAllAsync()
        {
            return _context.Stocks.ToListAsync();
        }

        public async Task<Stock> CreateStock(Stock stockModel)
        {
           await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        //public async Task<Stock?> DeleteStock(int id)
        //{
        //   var stockModel = _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        //    if 
        //        (stockModel == null)
        //    {
        //        return null;
        //    }
        //    _context.Stocks.Remove(stockModel);
        //    await _context.SaveChangesAsync();
        //    return stockModel;
        //}
    }
}
