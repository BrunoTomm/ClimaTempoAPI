using ClimaLocal.Domain.Interfaces;
using ClimaLocal.Domain.Models;


namespace ClimaLocal.Data.Repository;

public class LogRepository : ILogRepository
{
    private readonly ClimaContext _context;

    public LogRepository(ClimaContext context)
    {
        _context = context;
    }

    public void Adicionar(Log entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _context.Set<Log>().Add(entity);
        _context.SaveChanges();
    }
}
