using System;
using System.Collections.Generic;
using System.Linq;

namespace ZoologicoPoo.Domain;

public class Zoologico
{
    private readonly List<Animal> _animais = new();

    public IReadOnlyCollection<Animal> Animais => _animais.AsReadOnly();

    public void Adicionar(Animal animal)
    {
        if (animal == null) throw new ArgumentNullException(nameof(animal));
        _animais.Add(animal);
    }

    public bool Remover(string nomeAnimal)
    {
        if (string.IsNullOrWhiteSpace(nomeAnimal))
            return false;

        var animal = _animais.FirstOrDefault(a =>
            string.Equals(a.Nome, nomeAnimal, StringComparison.OrdinalIgnoreCase));

        if (animal is null)
            return false;

        return _animais.Remove(animal);
    }

    public Animal? BuscarPorNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return null;

        return _animais.FirstOrDefault(a =>
            string.Equals(a.Nome, nome, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<IAnimalDomesticado> ListarAnimaisDomesticos()
    {
        // OfType faz o filtro e o cast automático
        return _animais.OfType<IAnimalDomesticado>();
    }

    public IEnumerable<Animal> ListarAnimaisPorHabitat(TipoHabitat habitat)
    {
        return _animais.Where(a => a.Habitat == habitat);
    }

    public IEnumerable<IGrouping<Type, Animal>> ObterAnimaisAgrupadosPorTipo()
    {
        // Agrupa por tipo concreto (Gato, Cachorro, Tilapia, Pardal, etc.)
        return _animais.GroupBy(a => a.GetType());
    }
}
