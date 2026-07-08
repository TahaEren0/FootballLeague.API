using FootballLeague.API.Data;
using FootballLeague.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FootballLeague.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IFixtureService _fixtureService;

        public MatchesController(AppDbContext context, IFixtureService fixtureService)
        {
            _context = context;
            _fixtureService = fixtureService;
        }

        // FİKSTÜR ÇEKİMİ İŞLEMİ
        [HttpPost("generate-fixture")]
        public IActionResult GenerateFixture()
        {
            var teams = _context.Teams.ToList();

            // Eğer içeride takım yoksa uyar
            if (teams.Count < 2)
            {
                return BadRequest("Fikstür çekmek için yeterli takım yok.");
            }

            // Daha önce fikstür çekilmişse mükerrer olmasın diye durdur
            if (_context.Matches.Any())
            {
                return BadRequest("Fikstür zaten çekilmiş!");
            }

            // O yazdığımız zeki algoritmayı çalıştırıp maç listesini alıyoruz
            var matches = _fixtureService.GenerateFixture(teams);

            // Çıkan sonucu veritabanındaki Matches tablomuza kaydediyoruz
            _context.Matches.AddRange(matches);
            _context.SaveChanges();

            return Ok($"Harika! Fikstür başarıyla çekildi. Toplam {matches.Count} maç oluşturuldu.");
        }
        // 2. READ (Listeleme): Veritabanındaki tüm fikstürü haftalara göre sıralayıp getirir
        [HttpGet]
        public IActionResult GetMatches()
        {
            var matches = _context.Matches.OrderBy(m => m.Week).ToList();
            return Ok(matches);
        }
    }
}