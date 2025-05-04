# StarWarsCommandApp

Ein kleines, aber C#-Konsolenprojekt zum May the 4th – demonstriert das Command Pattern anhand verschiedener Lichtschwert-Stile und ruft echte Star-Wars-Zitate von einer öffentlichen API ab.

---

##  Vorschau

Willkommen zu 'Use the Command, Luke!'

Wähle deinen Kampfstil:
1 - Yoda  
2 - Darth Vader  
3 - Luke Skywalker

Strategie wird ausgeführt:  
Yoda-Strategie aktiviert: Nutze die Macht mit Weisheit und Geduld.

Lade Zitat aus der Macht...

 **Zitat der Macht:**  
"The Force is strong with this one."

---

##  API-Erklärung: *SW Quotes API*

Dieses Projekt nutzt die öffentliche API:

🔗 [https://swquotesapi.digitaljedi.dk/api/SWQuote/RandomStarWarsQuote](https://swquotesapi.digitaljedi.dk/api/SWQuote/RandomStarWarsQuote)

Sie liefert ein zufälliges Star-Wars-Zitat als JSON-Antwort, z. B.:

```json
{
  "starWarsQuote": "I find your lack of faith disturbing."
}
```

---

##  Verwendung im Code

Mit `HttpClient` wird ein GET-Request an die URL geschickt
Die JSON-Antwort wird mit `System.Text.Json` deserialisiert
Das Zitat wird dann in der Konsole ausgegeben

```csharp
public class QuoteResponse
{
    public string StarWarsQuote { get; set; }
}

public class QuoteService
{
    private static readonly HttpClient _httpClient = new();

    public async Task<string> GetRandomQuoteAsync()
    {
        try
        {
            var response = await _httpClient.GetStringAsync(
                "https://swquotesapi.digitaljedi.dk/api/SWQuote/RandomStarWarsQuote"
            );

            var quote = JsonSerializer.Deserialize<QuoteResponse>(response);
            return quote?.StarWarsQuote ?? "Die Macht schweigt.";
        }
        catch
        {
            return "Zitat konnte nicht geladen werden. Der Imperator blockiert das Netz.";
        }
    }
}
```

