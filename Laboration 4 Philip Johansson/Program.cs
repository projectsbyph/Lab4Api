using Laboration_4_Philip_Johansson;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args) // main -metod för att köra asynkron kod
    {
        Run().GetAwaiter().GetResult();
    }

    static async Task Run() // Använder async för att kunna använda await inuti metoden, liknande föreläsningen fast uppdelad i flera metoder
    {
        await ShowDotnetReposAsync(); // await för att vänta på att metoden ska bli klar

        Console.WriteLine();          // tom rad

        await ShowMontvaleInfoAsync(); // denna metod gör vi i nästa steg

        Console.WriteLine();
        Console.WriteLine("Klar! Tryck på valfri tangent för att avsluta.");
        Console.ReadKey();
    }

    static async Task ShowDotnetReposAsync() // metod för att hämta och visa .NET-repos från GitHub
    {
        using (HttpClient client = new HttpClient()) // skapar en HttpClient för att göra HTTP-anrop
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Lab4ApiApp"); // GitHub API kräver en User-Agent header

            string url = "https://api.github.com/orgs/dotnet/repos"; // URL för att hämta .NET-repos

            try
            {
                HttpResponseMessage response = await client.GetAsync(url); // gör ett asynkront GET-anrop

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync(); // läser svaret som en sträng

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    List<Repo> repos = JsonSerializer.Deserialize<List<Repo>>(json, options); // deserialiserar JSON-strängen till en lista av Repo-objekt

                    // Skriv ut snyggt
                    foreach (var repo in repos) // loopar igenom varje repo och skriver ut information
                    {
                        Console.WriteLine($"Name: {repo.Name}");
                        Console.WriteLine($"Homepage: {(string.IsNullOrEmpty(repo.Homepage) ? "-" : repo.Homepage)}");
                        Console.WriteLine($"GitHub: {repo.HtmlUrl}");
                        Console.WriteLine($"Description: {repo.Description}");
                        Console.WriteLine($"Watchers: {repo.Watchers}");
                        Console.WriteLine($"Last push: {repo.PushedAt}");
                        Console.WriteLine(new string('-', 40));
                    }
                }
                else
                {
                    Console.WriteLine("Något gick fel. Statuskod: " + response.StatusCode);
                }
            }
            catch (Exception ex) // fångar eventuella fel vid anropet
            {
                Console.WriteLine("Fel vid anrop till API: " + ex.Message);
            }

        }
    }

    static async Task ShowMontvaleInfoAsync() // metod för att hämta och visa information om Montvale från Zippopotam
    {
        using (HttpClient client = new HttpClient()) // skapar en HttpClient för att göra HTTP-anrop
        {
            string url = "http://api.zippopotam.us/us/07645";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync(); // läser svaret som en sträng
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    ZippopotamResponse zipInfo = JsonSerializer.Deserialize<ZippopotamResponse>(json, options); // deserialiserar JSON-strängen till ett ZippopotamResponse-objekt

                    // Skriv ut snyggt
                    Console.WriteLine($"Post Code: {zipInfo.PostCode}");
                    Console.WriteLine($"Country: {zipInfo.Country} ({zipInfo.CountryAbbreviation})");
                    foreach (var place in zipInfo.Places) // loopar igenom varje plats och skriver ut information
                    {
                        Console.WriteLine($"Place Name: {place.PlaceName}");
                        Console.WriteLine($"State: {place.State} ({place.StateAbbreviation})");
                        Console.WriteLine($"Latitude: {place.Latitude}");
                        Console.WriteLine($"Longitude: {place.Longitude}");
                        Console.WriteLine(new string('-', 40));
                    }
                }
                else
                {
                    Console.WriteLine("Något gick fel. Statuskod: " + response.StatusCode);
                }
            }
            catch (Exception ex) // fångar eventuella fel vid anropet
            {
                Console.WriteLine("Fel vid anrop till Zippopotam: " + ex.Message);
            }

        }
    }
}






