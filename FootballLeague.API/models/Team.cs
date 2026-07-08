namespace FootballLeague.API.models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FoundedYear { get; set; }
        public string Colors { get; set; }
        public string LogoUrl { get; set; }
    }
}