﻿using learn_c__api.Data;
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
}
