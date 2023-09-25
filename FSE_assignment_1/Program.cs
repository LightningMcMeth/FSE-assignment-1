using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

string jsonUrl = "https://sef.podkolzin.consulting/api/users/lastSeen?offset=";

using (HttpClient client = new HttpClient())
{
    try
    {
        
        for (int i = 0; i <= 200; i += 20)
        {
            string jsonUrlOffest = jsonUrl + i.ToString();
            
            HttpResponseMessage response = await client.GetAsync(jsonUrlOffest);
            
            if (response.IsSuccessStatusCode)
            {
                
                string jsonContent = await response.Content.ReadAsStringAsync();

                var users = JsonSerializer.Deserialize<json_data>(jsonContent);

            }
            else
            {
                Console.WriteLine($"Failed to retrieve JSON file. Status code: {response.StatusCode}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Uh oh, error: {ex.Message}");
    }
}

public class json_data
{
    public string userId { get; }
    public string nickname { get; }
    public string firstName { get; }
    public string lastName { get; }
    public string registrationDate { get; }
    public string lastSeenDate { get; }
    public bool isOnline { get; }
}

public class root
{
    public int total { get; set; }
    public List<json_data> data { get; set; }
}

//Console.WriteLine("JSON data retrieved successfully:");
//Console.WriteLine(jsonContent);