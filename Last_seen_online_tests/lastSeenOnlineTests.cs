using FSE_assignment_1;

namespace LastSeenOnlineTests;


public class LastSeenOnlineTests
{
    [Fact]
    public void offset_IsJoinedWithValidValue_true()
    {
        string jsonUrl = "https://sef.podkolzin.consulting/api/users/lastSeen?offset=";
        string jsonUrlWithOffset = "https://sef.podkolzin.consulting/api/users/lastSeen?offset=20";

        string jsonUrlOffset = LastSeenOnlineProgram.offsetURL(20, jsonUrl);
        
        Assert.Equal(jsonUrlWithOffset, jsonUrlOffset);
    }

    [Fact]
    public void user_isOnline_true()
    {
        JsonData userData = new JsonData
        {
            userId = "e9de6dd1-84e5-9833-59de-8c51008de6a0",
            nickname = "Emmett82",
            firstName = "Emmett",
            lastName = "Block",
            registrationDate = "2023-06-08T21:50:41.1053254+00:00",
            lastSeenDate = "2023-09-28T17:00:00.4068456+00:00",
            isOnline = true,
        };
        //don't really need DateTime here, but I need to pass some value into the function, even if it's not used
        string datetimeStr = "2023-09-28T17:00:00.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 is online";

        string userOnlineStatus = LastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
        Assert.Equal(expectedStatus, userOnlineStatus);

    }

    [Fact]
    public void user_isOnline_justNow()
    {
        JsonData userData = new JsonData
        {
            userId = "e9de6dd1-84e5-9833-59de-8c51008de6a0",
            nickname = "Emmett82",
            firstName = "Emmett",
            lastName = "Block",
            registrationDate = "2023-06-08T21:50:41.1053254+00:00",
            lastSeenDate = "2023-09-29T16:59:45.4068456+00:00",
            isOnline = false,
        };
        string datetimeStr = "2023-09-29T17:00:00.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 was online just now";
        
        string userOnlineStatus = LastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
        Assert.Equal(expectedStatus, userOnlineStatus);
    }
    
    [Fact]
    public void user_isOnline_lessThanAMinuteAgo()
    {
        JsonData userData = new JsonData
        {
            userId = "e9de6dd1-84e5-9833-59de-8c51008de6a0",
            nickname = "Emmett82",
            firstName = "Emmett",
            lastName = "Block",
            registrationDate = "2023-06-08T21:50:41.1053254+00:00",
            lastSeenDate = "2023-09-29T16:59:15.3367009+00:00",
            isOnline = false,
        };
        string datetimeStr = "2023-09-29T17:00:00.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 was online less than a minute ago";
        
        string userOnlineStatus = LastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
        Assert.Equal(expectedStatus, userOnlineStatus);
    }
    
    [Fact]
    public void user_isOnline_coupleOfMinutesAgo()
    {
        JsonData userData = new JsonData
        {
            userId = "e9de6dd1-84e5-9833-59de-8c51008de6a0",
            nickname = "Emmett82",
            firstName = "Emmett",
            lastName = "Block",
            registrationDate = "2023-06-08T21:50:41.1053254+00:00",
            lastSeenDate = "2023-09-29T16:45:00.3367009+00:00",
            isOnline = false,
        };
        string datetimeStr = "2023-09-29T17:00:00.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 was online a couple of minutes ago";
        
        string userOnlineStatus = LastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
        Assert.Equal(expectedStatus, userOnlineStatus);
    }
    
    [Fact]
    public void user_isOnline_anHourAgo()
    {
        JsonData userData = new JsonData
        {
            userId = "e9de6dd1-84e5-9833-59de-8c51008de6a0",
            nickname = "Emmett82",
            firstName = "Emmett",
            lastName = "Block",
            registrationDate = "2023-06-08T21:50:41.1053254+00:00",
            lastSeenDate = "2023-09-29T15:45:00.3367009+00:00",
            isOnline = false,
        };
        string datetimeStr = "2023-09-29T17:00:00.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 was online an hour ago";
        
        string userOnlineStatus = LastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
        Assert.Equal(expectedStatus, userOnlineStatus);
    }
    
    [Fact]
    public void user_isOnline_today()
    {
        JsonData userData = new JsonData
        {
            userId = "e9de6dd1-84e5-9833-59de-8c51008de6a0",
            nickname = "Emmett82",
            firstName = "Emmett",
            lastName = "Block",
            registrationDate = "2023-06-08T21:50:41.1053254+00:00",
            lastSeenDate = "2023-09-29T13:00:00.3367009+00:00",
            isOnline = false,
        };
        string datetimeStr = "2023-09-29T17:00:00.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 was online today";
        
        string userOnlineStatus = LastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
        Assert.Equal(expectedStatus, userOnlineStatus);
    }
    
    [Fact]
    public void user_isOnline_yesterday()
    {
        JsonData userData = new JsonData
        {
            userId = "e9de6dd1-84e5-9833-59de-8c51008de6a0",
            nickname = "Emmett82",
            firstName = "Emmett",
            lastName = "Block",
            registrationDate = "2023-06-08T21:50:41.1053254+00:00",
            lastSeenDate = "2023-09-28T17:00:00.3367009+00:00",
            isOnline = false,
        };
        string datetimeStr = "2023-09-29T17:00:00.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 was online yesterday";
        
        string userOnlineStatus = LastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
        Assert.Equal(expectedStatus, userOnlineStatus);
    }
    
    [Fact]
    public void user_isOnline_thisWeek()
    {
        JsonData userData = new JsonData
        {
            userId = "e9de6dd1-84e5-9833-59de-8c51008de6a0",
            nickname = "Emmett82",
            firstName = "Emmett",
            lastName = "Block",
            registrationDate = "2023-06-08T21:50:41.1053254+00:00",
            lastSeenDate = "2023-09-24T17:00:00.3367009+00:00",
            isOnline = false,
        };
        string datetimeStr = "2023-09-29T17:00:00.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 was online this week";
        
        string userOnlineStatus = LastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
        Assert.Equal(expectedStatus, userOnlineStatus);
    }
    
    [Fact]
    public void user_isOnline_longTimeAgo()
    {
        JsonData userData = new JsonData
        {
            userId = "e9de6dd1-84e5-9833-59de-8c51008de6a0",
            nickname = "Emmett82",
            firstName = "Emmett",
            lastName = "Block",
            registrationDate = "2023-06-08T21:50:41.1053254+00:00",
            lastSeenDate = "2023-8-29T17:00:00.3367009+00:00",
            isOnline = false,
        };
        string datetimeStr = "2023-09-29T17:00:00.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 was online a long time ago";
        
        string userOnlineStatus = LastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
        Assert.Equal(expectedStatus, userOnlineStatus);
    }
}