using AcademicoAVD2.Data;
using AcademicoAVD2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademicoAVD2.Controllers;

public class InstituicaoController : Controller
{
    private readonly AcademicoContext _context;

    public InstituicaoController(AcademicoContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Instituicoes.ToListAsync());
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("InstituicaoID,Nome,Endereco")] Instituicao instituicao)
    {
        if (ModelState.IsValid)
        {
            _context.Add(instituicao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(instituicao);
    }

    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null) return NotFound();
        var instituicao = await _context.Instituicoes.FindAsync(id);
        if (instituicao == null) return NotFound();
        return View(instituicao);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, [Bind("InstituicaoID,Nome,Endereco")] Instituicao instituicao)
    {
        if (id != instituicao.InstituicaoID) return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(instituicao);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Instituicoes.Any(i => i.InstituicaoID == id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(instituicao);
    }

    public async Task<IActionResult> Details(long? id)
    {
        if (id == null) return NotFound();
        var instituicao = await _context.Instituicoes.FirstOrDefaultAsync(i => i.InstituicaoID == id);
        if (instituicao == null) return NotFound();
        return View(instituicao);
    }

    public async Task<IActionResult> Delete(long? id)
    {
        if (id == null) return NotFound();
        var instituicao = await _context.Instituicoes.FirstOrDefaultAsync(i => i.InstituicaoID == id);
        if (instituicao == null) return NotFound();
        return View(instituicao);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var instituicao = await _context.Instituicoes.FindAsync(id);
        if (instituicao != null) _context.Instituicoes.Remove(instituicao);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
