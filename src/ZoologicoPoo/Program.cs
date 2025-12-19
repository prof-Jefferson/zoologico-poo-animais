using System;
using System.IO;
using ZoologicoPoo.Domain;
using ZoologicoPoo.Persistence;
using System.Globalization;

namespace ZoologicoPoo;

internal class Program
{
    static void Main(string[] args)
    {
        var caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "animais.json");
        var repositorio = new RepositorioAnimaisJson(caminhoArquivo);
        var zoologico = new Zoologico();

        // 1) Carregar dados já salvos (se houver)
        foreach (var animal in repositorio.Carregar())
        {
            zoologico.Adicionar(animal);
        }

        // 2) Se estiver vazio, adiciona alguns animais padrão
        if (zoologico.Animais.Count == 0)
        {
            zoologico.Adicionar(new Gato("Luna", new DateTime(2022, 5, 10), "Maria"));
            zoologico.Adicionar(new Cachorro("Rex", new DateTime(2021, 3, 2), "João"));
            zoologico.Adicionar(new Tilapia("Tilápia Azul", new DateTime(2023, 1, 1)));
            zoologico.Adicionar(new Pardal("Pardalzinho", new DateTime(2022, 9, 15)));
        }

        // 3) Loop principal de menu
        while (true)
        {
            Console.WriteLine("=== ZOOLÓGICO POO ===");
            Console.WriteLine("1 - Listar todos os animais");
            Console.WriteLine("2 - Listar animais domésticos");
            Console.WriteLine("3 - Listar animais por habitat");
            Console.WriteLine("4 - Agrupar animais por tipo");
            Console.WriteLine("5 - Remover animal por nome");
            Console.WriteLine("6 - Cadastrar novo animal");
            Console.WriteLine("0 - Sair");

            var opcao = Console.ReadLine()?.Trim();

            if (opcao == "0")
            {
                // Salva antes de sair
                repositorio.Salvar(zoologico.Animais);
                Console.WriteLine("\nEstado do zoológico salvo em arquivo. Encerrando...");
                break;
            }

            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    ListarTodos(zoologico);
                    break;

                case "2":
                    ListarDomesticos(zoologico);
                    break;

                case "3":
                    ListarPorHabitat(zoologico);
                    break;

                case "4":
                    ListarAgrupadosPorTipo(zoologico);
                    break;

                case "5":
                    RemoverPorNome(zoologico);
                    break;
                
                case "6":
                    CadastrarAnimal(zoologico);
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            Console.WriteLine("\nPressione ENTER para continuar...");
            Console.ReadLine();
        }
    }

    private static void ListarTodos(Zoologico zoologico)
    {
        Console.WriteLine("=== TODOS OS ANIMAIS ===");
        if (zoologico.Animais.Count == 0)
        {
            Console.WriteLine("Nenhum animal cadastrado.");
            return;
        }

        foreach (var animal in zoologico.Animais)
        {
            Console.WriteLine(animal);
        }
    }

    private static void ListarDomesticos(Zoologico zoologico)
    {
        Console.WriteLine("=== ANIMAIS DOMÉSTICOS ===");

        var domesticos = zoologico.ListarAnimaisDomesticos();

        var encontrou = false;
        foreach (var d in domesticos)
        {
            encontrou = true;
            Console.WriteLine($"{d.Dono} é dono de {d.GetType().Name}");
        }

        if (!encontrou)
        {
            Console.WriteLine("Nenhum animal doméstico cadastrado.");
        }
    }

    private static void ListarPorHabitat(Zoologico zoologico)
    {
        Console.WriteLine("=== LISTAR POR HABITAT ===");
        Console.WriteLine("0 - Terrestre");
        Console.WriteLine("1 - Aquático");
        Console.WriteLine("2 - Aéreo");
        Console.Write("Informe o código do habitat: ");

        var entrada = Console.ReadLine();

        if (!int.TryParse(entrada, out var codigo) ||
            codigo < 0 || codigo > 2)
        {
            Console.WriteLine("Habitat inválido.");
            return;
        }

        var habitat = (TipoHabitat)codigo;

        var animais = zoologico.ListarAnimaisPorHabitat(habitat);

        Console.WriteLine($"\nAnimais no habitat {habitat}:");
        var encontrou = false;
        foreach (var animal in animais)
        {
            encontrou = true;
            Console.WriteLine(animal);
        }

        if (!encontrou)
        {
            Console.WriteLine("Nenhum animal encontrado nesse habitat.");
        }
    }

    private static void ListarAgrupadosPorTipo(Zoologico zoologico)
    {
        Console.WriteLine("=== ANIMAIS AGRUPADOS POR TIPO ===");

        var grupos = zoologico.ObterAnimaisAgrupadosPorTipo();

        var encontrouGrupo = false;

        foreach (var grupo in grupos)
        {
            encontrouGrupo = true;
            Console.WriteLine($"\n[{grupo.Key.Name}]");
            foreach (var animal in grupo)
            {
                Console.WriteLine($" - {animal}");
            }
        }

        if (!encontrouGrupo)
        {
            Console.WriteLine("Nenhum animal cadastrado para agrupar.");
        }
    }

    private static void RemoverPorNome(Zoologico zoologico)
    {
        Console.Write("Digite o nome do animal a remover: ");
        var nome = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome inválido.");
            return;
        }

        var removido = zoologico.Remover(nome);

        Console.WriteLine(removido
            ? $"Animal '{nome}' removido com sucesso."
            : $"Animal '{nome}' não encontrado.");
    }

    private static void CadastrarAnimal(Zoologico zoologico)
    {
        Console.WriteLine("=== CADASTRO DE NOVO ANIMAL ===");
        Console.WriteLine("1 - Gato");
        Console.WriteLine("2 - Cachorro");
        Console.WriteLine("3 - Tilápia");
        Console.WriteLine("4 - Pardal");
        Console.Write("Escolha o tipo (1-4): ");

        var tipoOpcao = Console.ReadLine()?.Trim();

        Console.Write("Nome do animal: ");
        var nome = Console.ReadLine()?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome inválido. Cadastro cancelado.");
            return;
        }

        Console.Write("Data de nascimento (dd/mm/aaaa): ");
        var dataStr = Console.ReadLine();

        if (!DateTime.TryParseExact(
                dataStr,
                "dd/MM/yyyy",
                new CultureInfo("pt-BR"),
                DateTimeStyles.None,
                out var dataNascimento))
        {
            Console.WriteLine("Data inválida. Cadastro cancelado.");
            return;
        }

        Animal? novo = null;

        switch (tipoOpcao)
        {
            case "1": // Gato
                Console.Write("Nome do dono: ");
                var donoGato = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(donoGato))
                {
                    donoGato = "Dono desconhecido";
                }
                novo = new Gato(nome, dataNascimento, donoGato);
                break;

            case "2": // Cachorro
                Console.Write("Nome do dono: ");
                var donoCachorro = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(donoCachorro))
                {
                    donoCachorro = "Dono desconhecido";
                }
                novo = new Cachorro(nome, dataNascimento, donoCachorro);
                break;

            case "3": // Tilápia
                novo = new Tilapia(nome, dataNascimento);
                break;

            case "4": // Pardal
                novo = new Pardal(nome, dataNascimento);
                break;

            default:
                Console.WriteLine("Tipo inválido. Cadastro cancelado.");
                return;
        }

        zoologico.Adicionar(novo);
        Console.WriteLine($"Animal '{nome}' cadastrado com sucesso!");
    }
}
