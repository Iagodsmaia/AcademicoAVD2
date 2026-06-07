using System.ComponentModel;

namespace AcademicoAVD2.Models;

public class Departamento
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    [DisplayName("Instituicao")]
    public long? InstituicaoId { get; set; }
    public Instituicao? Instituicao { get; set; }
}
