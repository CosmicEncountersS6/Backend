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
            IQueryable<Alien> easyAliens = _context.Aliens.Where(x => x.difficulty == Difficulty.Easy);
            IQueryable<Alien> mediumAliens = _context.Aliens.Where(x => x.difficulty == Difficulty.Medium);
            IQueryable<Alien> difficultAliens = _context.Aliens.Where(x => x.difficulty == Difficulty.Difficult);
            
            List<Alien> randomAliens = new List<Alien>();
            for (int i = 0; i < Easy; i++)
            {
                randomAliens.Add(getRandomEntity(easyAliens));
            }

            for (int i = 0; i < Medium; i++)
            {
                randomAliens.Add(getRandomEntity(mediumAliens));
            }

            for (int i = 0; i < Difficult; i++)
            {
                randomAliens.Add(getRandomEntity(difficultAliens));
            }

            return Ok(randomAliens);
        }

        private Alien getRandomEntity(IQueryable<Alien> repo)
        {
            var rand = new Random();

            var skip = (int)(rand.Next(0, repo.Count()));
            return repo.OrderBy(o => o.ID).Skip(skip).Take(1).First();
        }
    }
}
