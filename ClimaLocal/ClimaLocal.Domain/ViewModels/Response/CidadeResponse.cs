using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimaLocal.Domain.ViewModels.Response;

public class CidadeResponse
{
    public string Nome { get; set; }
    public string Estado { get; set; }
    public int CodigoCidade { get; set; }
}
