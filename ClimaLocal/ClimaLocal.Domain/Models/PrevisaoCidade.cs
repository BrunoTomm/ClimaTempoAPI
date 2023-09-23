using ClimaLocal.Domain.ViewModels.Response;

namespace ClimaLocal.Domain.Models;

public class PrevisaoCidade : EntidadeBase
{
    public Cidade Cidade { get; private set; }
    public Clima Clima { get; private set; }


    public static PrevisaoCidade ToEntityPrevisaoCidade(PrevisaoClimaCidadeResponse previsaoClimaCidadeResponse)
    {
        if (previsaoClimaCidadeResponse == null)
        {
            throw new ArgumentNullException(nameof(previsaoClimaCidadeResponse));
        }

        var cidadeDto = new CidadeDTO
        {
            Nome = previsaoClimaCidadeResponse.Cidade,
            Estado = previsaoClimaCidadeResponse.Estado
        };

        var cidade = Cidade.ToEntity(cidadeDto);

        var previsaoCidade = new PrevisaoCidade
        {
            Cidade = cidade,
            Clima = previsaoClimaCidadeResponse.Clima
        };

        return previsaoCidade;
    }

};



