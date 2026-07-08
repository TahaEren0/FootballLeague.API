using FootballLeague.API.models;
using FootballLeague.API.Models;

namespace FootballLeague.API.Services
{
    
    public interface IFixtureService
    {
        List<Match> GenerateFixture(List<Team> teams);
    }
}