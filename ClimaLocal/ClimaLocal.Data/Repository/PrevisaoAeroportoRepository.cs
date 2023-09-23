using ClimaLocal.Domain.Interfaces;
using ClimaLocal.Domain.Models;

namespace ClimaLocal.Data.Repository;

public class PrevisaoAeroportoRepository : IPrevisaoAeroportoRepository
{
    private readonly ClimaContext _context;

    public PrevisaoAeroportoRepository(ClimaContext context)
    {
        _context = context;
    }

    public void Adicionar(PrevisaoAeroporto entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _context.Set<PrevisaoAeroporto>().Add(entity);
        _context.SaveChanges();
    }

}
