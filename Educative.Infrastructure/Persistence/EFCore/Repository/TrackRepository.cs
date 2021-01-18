using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educative.Domain.Entity;
using Educative.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Educative.Infrastructure.Persistence.EFCore.Repository
{
    public class TrackRepository : ITrackRepository
    {
        private readonly DBContext context;
        public TrackRepository(DBContext context)
        {
            this.context = context;

        }

        public async Task<ICollection<Track>> GetAll(Paginator paginator)
        {

            List<Track> tracks = await context.Tracks
            .Skip(paginator.skip)
            .Take(paginator.take)
            .ToListAsync();
            return tracks;
        }

        public async Task<Track> GetById(int id)
        {
            return await context.Tracks.Include(t => t.image).Include(t => t.courses).ThenInclude(c => c.image).FirstOrDefaultAsync(t => t.id == id);
        }
        public async Task Add(Track track)
        {
            await context.AddAsync(track);
        }
        public async Task saveChanges()
        {
            await context.SaveChangesAsync();
        }
        public void Delete(Track track)
        {
            context.Tracks.Remove(track);
        }
    }
}
