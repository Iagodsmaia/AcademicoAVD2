using AcademicoAVD2.Data;
using AcademicoAVD2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AcademicoAVD2.Controllers;

public class DepartamentoController : Controller
{
    private readonly AcademicoContext _context;

    public DepartamentoController(AcademicoContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var departamentos = await _context.Departamentos
            .Include(d => d.Instituicao)
            .ToListAsync();
        return View(departamentos);
    }

    public IActionResult Create()
    {
        ViewBag.InstituicaoId = new SelectList(_context.Instituicoes, "InstituicaoID", "Nome");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,InstituicaoId")] Departamento departamento)
    {
        if (ModelState.IsValid)
        {
            _context.Add(departamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.InstituicaoId = new SelectList(_context.Instituicoes, "InstituicaoID", "Nome", departamento.InstituicaoId);
        return View(departamento);
    }

    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null) return NotFound();
        var departamento = await _context.Departamentos.FindAsync(id);
        if (departamento == null) return NotFound();
        ViewBag.InstituicaoId = new SelectList(_context.Instituicoes, "InstituicaoID", "Nome", departamento.InstituicaoId);
        return View(departamento);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, [Bind("Id,Nome,InstituicaoId")] Departamento departamento)
    {
        if (id != departamento.Id) return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(departamento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Departamentos.Any(d => d.Id == id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewBag.InstituicaoId = new SelectList(_context.Instituicoes, "InstituicaoID", "Nome", departamento.InstituicaoId);
        return View(departamento);
    }

    public async Task<IActionResult> Details(long? id)
    {
        if (id == null) return NotFound();
        var departamento = await _context.Departamentos
            .Include(d => d.Instituicao)
            .FirstOrDefaultAsync(d => d.Id == id);
        if (departamento == null) return NotFound();
        ViewBag.InstituicaoId = new SelectList(_context.Instituicoes, "InstituicaoID", "Nome", departamento.InstituicaoId);
        return View(departamento);
    }

    public async Task<IActionResult> Delete(long? id)
    {
        if (id == null) return NotFound();
        var departamento = await _context.Departamentos
            .Include(d => d.Instituicao)
            .FirstOrDefaultAsync(d => d.Id == id);
        if (departamento == null) return NotFound();
        return View(departamento);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var departamento = await _context.Departamentos.FindAsync(id);
        if (departamento != null) _context.Departamentos.Remove(departamento);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
