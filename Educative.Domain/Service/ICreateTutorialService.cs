using System.Threading.Tasks;
using Educative.Domain.DTO;
using Educative.Domain.Entity;

namespace Educative.Domain.Services
{
    public interface ICreateTutorialService
    {
        Task<Tutorial> execute(TutorialRequest request);
    }
}
