using Agenda.Application.Services;
using Agenda.Domain.Contatos;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.WebApp.Controllers;
[System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S4144:Methods should not have identical implementations", Justification = "<Pending>")]
public class ContatoController : Controller
{
    private readonly IContatoService _pessooaService;

    public ContatoController(IContatoService pessooaService)
    {
        _pessooaService = pessooaService;
    }

    public async Task<IActionResult> Index()
    {
        var contatosViewModel = await _pessooaService.ObterContatosComCepAsync();
        return View(contatosViewModel);
    }

    [Route("details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var contato = await _pessooaService.ObterContatoComCepAsync(id);
        if (contato is null)
            return NotFound();

        return View(contato);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ContatoViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _pessooaService.CadastrarContatoAsync(model);

            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    [Route("edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var contato = await _pessooaService.ObterContatoComCepAsync(id);
        if (contato is null)
            return NotFound();

        return View(contato);
    }

    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ContatoViewModel model)
    {
        if (model?.Id != id)
            return BadRequest();

        if (ModelState.IsValid)
        {
            await _pessooaService.AlterarContatoAsync(model);

            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _pessooaService.DeletarContatoAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
