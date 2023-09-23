using ClimaLocal.Domain.ViewModels.Response;

namespace ClimaLocal.Domain.Models;

public class Clima : EntidadeBase
{
    public DateTime? Data { get; private set; }
    public DateTime DataAtualizacao { get; private set; }
    public string Condicao { get; private set; }
    public string CondicaoDescricao { get; private set; }
    public int TemperaturaMin { get; private set; }
    public int TemperaturaMax { get; private set; }
    public float Temperatura { get; private set; }
    public int IndiceUv { get; private set; }
    public int? PressaoAtmosferica { get; private set; }
    public string? Visibilidade { get; private set; }
    public int? Vento { get; private set; }
    public int? DirecaoVento { get; private set; }
    public int? Umidade { get; private set; }

    public static Clima ToEntity(ClimaDTO climaDTO)
    {
        if (climaDTO == null)
        {
            throw new ArgumentNullException(nameof(climaDTO));
        }

        return new Clima
        {
            Data = climaDTO.data,
            Condicao = climaDTO.condicao,
            CondicaoDescricao = climaDTO.condicao_desc,
            TemperaturaMin = climaDTO.min,
            TemperaturaMax = climaDTO.max,
            Temperatura = climaDTO.temp,
            IndiceUv = climaDTO.indice_uv,
            PressaoAtmosferica = climaDTO.pressao_atmosferica,
            Visibilidade = climaDTO.visibilidade,
            Vento = climaDTO.vento,
            DirecaoVento = climaDTO.direcao_vento,
            Umidade = climaDTO.umidade,
        };
    }
}

