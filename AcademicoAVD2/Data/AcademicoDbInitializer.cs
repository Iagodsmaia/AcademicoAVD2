using AcademicoAVD2.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademicoAVD2.Data;

public static class AcademicoDbInitializer
{
    public static void Initialize(AcademicoContext context)
    {
        context.Database.Migrate();

        if (context.Alunos.Any()) return;

        var alunos = new Aluno[]
        {
            new() {
                Nome = "João Silva",
                Email = "joao@email.com",
                Telefone = "(11) 98765-4321",
                Endereco = "Rua das Flores, 100",
                Complemento = "Apto 1",
                Bairro = "Centro",
                Municipio = "São Paulo",
                Uf = "SP",
                Cep = "01001-000"
            }
        };
        context.Alunos.AddRange(alunos);

        var instituicoes = new Instituicao[]
        {
            new() { Nome = "Universidade Federal", Endereco = "Av. Universitária, 1" },
            new() { Nome = "Instituto Federal", Endereco = "Rua Técnica, 200" }
        };
        context.Instituicoes.AddRange(instituicoes);
        context.SaveChanges();

        var departamentos = new Departamento[]
        {
            new() { Nome = "Ciência da Computação", InstituicaoId = instituicoes[0].InstituicaoID },
            new() { Nome = "Engenharia Elétrica", InstituicaoId = instituicoes[1].InstituicaoID }
        };
        context.Departamentos.AddRange(departamentos);
        context.SaveChanges();

        var cursos = new Curso[]
        {
            new() { Nome = "Sistemas de Informação", CargaHoraria = 3200, DepartamentoId = departamentos[0].Id }
        };
        context.Cursos.AddRange(cursos);
        context.SaveChanges();
    }
}
