using ClimaLocal.Domain.Interfaces;
using ClimaLocal.Domain.Models;
using ClimaLocal.Domain.ViewModels.Response;

namespace ClimaLocal.App.Interfaces;

public interface IClimaApp
{
    List<CidadeResponse> RetornaCidades(); 
    PrevisaoClimaCidadeResponse RetornaClimaCidade(int id);
    PrevisaoClimaAeroportoResponse RetornaClimaAeroporto(string icaoCode);
}
