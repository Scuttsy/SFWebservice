using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SFWebservice;
using SFWebservice.Modules;

namespace SFWebservice.Controllers
{
    [Route("api/[controller]")]
    [EnableCors()]
    [ApiController]
    public class GameSessionsController : ControllerBase
    {
        private readonly Sfdb01Context _context;

        public GameSessionsController(Sfdb01Context context)
        {
            _context = context;
        }

        // GET: api/GameSessions
        [HttpGet]
        public IEnumerable<GameSession> GetGameSessions()
        {
            List<GameSession> result = _context.GameSessions.ToList();
            return result ?? new List<GameSession>();
        }   

        // GET: api/GameSessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameSession>> GetGameSession(int id)
        {
            var gameSession = await _context.GameSessions.FindAsync(id);

            if (gameSession == null)
            {
                return NotFound();
            }

            return gameSession;
        }

        // PUT: api/GameSessions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameSession(int id, GameSession gameSession)
        {
            if (id != gameSession.SessionId)
            {
                return BadRequest();
            }

            _context.Entry(gameSession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameSessionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GameSessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754S
        [HttpPost]
        public PlayerData PostGameSession([FromBody] PlayerData playerData)
        {
            if (playerData.Sessions == null) return new PlayerData();

            GameSession newGameSesion;
            List<GameSession> newSessions = new List<GameSession>();

            foreach (GameSession session in playerData.Sessions)
                if (!_context.GameSessions.Any(e => e.SessionId == session.SessionId))
                {
                    newGameSesion = new GameSession()
                    {
                        StartTime = session.StartTime,
                        PlayerId = playerData.ID,
                        EndTime = session.EndTime,
                        FeedbackType = session.FeedbackType
                    };

                    _context.Add(newGameSesion);
                    _context.SaveChanges();

                    newSessions.Add(newGameSesion);
                }
                else
                {
                    newGameSesion = _context.GameSessions.Where(e => e.SessionId == session.SessionId).First();
                    newGameSesion.EndTime = session.EndTime;

                    _context.Update(newGameSesion);
                    _context.SaveChanges();

                    newSessions.Add(newGameSesion);
                }

            playerData.Sessions = newSessions;
            return playerData;
        }

        // DELETE: api/GameSessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameSession(int id)
        {
            var gameSession = await _context.GameSessions.FindAsync(id);
            if (gameSession == null)
            {
                return NotFound();
            }

            _context.GameSessions.Remove(gameSession);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameSessionExists(int id)
        {
            return _context.GameSessions.Any(e => e.SessionId == id);
        }
    }
}
