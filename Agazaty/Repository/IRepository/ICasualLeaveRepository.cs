using Agazaty.Models;

namespace Agazaty.Repository.IRepository
{
    public interface ICasualLeaveRepository : IRepository<CasualLeave>
    {
        void Update(CasualLeave casualLeave);
    }
}
