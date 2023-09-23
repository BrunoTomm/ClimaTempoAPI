using ClimaLocal.Domain.Interfaces;
using ClimaLocal.Domain.Models;

namespace ClimaLocal.Data.Repository;

public class PrevisaoAeroportoRepository : RepositoryBase<PrevisaoAeroporto>, IPrevisaoAeroportoRepository
{
    public PrevisaoAeroportoRepository(ClimaContext context) : base(context)
    {
    }
}
