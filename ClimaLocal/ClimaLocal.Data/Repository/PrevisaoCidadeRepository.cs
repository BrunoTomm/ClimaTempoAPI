using ClimaLocal.Domain.Interfaces;
using ClimaLocal.Domain.Models;

namespace ClimaLocal.Data.Repository;

public class PrevisaoCidadeRepository : IPrevisaoCidadeRepository
{
    private readonly ClimaContext _context;

    public PrevisaoCidadeRepository(ClimaContext context)
    {
        _context = context;
    }

    public void Adicionar(PrevisaoCidade entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _context.Set<PrevisaoCidade>().Add(entity);
        _context.SaveChanges();
    }

}
