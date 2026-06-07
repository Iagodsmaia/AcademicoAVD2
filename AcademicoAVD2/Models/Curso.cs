namespace AcademicoAVD2.Models;

public class Curso
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int CargaHoraria { get; set; }
    public long? DepartamentoId { get; set; }
    public Departamento? Departamento { get; set; }
}
