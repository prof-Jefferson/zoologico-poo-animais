using System;

namespace ZoologicoPoo.Domain;

public abstract class Mamifero : Animal
{
    protected Mamifero(string nome, DateTime dataNascimento)
        : base(nome, dataNascimento, TipoHabitat.Terrestre)
    {
    }
}
