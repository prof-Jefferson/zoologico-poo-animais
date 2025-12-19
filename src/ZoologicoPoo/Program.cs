using System;
using System.IO;
using ZoologicoPoo.Domain;
using ZoologicoPoo.Persistence;

namespace ZoologicoPoo;

internal class Program
{
    static void Main(string[] args)
    {
        var caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "animais.json");
        var repo = new RepositorioAnimaisJson(caminhoArquivo);
        var zoo = new Zoologico();

        // Carregar animais salvos anteriormente
        foreach (var a in repo.Carregar())
        {
            zoo.Adicionar(a);
        }

        // Se estiver vazio, cria alguns padrões
        if (zoo.Animais.Count == 0)
        {
            zoo.Adicionar(new Gato("Luna", new DateTime(2022, 5, 10), "Maria"));
            zoo.Adicionar(new Cachorro("Rex", new DateTime(2021, 3, 2), "João"));
            zoo.Adicionar(new Tilapia("Tilápia Azul", new DateTime(2023, 1, 1)));
            zoo.Adicionar(new Pardal("Pardalzinho", new DateTime(2022, 9, 15)));
        }

        Console.WriteLine("=== Animais carregados no zoológico ===");
        foreach (var a in zoo.Animais)
        {
            Console.WriteLine(a);
        }

        // Salvar estado atual
        repo.Salvar(zoo.Animais);

        Console.WriteLine($"\nEstado salvo em: {caminhoArquivo}");
    }
}
