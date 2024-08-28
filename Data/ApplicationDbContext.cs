using learn_c__api.models;
using Microsoft.EntityFrameworkCore;

namespace learn_c__api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base  (dbContextOptions) 
    {
        
    }
    
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
}