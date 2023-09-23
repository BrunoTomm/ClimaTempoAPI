namespace ClimaLocal.Domain.ViewModels.Response;

public class PrevisaoClimaAeroportoDTO
{
    public string codigo_icao { get; set; }
    public string atualizado_em { get; set; }
    public int pressao_atmosferica { get; set; }
    public string visibilidade { get; set; }
    public int vento { get; set; }
    public int direcao_vento { get; set; }
    public int umidade { get; set; }
    public string condicao { get; set; }
    public string condicao_Desc { get; set; }
    public float temp { get; set; }
}

