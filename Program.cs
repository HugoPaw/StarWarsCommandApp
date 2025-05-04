using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

// === MODELL für API ===
public class QuoteResponse
{
    public string StarWarsQuote { get; set; }
}

// === COMMAND PATTERN ===
public interface ICommand
{
    void Execute();
}

public class YodaCommand : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Yoda-Strategie aktiviert: Nutze die Macht mit Weisheit und Geduld.");
    }
}

public class VaderCommand : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Vader-Strategie aktiviert: Angriff, Wut, Kontrolle!");
    }
}

public class LukeCommand : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Luke-Strategie aktiviert: Balance zwischen Licht und Dunkel.");
    }
}

// === INVOKER ===
public class StrategyInvoker
{
    private ICommand _command;

    public void SetCommand(ICommand command)
    {
        _command = command;
    }

    public void ExecuteCommand()
    {
        _command?.Execute();
    }
}

// === API SERVICE ===
public class QuoteService
{
    private static readonly HttpClient _httpClient = new();

    public async Task<string> GetRandomQuoteAsync()
    {
        try
        {
            var response = await _httpClient.GetStringAsync("https://swquotesapi.digitaljedi.dk/api/SWQuote/RandomStarWarsQuote");
            var quote = JsonSerializer.Deserialize<QuoteResponse>(response);
            return quote?.StarWarsQuote ?? "Die Macht schweigt.";
        }
        catch
        {
            return "Zitat konnte nicht geladen werden. Der Imperator blockiert das Netz.";
        }
    }
}

// === MAIN APP ===
public class Program
{
    public static async Task Main()
    {
        Console.WriteLine("Willkommen zu 'Use the Command, Luke!'\n");
        Console.WriteLine("Wähle deinen Kampfstil:");
        Console.WriteLine("1 - Yoda\n2 - Darth Vader\n3 - Luke Skywalker");
        Console.Write("> ");
        var input = Console.ReadLine();

        var invoker = new StrategyInvoker();

        switch (input)
        {
            case "1": invoker.SetCommand(new YodaCommand()); break;
            case "2": invoker.SetCommand(new VaderCommand()); break;
            case "3": invoker.SetCommand(new LukeCommand()); break;
            default:
                Console.WriteLine("Unbekannter Pfad – ein Sith vielleicht?");
                return;
        }

        Console.WriteLine("\nStrategie wird ausgeführt:");
        invoker.ExecuteCommand();

        Console.WriteLine("\nLade Zitat aus der Macht...");
        var quoteService = new QuoteService();
        var quote = await quoteService.GetRandomQuoteAsync();

        Console.WriteLine($"\n🧠 Zitat der Macht:\n\"{quote}\"");
    }
}
