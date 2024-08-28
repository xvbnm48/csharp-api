﻿using learn_c__api.Data;
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
        var stocks = _context.Stocks.ToList();
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
}
