using ClimaLocal.Domain.Interfaces;
using ClimaLocal.Domain.Models;
using ClimaLocal.Domain.ViewModels.Response;

namespace ClimaLocal.App.Interfaces;

public interface IClimaApp
{
    Task<List<CidadeResponse>> RetornaCidades();
    Task<PrevisaoClimaCidadeResponse> RetornaClimaCidade(int idCidade);
    Task<PrevisaoClimaAeroportoResponse> RetornaClimaAeroporto(string icaoCode);
}
