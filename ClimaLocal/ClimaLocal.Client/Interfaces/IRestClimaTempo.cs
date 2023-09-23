using ClimaLocal.Domain.ViewModels.Response;
using System.Text;

namespace ClimaLocal.Client.Interfaces;

public interface IRestClimaTempo
{
    Task<string> GetAsync(string url);
}



