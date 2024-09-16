using learn_c__api.Data;
using learn_c__api.Dtos.Stock;
using learn_c__api.Interface;
using learn_c__api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace learn_c__api.Controllers;
[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IStockRepository _stockRepo;
    public StockController(ApplicationDbContext context, IStockRepository repository)
    {
        _stockRepo = repository;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        //var stocks = await _context.Stocks.ToListAsync();
        var stocks = await _stockRepo.GetAllAsync();
        var stockDto = stocks.Select(s => s.ToStockDto());
        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _stockRepo.GetByIdAsync(id);
        //var stock = await _context.Stocks.FindAsync(id);
        if (stock == null)
        {
            return NotFound();
        }

        return Ok(stock);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(stockDto));
        var stockModel = stockDto.ToStockFromCreateDTO();
        //await _context.Stocks.AddAsync(stockModel);
        //await _context.SaveChangesAsync()
        await _stockRepo.CreateStock(stockModel);
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
    {
        //var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        var stockModel = await _stockRepo.UpdateAsync(id, stockDto);
        if (stockModel== null)
        {
            return NotFound();
        }
        //stockModel.Symbol = stockDto.Symbol;
        //stockModel.CompanyName = stockDto.CompanyName;
        //stockModel.Purchase = stockDto.Purchase;
        //stockModel.LastDiv = stockDto.LastDiv;
        //stockModel.Industry = stockDto.Industry;
        //stockModel.MarketCap = stockDto.MarketCap;

        //await _context.SaveChangesAsync();

        return Ok(stockModel.ToStockDto());

    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
       var stockModel = await _stockRepo.DeleteAsync(id);
        if (stockModel == null)
        {
            return NotFound();        
        }
        return NoContent();
        //var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        //if (stockModel == null)
        //{
        //    return NotFound();
        //}

        //_context.Stocks.Remove(stockModel);
        //await _context.SaveChangesAsync();
        //return NoContent();
        // var IdStock = _context.Stocks.Find(id);
        // if (IdStock == null)
        // {
        //     return NotFound(new {message = "Stock not found."});
        // }
        // _context.Stocks.Remove(IdStock);
        //
        // try
        // {
        //     _context.SaveChanges();
        // }
        // catch(Exception ex) 
        // {
        //     return StatusCode(StatusCodes.Status500InternalServerError, new { message = "an error occured while deleting the stock", details = ex.Message });
        // }
        // return Ok(new {message = "Stock delected successfully", statusCode = StatusCodes.Status200OK});
    }
}
