using CEBackend.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RansomizerController : ControllerBase
    {
        private readonly Context _context;

        public RansomizerController(Context context)
        {
            _context = context;
        }

        // GET: api/Aliens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alien>>> GetAliens(int Easy, int Medium, int Difficult)
        {
            List<Alien> easyAliens = _context.Aliens.Where(x => x.difficulty == Difficulty.Easy).ToList();
            List<Alien> mediumAliens = _context.Aliens.Where(x => x.difficulty == Difficulty.Medium).ToList();
            List<Alien> difficultAliens = _context.Aliens.Where(x => x.difficulty == Difficulty.Difficult).ToList();
            
            List<Alien> randomAliens = new List<Alien>();
            for (int i = 0; i < Easy; i++)
            {
                if (easyAliens.Count == 0)
                    continue;

                Alien alien = getRandomEntity(easyAliens);
                easyAliens.Remove(alien);
                randomAliens.Add(alien);
            }

            for (int i = 0; i < Medium; i++)
            {
                if (mediumAliens.Count == 0)
                    continue;

                Alien alien = getRandomEntity(mediumAliens);
                mediumAliens.Remove(alien);
                randomAliens.Add(alien);
            }

            for (int i = 0; i < Difficult; i++)
            {
                if (difficultAliens.Count == 0)
                    continue;

                Alien alien = getRandomEntity(difficultAliens);
                difficultAliens.Remove(alien);
                randomAliens.Add(alien);
            }

            return Ok(randomAliens);
        }

        private Alien getRandomEntity(List<Alien> repo)
        {
            var rand = new Random();

            var skip = (int)(rand.Next(0, repo.Count()));
            return repo.OrderBy(o => o.ID).Skip(skip).Take(1).First();
        }
    }
}
