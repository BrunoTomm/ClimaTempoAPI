using ClimaLocal.Domain.ViewModels.Response;

namespace ClimaLocal.Domain.Models;

public class PrevisaoAeroporto : EntidadeBase
{
    public string CodigoIcao { get; private set; }

    public Clima Clima { get; private set; }

    public static PrevisaoAeroporto ToEntityPrevisaoAeroporto(PrevisaoClimaAeroportoResponse previsaoClimaAeroportoResponse, string codigoIcao)
    {
        var previsaoAeroporto = new PrevisaoAeroporto
        {
            CodigoIcao = codigoIcao,
            Clima = previsaoClimaAeroportoResponse.Clima
        };

        return previsaoAeroporto;
    }

}
