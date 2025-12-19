using System;

namespace ZoologicoPoo.Domain;

public abstract class Peixe : Animal
{
    protected Peixe(string nome, DateTime dataNascimento)
        : base(nome, dataNascimento, TipoHabitat.Aquatico)
    {
    }
}
