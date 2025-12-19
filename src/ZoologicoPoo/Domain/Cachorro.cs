using System;

namespace ZoologicoPoo.Domain;

public class Cachorro : Mamifero, IAnimalDomesticado
{
    public string Dono { get; }

    public Cachorro(string nome, DateTime dataNascimento, string dono)
        : base(nome, dataNascimento)
    {
        Dono = dono;
    }

    public override void EmitirSom()
    {
        Console.WriteLine($"{Nome}: au au!");
    }

    public override void Mover()
    {
        Console.WriteLine($"{Nome} está correndo abanando o rabo.");
    }

    public void Brincar()
    {
        Console.WriteLine($"{Nome} está buscando a bolinha.");
    }
}
