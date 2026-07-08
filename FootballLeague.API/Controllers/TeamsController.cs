using FootballLeague.API.Data;
using FootballLeague.API.models;
using Microsoft.AspNetCore.Mvc;

namespace FootballLeague.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        // Erişim Anahtarı
        private readonly AppDbContext _context;

        public TeamsController(AppDbContext context)
        {
            _context = context;
        }

        // 1. CREATE (Oluşturma): Yeni bir takım ekler
        [HttpPost]
        public IActionResult AddTeam(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
            return Ok(team);
        }
        
        // 2. READ (Listeleme): Tüm takımları getirir
        [HttpGet]
        public IActionResult GetTeams()
        {
            var teams = _context.Teams.ToList();
            return Ok(teams);
        }

         // 3. DELETE (Silme): İstenmeyen takımı veritabanından siler
        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            // Önce silinecek takımı ID numarasına göre bul
            var team = _context.Teams.Find(id);

            // Eğer o numarada bir takım yoksa "Bulunamadı" de
            if (team == null)
            {
                return NotFound();
            }

            // Takım bulunduysa listeden çıkar ve veritabanını kaydet
            _context.Teams.Remove(team);
            _context.SaveChanges();

            return Ok(); 
        }
        // 4. UPDATE (Güncelleme): Var olan bir takımın bilgilerini değiştirir
        [HttpPut("{id}")]
        public IActionResult UpdateTeam(int id, Team updatedTeam)
        {
            // Önce güncellenecek takımı ID numarasına göre bul
            var team = _context.Teams.Find(id);

            if (team == null)
            {
                return NotFound(); // Takım yoksa hata ver
            }

            // Takım bulunduysa, yeni gelen bilgilerle eskilerini değiştir
            team.Name = updatedTeam.Name;
            team.FoundedYear = updatedTeam.FoundedYear;
            team.Colors = updatedTeam.Colors;
            team.LogoUrl = updatedTeam.LogoUrl;

            // Değişiklikleri veritabanına kaydet
            _context.SaveChanges();

            return Ok(team); // Güncellenmiş halini onayla
        }
        // 5. AUTO-SEED 
        [HttpPost("inject-teams")]
        public IActionResult InjectTeams()
        {
            // Eğer içeride zaten takım varsa, doz aşımı olmasın diye işlemi durdur
            if (_context.Teams.Any())
            {
                return BadRequest();
            }

            var defaultTeams = new List<Team>
            {
                new Team { Name = "Galatasaray", FoundedYear = 1905, Colors = "Sarı-Kırmızı", LogoUrl = "gs.png" },
                new Team { Name = "Fenerbahçe", FoundedYear = 1907, Colors = "Sarı-Lacivert", LogoUrl = "fb.png" },
                new Team { Name = "Beşiktaş", FoundedYear = 1903, Colors = "Siyah-Beyaz", LogoUrl = "bjk.png" },
                new Team { Name = "Trabzonspor", FoundedYear = 1967, Colors = "Bordo-Mavi", LogoUrl = "ts.png" },
                new Team { Name = "Samsunspor", FoundedYear = 1965, Colors = "Kırmızı-Beyaz", LogoUrl = "sam.png" },
                new Team { Name = "Adana Demirspor", FoundedYear = 1940, Colors = "Mavi-Lacivert", LogoUrl = "ads.png" },
                new Team { Name = "Antalyaspor", FoundedYear = 1966, Colors = "Kırmızı-Beyaz", LogoUrl = "ant.png" },
                new Team { Name = "Alanyaspor", FoundedYear = 1948, Colors = "Turuncu-Yeşil", LogoUrl = "ala.png" },
                new Team { Name = "Konyaspor", FoundedYear = 1922, Colors = "Yeşil-Beyaz", LogoUrl = "kon.png" },
                new Team { Name = "Kayserispor", FoundedYear = 1966, Colors = "Sarı-Kırmızı", LogoUrl = "kay.png" },
                new Team { Name = "Sivasspor", FoundedYear = 1967, Colors = "Kırmızı-Beyaz", LogoUrl = "siv.png" },
                new Team { Name = "Başakşehir", FoundedYear = 1990, Colors = "Turuncu-Lacivert", LogoUrl = "bas.png" },
                new Team { Name = "Kasımpaşa", FoundedYear = 1921, Colors = "Lacivert-Beyaz", LogoUrl = "kas.png" },
                new Team { Name = "Ankaragücü", FoundedYear = 1910, Colors = "Sarı-Lacivert", LogoUrl = "ag.png" },
                new Team { Name = "Hatayspor", FoundedYear = 1967, Colors = "Bordo-Beyaz", LogoUrl = "hat.png" },
                new Team { Name = "Rizespor", FoundedYear = 1953, Colors = "Yeşil-Mavi", LogoUrl = "riz.png" },
                new Team { Name = "Gaziantep FK", FoundedYear = 1988, Colors = "Kırmızı-Siyah", LogoUrl = "gaz.png" },
                new Team { Name = "Göztepe", FoundedYear = 1925, Colors = "Sarı-Kırmızı", LogoUrl = "goz.png" }
            };

            _context.Teams.AddRange(defaultTeams);
            _context.SaveChanges();

            return Ok();
        }
    }
}