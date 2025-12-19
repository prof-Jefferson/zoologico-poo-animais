using System;

namespace ZoologicoPoo.Persistence;

internal class AnimalDto
{
    public string Tipo { get; set; } = string.Empty; // "Gato", "Cachorro", "Tilapia", "Pardal"
    public string Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string? Dono { get; set; } // só faz sentido para animais domesticados
}
