using ClimaLocal.Domain.Models;

namespace ClimaLocal.Domain.Interfaces;

public interface IPrevisaoAeroportoRepository
{
    void Adicionar(PrevisaoAeroporto entity);
}
