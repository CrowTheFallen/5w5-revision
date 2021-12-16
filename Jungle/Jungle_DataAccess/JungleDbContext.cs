using Jungle_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungle_DataAccess
{
  public class JungleDbContext:DbContext
  {
    public JungleDbContext(DbContextOptions<JungleDbContext> options) : base(options)
    {
    
    }

    public DbSet<Guide> Guide { get; set; }
    public DbSet<Travel> Travel { get; set; }
    public DbSet<TravelRecommendation> TravelRecommendation { get; set; }
    public DbSet<Destination> Destination { get; set; }
    public DbSet<Country> Country { get; set; }

    public DbSet<Evaluation> Evaluations { get; set; }

    public DbSet<Option> Options { get; set; }

    public DbSet<Customer> Customers { get; set; }

  }
}
