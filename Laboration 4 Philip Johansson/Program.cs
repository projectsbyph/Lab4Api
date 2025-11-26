using Laboration_4_Philip_Johansson;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Run().GetAwaiter().GetResult();
    }

    static async Task Run()
    {
        await ShowDotnetReposAsync();

        Console.WriteLine();          // tom rad

        await ShowMontvaleInfoAsync(); // denna metod gör vi i nästa steg

        Console.WriteLine();
        Console.WriteLine("Klar! Tryck på valfri tangent för att avsluta.");
        Console.ReadKey();
    }

    static async Task ShowDotnetReposAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Lab4ApiApp");

            string url = "https://api.github.com/orgs/dotnet/repos";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    List<Repo> repos = JsonSerializer.Deserialize<List<Repo>>(json, options);

                    // Skriv ut snyggt
                    foreach (var repo in repos)
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
            catch (Exception ex)
            {
                Console.WriteLine("Fel vid anrop till API: " + ex.Message);
            }

        }
    }

    static async Task ShowMontvaleInfoAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            string url = "http://api.zippopotam.us/us/07645";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    ZippopotamResponse zipInfo = JsonSerializer.Deserialize<ZippopotamResponse>(json, options);

                    // Skriv ut snyggt
                    Console.WriteLine($"Post Code: {zipInfo.PostCode}");
                    Console.WriteLine($"Country: {zipInfo.Country} ({zipInfo.CountryAbbreviation})");
                    foreach (var place in zipInfo.Places)
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
            catch (Exception ex)
            {
                Console.WriteLine("Fel vid anrop till Zippopotam: " + ex.Message);
            }

        }
    }
}






