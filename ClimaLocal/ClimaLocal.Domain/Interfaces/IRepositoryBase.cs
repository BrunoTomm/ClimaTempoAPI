using ClimaLocal.Domain.Models;

namespace ClimaLocal.Domain.Interfaces;

public interface IRepositoryBase<T> where T : class
{
    void Adicionar(T entity);
}
