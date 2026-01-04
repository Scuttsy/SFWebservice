using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SFWebservice.Modules;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SFWebservice.Controllers
{
    [Route("api/[controller]")]
    [EnableCors()]
    [ApiController]
    public class PlayerDataController : ControllerBase
    {
        private readonly Sfdb01Context _context;

        public PlayerDataController(Sfdb01Context context)
        {
            _context = context;
        }

        // POST api/<PlayerDataController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PlayerData playerData)
        {
            //update session
            foreach (GameSession session in playerData.Sessions)
                if (_context.GameSessions.Any(e => e.SessionId == session.SessionId))
                {
                    GameSession newSession = _context.GameSessions.Where(e => e.SessionId == session.SessionId).First();
                    if (newSession.EndTime == null)
                    {
                        newSession.EndTime = session.EndTime;
                        _context.Update(newSession);
                        _context.SaveChanges();
                    }
                }

            // Add Hits
            if (playerData.Hits != null)
            {
                foreach (var hit in playerData.Hits)
                {
                    _context.Hits.Add(hit);
                }
                _context.SaveChanges();
            }
            // Add Dodges
            if (playerData.Dodges != null)
            {
                foreach (var dodge in playerData.Dodges)
                {
                    _context.Dodges.Add(dodge);
                }
                _context.SaveChanges();
            }
            // Add Objectives Completed
            if (playerData.ObjectivesCompleted != null)
            {
                foreach (var objectiveCompleted in playerData.ObjectivesCompleted)
                {
                    _context.ObjectiveCompleteds.Add(objectiveCompleted);
                }
                _context.SaveChanges();
            }

            return NoContent();
        }

    }
}
