using AcademicoAVD2.Data;
using AcademicoAVD2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AcademicoAVD2.Controllers;

public class CursoController : Controller
{
    private readonly AcademicoContext _context;

    public CursoController(AcademicoContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var cursos = await _context.Cursos
            .Include(c => c.Departamento)
            .ToListAsync();
        return View(cursos);
    }

    public IActionResult Create()
    {
        ViewBag.DepartamentoId = new SelectList(_context.Departamentos, "Id", "Nome");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,CargaHoraria,DepartamentoId")] Curso curso)
    {
        if (ModelState.IsValid)
        {
            _context.Add(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.DepartamentoId = new SelectList(_context.Departamentos, "Id", "Nome", curso.DepartamentoId);
        return View(curso);
    }

    public async Task<IActionResult> Edit(long? id)
    {
        if (id == null) return NotFound();
        var curso = await _context.Cursos.FindAsync(id);
        if (curso == null) return NotFound();
        ViewBag.DepartamentoId = new SelectList(_context.Departamentos, "Id", "Nome", curso.DepartamentoId);
        return View(curso);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, [Bind("Id,Nome,CargaHoraria,DepartamentoId")] Curso curso)
    {
        if (id != curso.Id) return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(curso);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cursos.Any(c => c.Id == id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewBag.DepartamentoId = new SelectList(_context.Departamentos, "Id", "Nome", curso.DepartamentoId);
        return View(curso);
    }

    public async Task<IActionResult> Details(long? id)
    {
        if (id == null) return NotFound();
        var curso = await _context.Cursos
            .Include(c => c.Departamento)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (curso == null) return NotFound();
        ViewBag.DepartamentoId = new SelectList(_context.Departamentos, "Id", "Nome", curso.DepartamentoId);
        return View(curso);
    }

    public async Task<IActionResult> Delete(long? id)
    {
        if (id == null) return NotFound();
        var curso = await _context.Cursos
            .Include(c => c.Departamento)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (curso == null) return NotFound();
        return View(curso);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        if (curso != null) _context.Cursos.Remove(curso);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
