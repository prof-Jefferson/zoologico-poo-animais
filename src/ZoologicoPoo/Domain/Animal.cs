using System;

namespace ZoologicoPoo.Domain;

public abstract class Animal : IComSom, IMovimentacao
{
    public string Nome { get; }
    public DateTime DataNascimento { get; }
    public TipoHabitat Habitat { get; }

    public int Idade
    {
        get
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - DataNascimento.Year;

            if (DataNascimento.Date > hoje.AddYears(-idade))
            {
                idade--;
            }

            return idade;
        }
    }

    protected Animal(string nome, DateTime dataNascimento, TipoHabitat habitat)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        Habitat = habitat;
    }

    public abstract void EmitirSom();

    public virtual void Mover()
    {
        Console.WriteLine($"{Nome} está se movendo...");
    }

    public override string ToString()
    {
        return $"{Nome} ({Habitat}, {Idade} ano(s))";
    }
}
