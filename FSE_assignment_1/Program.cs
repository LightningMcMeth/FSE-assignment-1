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
                
                var root = JsonSerializer.Deserialize<root>(jsonContent);

                //var gamer = root.data[1].nickname;

                foreach (var user in root.data)
                {
                    //we only go in here if the user is not online
                    if (!user.isOnline)
                    {
                        DateTime lastSeenDate = DateTime.Parse(user.lastSeenDate);
                        
                        
                    }
                    else
                    {
                        Console.WriteLine($"{user.nickname} is online");
                    }
                }

            }
            else
            {
                Console.WriteLine($"Failed to retrieve JSON file. Cringe. Status code: {response.StatusCode}");
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
    public string userId { get; set; }
    public string nickname { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string registrationDate { get; set; }
    public string lastSeenDate { get; set; }
    public bool isOnline { get; set; }
}

public class root
{
    public int total { get; set; }
    public List<json_data> data { get; set; }
}