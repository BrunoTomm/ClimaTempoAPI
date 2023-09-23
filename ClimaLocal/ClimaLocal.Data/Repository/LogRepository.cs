using ClimaLocal.Domain.Interfaces;
using ClimaLocal.Domain.Models;


namespace ClimaLocal.Data.Repository;

public class LogRepository : RepositoryBase<Log>, ILogRepository
{
    public LogRepository(ClimaContext context) : base(context)
    {
    }
}
