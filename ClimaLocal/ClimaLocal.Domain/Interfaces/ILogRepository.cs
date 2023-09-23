using ClimaLocal.Domain.Models;

namespace ClimaLocal.Domain.Interfaces;

public interface ILogRepository
{
    void Adicionar(Log entity);
}
