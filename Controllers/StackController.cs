using learn_c__api.Data;
using learn_c__api.Dtos.Stock;
using learn_c__api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace learn_c__api.Controllers;
[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public StockController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var stocks = _context.Stocks.ToList()
            .Select(s => s.ToStockDto());
        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = _context.Stocks.Find(id);
        if (stock == null)
        {
            return NotFound();
        }

        return Ok(stock);
    }
    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
    {
        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(stockDto));
        var stockModel = stockDto.ToStockFromCreateDTO();
        _context.Stocks.Add(stockModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }
    [HttpPut]
    [Route("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
    {
        var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
        if (stockModel== null)
        {
            return NotFound();
        }
        stockModel.Symbol = stockDto.Symbol;
        stockModel.CompanyName = stockDto.CompanyName;
        stockModel.Purchase = stockDto.Purchase;
        stockModel.LastDiv = stockDto.LastDiv;
        stockModel.Industry = stockDto.Industry;
        stockModel.MarketCap = stockDto.MarketCap;

        _context.SaveChanges();

        return Ok(stockModel.ToStockDto());

    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var IdStock = _context.Stocks.Find(id);
        if (IdStock == null)
        {
            return NotFound();
        }

        _context.Stocks.Remove(IdStock);
        return Ok();
    }
}
