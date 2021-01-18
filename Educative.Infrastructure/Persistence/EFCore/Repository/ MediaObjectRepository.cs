using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educative.Domain.Entity;
using Educative.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Educative.Infrastructure.Persistence.EFCore.Repository
{
    public class MediaObjectRepository : IMediaObjectRepository
    {
        private readonly DBContext context;

        public MediaObjectRepository(DBContext context)
        {
            this.context = context;
        }
        public async Task<ICollection<MediaObject>> GetAll(Paginator paginator)
        {
            return await context.MediaObjects
            .Skip(paginator.skip)
            .Take(paginator.take)
            .ToListAsync();
        }
        public async Task<MediaObject> GetById(int id)
        {
            return await context.MediaObjects.FirstOrDefaultAsync(m => m.id == id);
        }
        public async Task Add(MediaObject media)
        {
            await context.MediaObjects.AddAsync(media);
        }
        public void Delete(MediaObject media)
        {
            context.MediaObjects.Remove(media);
        }
        public async Task saveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
