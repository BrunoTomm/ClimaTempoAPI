using ClimaLocal.App.Interfaces;
using ClimaLocal.Domain.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;

namespace ClimaLocal.WebApi.Controllers;

/// <summary>
/// Controlador responsável por fornecer informações climáticas relacionadas a cidades e aeroportos.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ClimaController : ControllerBase
{
    private readonly IClimaApp _climaApp;

    /// <summary>
    /// Inicializa uma nova instância do controlador ClimaController.
    /// </summary>
    /// <param name="climaApp">A interface de aplicação climática.</param>
    public ClimaController(IClimaApp climaApp)
    {
        _climaApp = climaApp ?? throw new ArgumentNullException(nameof(climaApp));
    }

    /// <summary>
    /// Obtém uma lista de cidades com informações de código, úteis para pesquisas relacionadas ao clima da cidade.
    /// </summary>
    /// <returns>Uma lista de cidades com códigos.</returns>
    [HttpGet]
    [Route("retorna-cidades")]
    public async Task<ActionResult<List<CidadeResponse>>> RetornaCidadesAsync()
    {
        try
        {
            var cidades = await _climaApp.RetornaCidades();

            return Ok(cidades);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Erro na requisição HTTP: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao buscar cidades: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtém informações climáticas atuais para uma cidade específica.
    /// </summary>
    /// <param name="codigoCidade">O código da cidade para a qual deseja obter informações climáticas.</param>
    /// <returns>As informações climáticas da cidade especificada.</returns>
    [HttpGet]
    [Route("retorna-clima-cidade/{codigoCidade}")]
    public async Task<ActionResult<PrevisaoClimaCidadeResponse>> RetornaClimaCidadeAsync(int codigoCidade)
    {
        try
        {
            var climaCidade = await _climaApp.RetornaClimaCidade(codigoCidade);

            return Ok(climaCidade);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Erro na requisição HTTP: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao buscar clima da cidade: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtém informações climáticas atuais para um aeroporto específico.
    /// </summary>
    /// <param name="codigoAeroporto">O código do aeroporto para o qual deseja obter informações climáticas.</param>
    /// <returns>As informações climáticas do aeroporto especificado.</returns>
    [HttpGet]
    [Route("retorna-clima-aeroporto/{codigoAeroporto}")]
    public async Task<ActionResult<PrevisaoClimaAeroportoResponse>> RetornaClimaAeroportoAsync(string codigoAeroporto)
    {
        try
        {
            var climaAeroporto = await _climaApp.RetornaClimaAeroporto(codigoAeroporto);

            return Ok(climaAeroporto);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Erro na requisição HTTP: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao buscar clima do aeroporto: {ex.Message}");
        }
    }
}
