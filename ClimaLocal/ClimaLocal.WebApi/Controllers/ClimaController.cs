using ClimaLocal.App.Interfaces;
using ClimaLocal.Domain.ViewModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClimaLocal.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ClimaController : ControllerBase
    {
        private readonly IClimaApp _climaApp;

        public ClimaController(IClimaApp climaApp)
        {
            _climaApp = climaApp;
        }

        /// <summary>
        /// Responsável por buscar cidades e retornar informações. Entre elas o código, importante para posterior busca relacionada ao clima da cidade.
        /// </summary>
        /// <returns>Uma lista de cidades</returns>
        [HttpGet]
        [Route("retorna-cidades")]
        public ActionResult<List<CidadeResponse>> RetornaCidades()
        {
            try
            {
                return Ok(_climaApp.RetornaCidades());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Busca o clima atual em uma determinada cidade.
        /// </summary>
        /// <returns>O clima de determinada cidade</returns>
        [HttpGet]
        [Route("retorna-clima-cidade/{codigoCidade}")]
        public ActionResult<PrevisaoClimaCidadeResponse> RetornaClimaCidade(int codigoCidade)
        {
            try
            {
                return Ok(_climaApp.RetornaClimaCidade(codigoCidade));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Busca o clima atual em um determinado aeroporto.
        /// </summary>
        /// <returns>O clima de determinado aeroporto</returns>
        [HttpGet]
        [Route("retorna-clima-aeroporto/{codigoAeroporto}")]
        public ActionResult<PrevisaoClimaAeroportoResponse> RetornaClimaAeroporto(string codigoAeroporto)
        {
            try
            {
                return Ok(_climaApp.RetornaClimaAeroporto(codigoAeroporto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
