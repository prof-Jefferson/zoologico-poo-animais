using System;
using ZoologicoPoo.Domain;

namespace ZoologicoPoo;

internal class Program
{
    static void Main(string[] args)
    {
        var zoo = new Zoologico();

        zoo.Adicionar(new Gato("Luna", new DateTime(2022, 5, 10), "Maria"));
        zoo.Adicionar(new Cachorro("Rex", new DateTime(2021, 3, 2), "João"));
        zoo.Adicionar(new Tilapia("Tilápia Azul", new DateTime(2023, 1, 1)));
        zoo.Adicionar(new Pardal("Pardalzinho", new DateTime(2022, 9, 15)));

        Console.WriteLine("=== Todos os animais ===");
        foreach (var a in zoo.Animais)
            Console.WriteLine(a);

        Console.WriteLine("\n=== Domésticos ===");
        foreach (var d in zoo.ListarAnimaisDomesticos())
            Console.WriteLine($"{d.Dono} é dono de {d.GetType().Name}");
    }
}
