using Agenda.Infrastructure.RestResources;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Agenda.WebApp.Controllers;
public class CepController : Controller
{
    private readonly ICepExternalService _cepExternalService;

    public CepController(ICepExternalService cepExternalService)
    {
        _cepExternalService = cepExternalService;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GetCep(string codigo)
    {
        try
        {
            var codigoFormatado = string.Format(CultureInfo.InvariantCulture, @"{0:00000\-000}",
                                 Convert.ToUInt64(codigo, CultureInfo.InvariantCulture));

            var cep = await _cepExternalService.GetAsync(codigoFormatado);

            return Ok(cep);
        }
        catch (FormatException)
        {
            return BadRequest("Cep no formato inválido");
        }

    }
}
