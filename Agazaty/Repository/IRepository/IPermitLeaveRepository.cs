using Agazaty.Models;

namespace Agazaty.Repository.IRepository
{
    public interface IPermitLeaveRepository:IRepository<PermitLeave>
    {
        void Update(PermitLeave permitLeave);
    }
}
