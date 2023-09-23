using ClimaLocal.App.Interfaces;
using ClimaLocal.Client.Interfaces;
using ClimaLocal.Domain.Interfaces;
using ClimaLocal.Domain.Models;
using ClimaLocal.Domain.ViewModels.Response;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Log = ClimaLocal.Domain.Models.Log;

namespace ClimaLocal.App.Services;

public class ClimaApp : IClimaApp
{
    private readonly ILogger<ClimaApp> _logger;
    private readonly IRestClimaTempo _restClimaTempo;
    private readonly IPrevisaoCidadeRepository _previsaoCidadeRepository;
    private readonly IPrevisaoAeroportoRepository _previsaoAeroportoRepository;
    private readonly ILogRepository _logRepository;

    public ClimaApp(
        IRestClimaTempo restClimaTempo,
        IPrevisaoCidadeRepository previsaoCidadeRepository,
        IPrevisaoAeroportoRepository previsaoAeroportoRepository,
        ILogger<ClimaApp> logger,
        ILogRepository logRepository)
    {
        _previsaoCidadeRepository = previsaoCidadeRepository;
        _restClimaTempo = restClimaTempo;
        _previsaoAeroportoRepository = previsaoAeroportoRepository;
        _logger = logger;
        _logRepository = logRepository;
    }

    public List<CidadeResponse> RetornaCidades()
    {
        _logger.LogInformation("Inicio da busca pelas cidades.");

        var url = "https://brasilapi.com.br/api/cptec/v1/cidade";

        var retorno = _restClimaTempo.GetAsync(url).Result;

        var retornoDeserialize = JsonConvert.DeserializeObject<List<CidadeDTO>>(retorno);

        var cidadeResponse = new List<CidadeResponse>();

        foreach (var cidadeDTO in retornoDeserialize)
        {
            cidadeResponse.Add(new CidadeResponse
            {
                Nome = cidadeDTO.Nome,
                Estado = cidadeDTO.Estado,
                CodigoCidade = cidadeDTO.Id,
            });
        }

        _logger.LogInformation("Fim da busca pelas cidades.");

        return cidadeResponse;
    }

    public PrevisaoClimaCidadeResponse RetornaClimaCidade(int idCidade)
    {
        try
        {
            _logger.LogInformation($"Inicio da busca pelo clima da cidade com ID = {idCidade}.");

            var url = $"https://brasilapi.com.br/api/cptec/v1/clima/previsao/{idCidade}";

            var retorno = _restClimaTempo.GetAsync(url).Result;

            var retornoDeserialize = JsonConvert.DeserializeObject<PrevisaoClimaCidadeDTO>(retorno);

            var climaCidadeResponse = new PrevisaoClimaCidadeResponse
            {
                Cidade = retornoDeserialize.cidade,
                Estado = retornoDeserialize.estado,
                AtualizadoEm = retornoDeserialize.atualizado_em,
            };

            climaCidadeResponse.Clima = ToEntityClima(retornoDeserialize.clima.FirstOrDefault());

            var entidadePrevisaCidade = PrevisaoCidade.ToEntityPrevisaoCidade(climaCidadeResponse);

            if (entidadePrevisaCidade != null)
                _previsaoCidadeRepository.Adicionar(entidadePrevisaCidade);

            _logger.LogInformation($"Fim da busca pelo clima da cidade com ID = {idCidade}.");

            return climaCidadeResponse;
        }
        catch (Exception ex)
        {
            _logRepository.Adicionar(new Log
            {
                Timestamp = DateTimeOffset.Now,
                Level = "Error",
                Message = $"Ocorreu um erro durante a execução da cidade com ID = {idCidade}. ",
            });

            throw new Exception("Ocorreu um erro durante a execução da cidade com ID = " + idCidade);
        }

    }

    public PrevisaoClimaAeroportoResponse RetornaClimaAeroporto(string icaoCode)
    {
        try
        {
            _logger.LogInformation($"Inicio da busca pelo clima do aeroporto com icaoCode = {icaoCode}.");

            var url = $"https://brasilapi.com.br/api/cptec/v1/clima/aeroporto/{icaoCode}";

            var retorno = _restClimaTempo.GetAsync(url).Result;

            var retornoDeserialize = new PrevisaoClimaAeroportoDTO();

            if (!retorno.Contains("undefined"))
                retornoDeserialize = JsonConvert.DeserializeObject<PrevisaoClimaAeroportoDTO>(retorno);
            else
                throw new Exception("O Aeroporto não consta na API.");

            var climaAeroportoResponse = new PrevisaoClimaAeroportoResponse
            {
                AtualizadoEm = retornoDeserialize.atualizado_em
            };

            var climaDTO = new ClimaDTO
            {
                pressao_atmosferica = retornoDeserialize.pressao_atmosferica,
                visibilidade = retornoDeserialize.visibilidade,
                vento = retornoDeserialize.vento,
                direcao_vento = retornoDeserialize.direcao_vento,
                umidade = retornoDeserialize.umidade,
                condicao = retornoDeserialize.condicao,
                condicao_desc = retornoDeserialize.condicao_Desc,
                temp = retornoDeserialize.temp,
            };

            climaAeroportoResponse.Clima = ToEntityClima(climaDTO);

            var entidadePrevisaCidade = PrevisaoAeroporto.ToEntityPrevisaoAeroporto(climaAeroportoResponse, retornoDeserialize.codigo_icao);

            if (entidadePrevisaCidade != null)
                _previsaoAeroportoRepository.Adicionar(entidadePrevisaCidade);

            _logger.LogInformation($"Inicio da busca pelo clima do aeroporto com icaoCode = {icaoCode}.");

            return climaAeroportoResponse;
        }
        catch (Exception ex)
        {
            _logRepository.Adicionar(new Log
            {
                Timestamp = DateTimeOffset.Now,
                Level = "Error",
                Message = $"Ocorreu um erro durante a execução do aeroporto com icaoCode = {icaoCode}. ",
            });

            throw new Exception($"Ocorreu um erro durante a execução do aeroporto com icaoCode = {icaoCode}");
        }

    }

    protected Clima ToEntityClima(ClimaDTO ClimaDTO)
    {
        var climaItem = new Clima();

        climaItem = Clima.ToEntity(ClimaDTO);

        return climaItem;
    }

}
