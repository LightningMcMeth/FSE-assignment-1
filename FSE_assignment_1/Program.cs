using System;
using System.Net.Http;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using System.Text.Json;

string jsonUrl = "https://sef.podkolzin.consulting/api/users/lastSeen?offset=";

using (HttpClient client = new HttpClient())
{
    try
    {
        Console.WriteLine($"Current time: {DateTime.UtcNow}");

        //TimeZoneInfo KyivTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Kiev");     //I hate that Kyiv has to be spelt incorrectly
        //DateTime currentKyivTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, KyivTimeZone);
        
        for (int i = 0; i <= 200; i += 20)
        {
            string jsonUrlOffest = jsonUrl + i.ToString();
            
            HttpResponseMessage response = await client.GetAsync(jsonUrlOffest);
            
            if (response.IsSuccessStatusCode)
            {
                
                string jsonContent = await response.Content.ReadAsStringAsync();
                
                var root = JsonSerializer.Deserialize<root>(jsonContent);

                foreach (var user in root.data)
                {
                    //we only go in here if the user is not online
                    if (!user.isOnline)
                    {
                        DateTime lastSeenDate = DateTime.Parse(user.lastSeenDate);
                        DateTime startOfDay = DateTime.UtcNow.Date;

                        TimeSpan startOfDayDT = lastSeenDate - startOfDay;
                        TimeSpan timeDifference = DateTime.UtcNow - lastSeenDate;

                        if (timeDifference <= TimeSpan.FromSeconds(30))
                        {
                            Console.WriteLine($"{user.nickname} was online just now");
                        }
                        else if (timeDifference <= TimeSpan.FromMinutes(1))
                        {
                            Console.WriteLine($"{user.nickname} was online less than a minute ago");
                        }
                        else if (timeDifference <= TimeSpan.FromHours(1))
                        {
                            Console.WriteLine($"{user.nickname} was online a couple of minutes ago");
                        }
                        else if (timeDifference <= TimeSpan.FromHours(2))
                        {
                            Console.WriteLine($"{user.nickname} was online an hour ago");
                        }
                        else if (timeDifference <= TimeSpan.FromHours(2) && timeDifference <= startOfDayDT)
                        {
                            Console.WriteLine($"{user.nickname} was online today");
                        }
                        else if (timeDifference >= TimeSpan.FromHours(2) && timeDifference >= startOfDayDT)
                        {
                            Console.WriteLine($"{user.nickname} was online just now");
                        }
                        else if (timeDifference <= TimeSpan.FromDays(7))
                        {
                            Console.WriteLine($"{user.nickname} was online this week");
                        }
                        else
                        {
                            Console.WriteLine($"{user.nickname} was online a long time ago");
                        }
                        
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