using AcademicoAVD2.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademicoAVD2.Data;

public class AcademicoContext : DbContext
{
    public AcademicoContext(DbContextOptions<AcademicoContext> options) : base(options) { }

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Instituicao> Instituicoes { get; set; }
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<Curso> Cursos { get; set; }
}
