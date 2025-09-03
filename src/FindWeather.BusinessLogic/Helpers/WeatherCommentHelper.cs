namespace FindWeather.BusinessLogic.Helpers;

public static class WeatherCommentHelper
{
    public static string GetComment(double temperature)
    {
        return temperature switch
        {
            <= 0 => "Dress warmly",
            < 20 => "It's fresh",
            < 30 => "Good weather",
            _ => "it's time to go to the beach"
        };
    }
}
