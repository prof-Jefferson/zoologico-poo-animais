using System;
using System.IO;
using ZoologicoPoo.Domain;
using ZoologicoPoo.Persistence;
using ZoologicoPoo.Application;

namespace ZoologicoPoo;

internal class Program
{
    static void Main(string[] args)
    {
        var caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "animais.json");
        var repositorio = new RepositorioAnimaisJson(caminhoArquivo);
        var zoologico = new Zoologico();

        // Carrega animais já salvos (se houver)
        foreach (var animal in repositorio.Carregar())
        {
            zoologico.Adicionar(animal);
        }

        // Se estiver vazio, adiciona alguns animais padrão
        if (zoologico.Animais.Count == 0)
        {
            zoologico.Adicionar(new Gato("Luna", new DateTime(2022, 5, 10), "Maria"));
            zoologico.Adicionar(new Cachorro("Rex", new DateTime(2021, 3, 2), "João"));
            zoologico.Adicionar(new Tilapia("Tilápia Azul", new DateTime(2023, 1, 1)));
            zoologico.Adicionar(new Pardal("Pardalzinho", new DateTime(2022, 9, 15)));
        }

        // Cria e executa o menu de console
        var menu = new MenuZoologico(zoologico);
        menu.ExecutarLoop();

        // Salva o estado final do zoológico
        repositorio.Salvar(zoologico.Animais);

        Console.WriteLine("\nEstado do zoológico salvo. Até mais!");
    }
}
