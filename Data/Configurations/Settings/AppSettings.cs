namespace Todo.Api.Data.Configurations.Settings
{
    internal abstract class AppSettings
    {
        public static string ConnectionString => "Server=localhost;Port=5432;Database=tododb;User Id=dev;Password=12345678;";

        public static string SecurityKey => "UzI1IshbGciOiJIInR5ceyJzdWIiOiIxMjDkwIiwI6kpviaWF0E2MjM5MDIwRSMeKKFPOk6yJw5c";

        public static string Issuer => "TodoIssuer";

        public static string Audience => "TodoAudience";
    }
}
