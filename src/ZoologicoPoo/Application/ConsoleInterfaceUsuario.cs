using System;

namespace ZoologicoPoo.Application;

public class ConsoleInterfaceUsuario : IInterfaceUsuario
{
    public string LerTexto(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    public int LerInteiro(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var entrada = Console.ReadLine();

            if (int.TryParse(entrada, out var valor))
                return valor;

            Console.WriteLine("Valor inválido, tente novamente.");
        }
    }

    public void MostrarMensagem(string mensagem)
    {
        Console.WriteLine(mensagem);
    }
}
