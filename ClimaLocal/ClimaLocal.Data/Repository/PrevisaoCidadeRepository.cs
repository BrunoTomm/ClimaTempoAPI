using ClimaLocal.Domain.Interfaces;
using ClimaLocal.Domain.Models;

namespace ClimaLocal.Data.Repository;

public class PrevisaoCidadeRepository : RepositoryBase<PrevisaoCidade>, IPrevisaoCidadeRepository
{
    public PrevisaoCidadeRepository(ClimaContext context) : base(context)
    {
    }
}
