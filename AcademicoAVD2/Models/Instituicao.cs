namespace AcademicoAVD2.Models;

public class Instituicao
{
    public long InstituicaoID { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public ICollection<Departamento>? Departamentos { get; set; }
}
