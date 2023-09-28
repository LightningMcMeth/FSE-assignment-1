using FSE_assignment_1;

namespace UnitTest1;


public class lastSeenOnlineTests
{
    [Fact]
    public void offset_IsJoinedWithValidValue_true()
    {
        string jsonUrl = "https://sef.podkolzin.consulting/api/users/lastSeen?offset=";
        string jsonUrlWithOffset = "https://sef.podkolzin.consulting/api/users/lastSeen?offset=20";

        string jsonUrlOffset = lastSeenOnlineProgram.offsetURL(20, jsonUrl);
        
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
            lastSeenDate = "2023-09-28T17:16:28.4068456+00:00",
            isOnline = true,
        };
        //don't really need DateTime here, but I need to pass some value into the function, even if it's not used
        string datetimeStr = "2023-09-28T17:16:38.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 is online";

        string userOnlineStatus = lastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
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
            lastSeenDate = "2023-09-28T17:16:28.4068456+00:00",
            isOnline = false,
        };
        string datetimeStr = "2023-09-28T17:16:38.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 was online just now";
        
        string userOnlineStatus = lastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
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
            lastSeenDate = "2023-09-28T17:16:28.4068456+00:00",
            isOnline = false,
        };
        string datetimeStr = "2023-09-28T17:17:02.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 was online less than a minute ago";
        
        string userOnlineStatus = lastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
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
            lastSeenDate = "2023-09-28T17:16:28.4068456+00:00",
            isOnline = false,
        };
        string datetimeStr = "2023-09-28T17:20:28.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 was online a couple of minutes ago";
        
        string userOnlineStatus = lastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
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
            lastSeenDate = "2023-09-28T17:16:28.4068456+00:00",
            isOnline = false,
        };
        string datetimeStr = "2023-09-28T18:17:28.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 was online an hour ago";
        
        string userOnlineStatus = lastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
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
            lastSeenDate = "2023-09-28T17:16:28.4068456+00:00",
            isOnline = false,
        };
        string datetimeStr = "2023-09-28T20:16:28.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 was online today";
        
        string userOnlineStatus = lastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
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
            lastSeenDate = "2023-09-28T17:16:28.4068456+00:00",
            isOnline = false,
        };
        string datetimeStr = "2023-09-27T17:16:28.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 was online yesterday";
        
        string userOnlineStatus = lastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
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
            lastSeenDate = "2023-09-28T17:16:28.4068456+00:00",
            isOnline = false,
        };
        string datetimeStr = "2023-09-23T17:16:28.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 was online this week";
        
        string userOnlineStatus = lastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
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
            lastSeenDate = "2023-09-28T17:16:28.4068456+00:00",
            isOnline = false,
        };
        string datetimeStr = "2023-08-28T17:16:28.3367009+00:00";
        DateTime datetimeComparison = DateTime.Parse(datetimeStr);
        datetimeComparison = datetimeComparison.ToUniversalTime();
        string expectedStatus = "Emmett82 was online a long time ago";
        
        string userOnlineStatus = lastSeenOnlineProgram.calculateLastSeenOnline(userData, datetimeComparison);
        
        Assert.Equal(expectedStatus, userOnlineStatus);
    }
}