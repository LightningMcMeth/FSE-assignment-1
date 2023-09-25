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
                // Read the JSON content as a string
                string jsonContent = await response.Content.ReadAsStringAsync();

                // Process the JSON data as needed
                // For example, you can deserialize it into objects using Newtonsoft.Json:
                // var data = JsonConvert.DeserializeObject<MyDataClass>(jsonContent);

                Console.WriteLine("JSON data retrieved successfully:");
                Console.WriteLine(jsonContent);
            }
            else
            {
                Console.WriteLine($"Failed to retrieve JSON. Status code: {response.StatusCode}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Uh oh, error: {ex.Message}");
    }
}
