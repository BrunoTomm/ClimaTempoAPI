using ClimaLocal.Domain.Models;

namespace ClimaLocal.Domain.Interfaces;

public interface IPrevisaoCidadeRepository
{
    void Adicionar(PrevisaoCidade entity);
}
