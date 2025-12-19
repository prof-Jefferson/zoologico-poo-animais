using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ZoologicoPoo.Domain;

namespace ZoologicoPoo.Persistence;

public class RepositorioAnimaisJson
{
    private readonly string _caminhoArquivo;
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    public RepositorioAnimaisJson(string caminhoArquivo)
    {
        _caminhoArquivo = caminhoArquivo;
    }

    public void Salvar(IEnumerable<Animal> animais)
    {
        // Converte os animais do domínio para DTOs "achatados"
        var listaDto = animais
            .Select(a => new AnimalDto
            {
                Tipo = a switch
                {
                    Gato      => "Gato",
                    Cachorro  => "Cachorro",
                    Tilapia   => "Tilapia",
                    Pardal    => "Pardal",
                    _         => a.GetType().Name
                },
                Nome = a.Nome,
                DataNascimento = a.DataNascimento,
                Dono = a is IAnimalDomesticado domestico ? domestico.Dono : null
            })
            .ToList();

        var json = JsonSerializer.Serialize(listaDto, _options);

        // Garante que o diretório existe
        var dir = Path.GetDirectoryName(_caminhoArquivo);
        if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        File.WriteAllText(_caminhoArquivo, json);
    }

    public List<Animal> Carregar()
    {
        if (!File.Exists(_caminhoArquivo))
        {
            return new List<Animal>();
        }

        var json = File.ReadAllText(_caminhoArquivo);

        List<AnimalDto>? listaDto;
        try
        {
            listaDto = JsonSerializer.Deserialize<List<AnimalDto>>(json, _options);
        }
        catch
        {
            // Se o arquivo estiver corrompido ou com formato estranho, devolve vazio
            return new List<Animal>();
        }

        if (listaDto is null)
        {
            return new List<Animal>();
        }

        var animais = new List<Animal>();

        foreach (var dto in listaDto)
        {
            Animal? animal = dto.Tipo switch
            {
                "Gato" =>
                    new Gato(dto.Nome, dto.DataNascimento, dto.Dono ?? "Dono desconhecido"),

                "Cachorro" =>
                    new Cachorro(dto.Nome, dto.DataNascimento, dto.Dono ?? "Dono desconhecido"),

                "Tilapia" =>
                    new Tilapia(dto.Nome, dto.DataNascimento),

                "Pardal" =>
                    new Pardal(dto.Nome, dto.DataNascimento),

                _ => null
            };

            if (animal != null)
            {
                animais.Add(animal);
            }
        }

        return animais;
    }
}
