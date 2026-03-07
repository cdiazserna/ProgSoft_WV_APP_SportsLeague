using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(LeagueDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Player>> GetByTeamAsync(int teamId)
        {
            var players = await _dbSet
                .Where(p => p.TeamId == teamId) // Where es el filtro para obtener solo los jugadores del equipo especificado
                .Include(p => p.Team) // Es un Inner Join para incluir la información del equipo en el resultado
                .ToListAsync(); // ToListAsync ejecuta la consulta y devuelve una lista de jugadores

            return players;
        }

        public async Task<Player?> GetByTeamAndNumberAsync(int teamId, int number)
        {
            return await _dbSet
                .FirstOrDefaultAsync(p => p.TeamId == teamId && p.Number == number);
            //Linq es la forma simplificada de escribir consultas a la base de datos
        }

        public async Task<IEnumerable<Player>> GetAllWithTeamAsync()
        {
            return await _dbSet
                .Include(p => p.Team)
                .ToListAsync();
        }

        public async Task<Player?> GetByIdWithTeamAsync(int id)
        {
            return await _dbSet
                .Where(p =>p.Id == id)
                .Include(p => p.Team)
                .FirstOrDefaultAsync();
        }
    }
}
