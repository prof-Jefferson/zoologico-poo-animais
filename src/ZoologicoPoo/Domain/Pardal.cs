using System;

namespace ZoologicoPoo.Domain;

public class Pardal : Ave, IVoador, IOviparo
{
    public Pardal(string nome, DateTime dataNascimento)
        : base(nome, dataNascimento)
    {
    }

    public override void EmitirSom()
    {
        Console.WriteLine($"{Nome}: piu piu!");
    }

    public override void Mover()
    {
        Console.WriteLine($"{Nome} está pulando entre os galhos.");
    }

    public void Voar()
    {
        Console.WriteLine($"{Nome} está voando.");
    }

    public void BotarOvo()
    {
        Console.WriteLine($"{Nome} está botando ovos no ninho.");
    }
}
