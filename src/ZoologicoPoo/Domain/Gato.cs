using System;

namespace ZoologicoPoo.Domain;

public class Gato : Mamifero, IAnimalDomesticado
{
    public string Dono { get; }

    public Gato(string nome, DateTime dataNascimento, string dono)
        : base(nome, dataNascimento)
    {
        Dono = dono;
    }

    public override void EmitirSom()
    {
        Console.WriteLine($"{Nome}: miau miau!");
    }

    public override void Mover()
    {
        Console.WriteLine($"{Nome} está andando silenciosamente.");
    }

    public void Brincar()
    {
        Console.WriteLine($"{Nome} está brincando com um novelo de lã.");
    }
}
