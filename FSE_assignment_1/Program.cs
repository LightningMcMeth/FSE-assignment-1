using System.Globalization;
using System.Text.Json;

namespace FSE_assignment_1
{
    public class lastSeenOnlineProgram
    {
        static async Task Main(string[] args)
        {
            string jsonUrl = "https://sef.podkolzin.consulting/api/users/lastSeen?offset=";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    DateTime currentTime = DateTime.UtcNow;
                    Console.WriteLine($"Current time: {currentTime}");

                    for (int i = 0; i <= 200; i += 20)
                    {
                        string jsonUrlOffset = offsetURL(i, jsonUrl);

                        HttpResponseMessage response = await client.GetAsync(jsonUrlOffset);

                        if (response.IsSuccessStatusCode)
                        {
                            string jsonContent = await response.Content.ReadAsStringAsync();
                            
                            var root = parseJsonData(jsonContent);

                            foreach (var user in root.data)
                            {
                                string userOnlineStatus = calculateLastSeenOnline(user, currentTime);
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
        }

        public static string offsetURL(int offset, string url)
        {
            return url + offset;
        }

        public static string calculateLastSeenOnline(JsonData user, DateTime currentTime)
        {
            if (!user.isOnline)
            {
                DateTime lastSeenDate = DateTime.Parse(user.lastSeenDate, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                lastSeenDate = lastSeenDate.ToUniversalTime();

                DateTime startOfDay = currentTime.Date;

                TimeSpan startOfDayDT = lastSeenDate - startOfDay;
                TimeSpan timeDifference = currentTime - lastSeenDate;

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
                    return user.nickname + " was online yesterday";
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
                return user.nickname + " is online";
            }
        }

        public static Root? parseJsonData(string jsonData)
        {
            return JsonSerializer.Deserialize<Root>(jsonData);
        }
    }
    
    //I know these are meant to be in separate files, which I have, but I couldn't get it to work
    //I think it's due to some solution naming issue, since I had to rename it.
    public class JsonData
    {
        public string userId { get; set; }
        public string nickname { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string registrationDate { get; set; }
        public string lastSeenDate { get; set; }
        public bool isOnline { get; set; }
    }

    public class Root
    {
        public int total { get; set; }
        public List<JsonData> data { get; set; }
    }
}