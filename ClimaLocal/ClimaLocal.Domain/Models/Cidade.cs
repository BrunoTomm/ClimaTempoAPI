using ClimaLocal.Domain.ViewModels.Response;

namespace ClimaLocal.Domain.Models;

public class Cidade : EntidadeBase
{
    public string Nome { get; private set; }
    public string Estado { get; private set; }
    public int Codigo { get; private set; }

    public static Cidade ToEntity(CidadeDTO climaDTO)
    {
        return new Cidade
        {
            Nome = climaDTO.Nome,
            Estado = climaDTO.Estado,
            Codigo = climaDTO.Id
        };
    }
}
