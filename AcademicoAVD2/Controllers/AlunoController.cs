using AcademicoAVD2.Data;
using AcademicoAVD2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademicoAVD2.Controllers;

public class AlunoController : Controller
{
    private readonly AcademicoContext _context;

    public AlunoController(AcademicoContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Alunos.OrderBy(a => a.Nome).ToListAsync());
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nome,Email,Telefone,Endereco,Complemento,Bairro,Municipio,Uf,Cep")] Aluno aluno)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _context.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Não foi possível inserir os dados.");
        }
        return View(aluno);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var aluno = await _context.Alunos.FindAsync(id);
        if (aluno == null) return NotFound();
        return View(aluno);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("AlunoID,Nome,Email,Telefone,Endereco,Complemento,Bairro,Municipio,Uf,Cep")] Aluno aluno)
    {
        if (id != aluno.AlunoID) return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(aluno);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Alunos.Any(a => a.AlunoID == id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(aluno);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var aluno = await _context.Alunos.SingleOrDefaultAsync(a => a.AlunoID == id);
        if (aluno == null) return NotFound();
        return View(aluno);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var aluno = await _context.Alunos.SingleOrDefaultAsync(a => a.AlunoID == id);
        if (aluno == null) return NotFound();
        return View(aluno);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var aluno = await _context.Alunos.FindAsync(id);
        _context.Alunos.Remove(aluno!);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
