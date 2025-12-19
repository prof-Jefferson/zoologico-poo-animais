using System;

namespace ZoologicoPoo.Domain;

public class Tilapia : Peixe, IOviparo
{
    public Tilapia(string nome, DateTime dataNascimento)
        : base(nome, dataNascimento)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine($"{Nome}: glu glu glu...");
    }

    public override void Mover()
    {
        Console.WriteLine($"{Nome} está nadando.");
    }

    public void BotarOvo()
    {
        Console.WriteLine($"{Nome} está botando ovos na água.");
    }
}
