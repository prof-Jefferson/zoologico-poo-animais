namespace ZoologicoPoo.Domain;

public interface IAnimalDomesticado
{
    string Dono { get; }
    void Brincar();
}

public interface IVoador
{
    void Voar();
}

public interface IOviparo
{
    void BotarOvo();
}
