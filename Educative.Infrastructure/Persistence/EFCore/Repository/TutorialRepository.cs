using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educative.Domain.Entity;
using Educative.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Educative.Infrastructure.Persistence.EFCore.Repository
{
    public class TutorialRepository : ITutorialRepository
    {
        private readonly DBContext context;

        public TutorialRepository(DBContext context)
        {
            this.context = context;
        }
        public async Task<ICollection<Tutorial>> GetAll(Paginator paginator)
        {
            return await context.Tutorials
                                .Skip(paginator.skip)
                                .Take(paginator.take)
                                .ToListAsync();
        }
        public async Task<Tutorial> GetById(int id)
        {
            return await context.Tutorials
                                .Include(t => t.course)
                                .Include(t => t.image)
                                .FirstOrDefaultAsync(t => t.id == id);
        }
        public async Task Add(Tutorial tutorial)
        {
            await context.Tutorials.AddAsync(tutorial);
        }

        public void Delete(Tutorial tutorial)
        {
            context.Tutorials.Remove(tutorial);
        }
        public async Task saveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
