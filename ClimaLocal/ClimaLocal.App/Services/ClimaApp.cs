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
        _previsaoCidadeRepository = previsaoCidadeRepository ?? throw new ArgumentNullException(nameof(previsaoCidadeRepository));
        _restClimaTempo = restClimaTempo ?? throw new ArgumentNullException(nameof(restClimaTempo));
        _previsaoAeroportoRepository = previsaoAeroportoRepository ?? throw new ArgumentNullException(nameof(previsaoAeroportoRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
    }

    public async Task<List<CidadeResponse>> RetornaCidades()
    {
        _logger.LogInformation("Início da busca pelas cidades.");

        var url = "https://brasilapi.com.br/api/cptec/v1/cidade";

        try
        {
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar cidades.");

            await RegistrarLogNoBanco(ex, true, "");

            throw new Exception($"Ocorreu um erro ao buscar cidades. {ex.Message}");
        }
    }

    public async Task<PrevisaoClimaCidadeResponse> RetornaClimaCidade(int idCidade)
    {
        _logger.LogInformation($"Início da busca pelo clima da cidade com ID = {idCidade}.");

        var url = $"https://brasilapi.com.br/api/cptec/v1/clima/previsao/{idCidade}";

        try
        {
            var retorno = _restClimaTempo.GetAsync(url).Result;

            if (retorno.Contains("Erro"))
                throw new Exception("A Cidade não consta na API.");

            var retornoDeserialize = JsonConvert.DeserializeObject<PrevisaoClimaCidadeDTO>(retorno);

            var climaCidadeResponse = new PrevisaoClimaCidadeResponse
            {
                Cidade = retornoDeserialize.cidade,
                Estado = retornoDeserialize.estado,
                AtualizadoEm = retornoDeserialize.atualizado_em,
            };

            climaCidadeResponse.Clima = ToEntityClima(retornoDeserialize.clima?.FirstOrDefault());

            var entidadePrevisaoCidade = PrevisaoCidade.ToEntityPrevisaoCidade(climaCidadeResponse);

            if (entidadePrevisaoCidade != null)
                _previsaoCidadeRepository.Adicionar(entidadePrevisaoCidade);

            _logger.LogInformation($"Fim da busca pelo clima da cidade com ID = {idCidade}.");

            return climaCidadeResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erro ao buscar clima da cidade com ID = {idCidade}.");

            await RegistrarLogNoBanco(
                ex,
                isCidade: true,
                identificacao: idCidade.ToString());

            throw new Exception($"Ocorreu um erro ao buscar o clima da cidade com ID = {idCidade}. {ex.Message}");
        }
    }

    public async Task<PrevisaoClimaAeroportoResponse> RetornaClimaAeroporto(string icaoCode)
    {
        _logger.LogInformation($"Início da busca pelo clima do aeroporto com icaoCode = {icaoCode}.");

        var url = $"https://brasilapi.com.br/api/cptec/v1/clima/aeroporto/{icaoCode}";

        try
        {
            var retorno = _restClimaTempo.GetAsync(url).Result;

            if (retorno.Contains("undefined") || retorno.Contains("Erro"))
                throw new Exception("O Aeroporto não consta na API.");

            var retornoDeserialize = JsonConvert.DeserializeObject<PrevisaoClimaAeroportoDTO>(retorno);

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

            var entidadePrevisaoAeroporto = PrevisaoAeroporto.ToEntityPrevisaoAeroporto(climaAeroportoResponse, retornoDeserialize.codigo_icao);

            if (entidadePrevisaoAeroporto != null)
                _previsaoAeroportoRepository.Adicionar(entidadePrevisaoAeroporto);

            _logger.LogInformation($"Fim da busca pelo clima do aeroporto com icaoCode = {icaoCode}.");

            return climaAeroportoResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erro ao buscar clima do aeroporto com icaoCode = {icaoCode}.");

            await RegistrarLogNoBanco(ex,
                isCidade: false,
                identificacao: icaoCode);

            throw new Exception($"Ocorreu um erro ao buscar o clima do aeroporto com icaoCode = {icaoCode}. {ex.Message}");
        }
    }

    private Clima ToEntityClima(ClimaDTO climaDTO)
    {
        return Clima.ToEntity(climaDTO);
    }

    private async Task RegistrarLogNoBanco(Exception exception, bool isCidade, string identificacao)
    {
        var log = new Log
        {
            Timestamp = DateTimeOffset.Now,
            Level = "Error",
            Message = exception.Message,
        };

        if (isCidade)
            log.Message += $" (Erro ao buscar cidade com código = {identificacao})";
        else
            log.Message += $" (Erro ao buscar aeroporto com código = {identificacao})";

        _logRepository.Adicionar(log);
    }
}
