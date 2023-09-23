using ClimaLocal.App.Services;
using ClimaLocal.Client.Interfaces;
using ClimaLocal.Domain.Interfaces;
using ClimaLocal.Domain.ViewModels.Response;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace ClimaLocal.Test;


[TestFixture]
public class ClimaAppTests
{
    private ClimaApp _climaApp;
    private Mock<IRestClimaTempo> _restClimaTempoMock;
    private Mock<IPrevisaoCidadeRepository> _previsaoCidadeRepositoryMock;
    private Mock<IPrevisaoAeroportoRepository> _previsaoAeroportoRepositoryMock;
    private Mock<ILogRepository> _logRepositoryMock;
    private Mock<ILogger<ClimaApp>> _loggerMock;

    [SetUp]
    public void Setup()
    {
        _restClimaTempoMock = new Mock<IRestClimaTempo>();
        _previsaoCidadeRepositoryMock = new Mock<IPrevisaoCidadeRepository>();
        _previsaoAeroportoRepositoryMock = new Mock<IPrevisaoAeroportoRepository>();
        _logRepositoryMock = new Mock<ILogRepository>();
        _loggerMock = new Mock<ILogger<ClimaApp>>();

        _climaApp = new ClimaApp(
            _restClimaTempoMock.Object,
            _previsaoCidadeRepositoryMock.Object,
            _previsaoAeroportoRepositoryMock.Object,
            _loggerMock.Object,
            _logRepositoryMock.Object);
    }

    [Test]
    public void Deve_Retornar_Cidades()
    {
        // Arrange
        var mockApiResponse = "[{\"Nome\":\"Esteio\",\"Estado\":\"Rio grande do sul\",\"Id\":4750}, {\"Nome\":\"Canoas\",\"Estado\":\"Rio Grande do Sul\",\"Id\":5045}]";
        _restClimaTempoMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(mockApiResponse);

        // Act
        var resultTask = _climaApp.RetornaCidades(); 

        // Assert
        Assert.IsNotNull(resultTask.Result); 
        Assert.IsInstanceOf<List<CidadeResponse>>(resultTask.Result);
        Assert.AreEqual(2, resultTask.Result.Count);
    }


    [Test]
    public void Deve_Retornar_Clima_Cidade()
    {
        // Arrange
        var idCidade = 4750;
        var mockApiResponse = "{\"cidade\":\"Santa Fé de Minas\",\"estado\":\"MG\",\"atualizado_em\":\"2023-09-22\",\"clima\":[{\"data\":\"2023-09-23\",\"condicao\":\"pn\",\"condicao_desc\":\"Parcialmente Nublado\",\"min\":18,\"max\":36,\"indice_uv\":11}]}";
        _restClimaTempoMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(mockApiResponse);

        // Act
        var resultTask = _climaApp.RetornaClimaCidade(idCidade); 

        // Assert
        Assert.IsNotNull(resultTask.Result); 
        Assert.IsInstanceOf<PrevisaoClimaCidadeResponse>(resultTask.Result);
    }

    [Test]
    public void Deve_Retornar_Clima_Aeroporto()
    {
        // Arrange
        var icaoCode = "SBBH";
        var mockApiResponse = "{\"umidade\":34,\"visibilidade\":\">10000\",\"codigo_icao\":\"SBBH\",\"pressao_atmosferica\":1019,\"vento\":7,\"direcao_vento\":60,\"condicao\":\"ps\",\"condicao_desc\":\"Predomínio de Sol\",\"temp\":26,\"atualizado_em\":\"2023-09-22T11:00:00.793Z\"}";
        _restClimaTempoMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(mockApiResponse);

        // Act
        var resultTask = _climaApp.RetornaClimaAeroporto(icaoCode); 

        // Assert
        Assert.IsNotNull(resultTask.Result); 
        Assert.IsInstanceOf<PrevisaoClimaAeroportoResponse>(resultTask.Result);
    }

}

