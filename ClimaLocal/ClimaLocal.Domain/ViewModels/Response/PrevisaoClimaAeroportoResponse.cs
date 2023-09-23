using ClimaLocal.Domain.Models;

namespace ClimaLocal.Domain.ViewModels.Response;

public class PrevisaoClimaAeroportoResponse
{
    public string AtualizadoEm { get; set; }
    public Clima Clima { get; set; }
}

