namespace GilsApi.Common;

public static class Constants
{
    public static class ResponseSources
    {
        public const string Database = "database";
        public const string Cache = "cache";
    }

    public static readonly TimeSpan StandardCacheDuration = TimeSpan.FromMinutes(10);
}