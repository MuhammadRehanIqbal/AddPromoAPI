namespace Infrastructure.DbContext
{
    public class DapperSettings
    {
        public const string SectionName = "ConnectionStrings";

        public string MySql { get; set; } = null!;
        //public string SqlServer { get; set; } = null!;
    }
}
