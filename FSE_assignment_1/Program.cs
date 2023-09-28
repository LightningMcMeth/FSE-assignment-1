using System.Globalization;
using System.Text.Json;

namespace MyNamespace
{
    
}

string jsonUrl = "https://sef.podkolzin.consulting/api/users/lastSeen?offset=";

using (HttpClient client = new HttpClient())
{
    try
    {
        Console.WriteLine($"Current time: {DateTime.UtcNow}");

        for (int i = 0; i <= 200; i += 20)
        {
            string jsonUrlOffest = offsetURL(i, jsonUrl);
            
            HttpResponseMessage response = await client.GetAsync(jsonUrlOffest);
            
            if (response.IsSuccessStatusCode)
            {
                
                string jsonContent = await response.Content.ReadAsStringAsync();
                
                var root = JsonSerializer.Deserialize<root>(jsonContent);

                foreach (var user in root.data)
                {
                    string userOnlineStatus = calculateLastSeenOnline(user);
                    Console.WriteLine(userOnlineStatus);
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


string offsetURL(int offset, string url)
{
    return url + offset;
}

string calculateLastSeenOnline(json_data user)
{
    if (!user.isOnline)
    {
        //DateTime lastSeenDate = DateTime.ParseExact(user.lastSeenDate, "yyyy-MM-ddTHH:mm:ss.fffffffzzz",
        //CultureInfo.InvariantCulture, DateTimeStyles.None);
        DateTime lastSeenDate = DateTime.Parse(user.lastSeenDate, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
        lastSeenDate = lastSeenDate.ToUniversalTime();
        
        DateTime startOfDay = DateTime.UtcNow.Date;

        TimeSpan startOfDayDT = lastSeenDate - startOfDay;
        TimeSpan timeDifference = DateTime.UtcNow - lastSeenDate;

        if (timeDifference <= TimeSpan.FromSeconds(30))
        {
            return user.nickname + " was online just now";
        }
        else if (timeDifference <= TimeSpan.FromMinutes(1))
        {
            return user.nickname + " was online less than a minute ago";
        }
        else if (timeDifference <= TimeSpan.FromHours(1))
        {
            return user.nickname + " was online a couple of minutes ago";
        }
        else if (timeDifference <= TimeSpan.FromHours(2))
        {
            return user.nickname + " was online an hour ago";
        }
        else if (timeDifference <= TimeSpan.FromHours(2) && timeDifference <= startOfDayDT)
        {
            return user.nickname + " was online today";
        }
        else if (timeDifference >= TimeSpan.FromHours(2) && timeDifference >= startOfDayDT)
        {
            return user.nickname + " was online just now";
        }
        else if (timeDifference <= TimeSpan.FromDays(7))
        {
            return user.nickname + " was online this week";
        }
        else
        {
            return user.nickname + " was online a long time ago";
        }

    }
    else
    {
        Console.WriteLine($"{user.nickname} is online");
        return user.nickname + "is online";
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