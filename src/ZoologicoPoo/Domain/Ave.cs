using System;

namespace ZoologicoPoo.Domain;

public abstract class Ave : Animal
{
    protected Ave(string nome, DateTime dataNascimento)
        : base(nome, dataNascimento, TipoHabitat.Aereo)
    {
    }
}
