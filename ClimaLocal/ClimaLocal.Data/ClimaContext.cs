using ClimaLocal.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClimaLocal.Data;

public class ClimaContext : DbContext
{
    public ClimaContext(DbContextOptions<ClimaContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Cidade> Cidades { get; set; }
    public DbSet<Clima> Clima { get; set; }
    public DbSet<PrevisaoAeroporto> PrevisaoClimaAeroporto { get; set; }
    public DbSet<PrevisaoCidade> PrevisaoClimaCidade { get; set; }
    public DbSet<Log> Logs { get; set; }
}
