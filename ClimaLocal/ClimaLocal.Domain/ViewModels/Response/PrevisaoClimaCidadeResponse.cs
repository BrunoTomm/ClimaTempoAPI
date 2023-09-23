using ClimaLocal.Domain.Models;

namespace ClimaLocal.Domain.ViewModels.Response;

public class PrevisaoClimaCidadeResponse
{
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public DateTime AtualizadoEm { get; set; }
    public Clima Clima { get; set; }
}

