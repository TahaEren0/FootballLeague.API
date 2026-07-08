using FootballLeague.API.models;
using FootballLeague.API.Models;


namespace FootballLeague.API.Services
{
    public class FixtureService : IFixtureService
    {
        public List<Match> GenerateFixture(List<Team> teams)
        {
            var matches = new List<Match>();
            int totalTeams = teams.Count;
            int totalWeeks = (totalTeams - 1) * 2; // Çift devreli lig (18 takım için 34 hafta)
            int halfSeason = totalWeeks / 2; // 17 hafta ilk yarı

            // Yuvarlak (Round-Robin) Algoritması: Takımları birbiriyle çakışmadan eşleştirir
            for (int week = 1; week <= halfSeason; week++)
            {
                for (int i = 0; i < totalTeams / 2; i++)
                {
                    int homeTeamIndex = (week + i) % (totalTeams - 1);
                    int awayTeamIndex = (totalTeams - 1 - i + week) % (totalTeams - 1);

                    if (i == 0) awayTeamIndex = totalTeams - 1;

                    var homeTeam = teams[homeTeamIndex];
                    var awayTeam = teams[awayTeamIndex];

                    // 1. Yarı Maçı (Örn: 1. Hafta, Fenerbahçe Ev Sahibi)
                    matches.Add(new Match
                    {
                        HomeTeamId = homeTeam.Id,
                        AwayTeamId = awayTeam.Id,
                        Week = week
                    });

                    // 2. Yarı Maçı (Örn: 18. Hafta, Fenerbahçe Deplasmanda)
                    matches.Add(new Match
                    {
                        HomeTeamId = awayTeam.Id,
                        AwayTeamId = homeTeam.Id,
                        Week = week + halfSeason
                    });
                }
            }

            // Maçları haftaya göre sıralayıp listeyi teslim et
            return matches.OrderBy(m => m.Week).ToList();
        }
    }
}