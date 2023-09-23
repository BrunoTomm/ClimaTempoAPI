namespace ClimaLocal.Domain.ViewModels.Response;

public class PrevisaoClimaCidadeDTO
{
    public string cidade { get; set; }
    public string estado { get; set; }
    public DateTime atualizado_em { get; set; }
    public List<ClimaDTO> clima { get; set; }
}
public class ClimaDTO
{
    public DateTime data { get; set; }
    public string condicao { get; set; }
    public int min { get; set; }
    public int max { get; set; }
    public int indice_uv { get; set; }
    public string condicao_desc { get; set; }
    public int pressao_atmosferica { get; set; }
    public string visibilidade { get; set; }
    public int vento { get; set; }
    public int direcao_vento { get; set; }
    public int umidade { get; set; }
    public float temp { get; set; }
}
