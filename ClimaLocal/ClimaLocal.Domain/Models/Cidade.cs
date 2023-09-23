using ClimaLocal.Domain.ViewModels.Response;

namespace ClimaLocal.Domain.Models;

public class Cidade : EntidadeBase
{
    public string Nome { get; private set; }
    public string Estado { get; private set; }
    public int Codigo { get; private set; }

    public static Cidade ToEntity(CidadeDTO cidadeDTO)
    {
        if (cidadeDTO == null)
        {
            throw new ArgumentNullException(nameof(cidadeDTO));
        }

        return new Cidade
        {
            Nome = cidadeDTO.Nome,
            Estado = cidadeDTO.Estado,
            Codigo = cidadeDTO.Id
        };
    }
}
