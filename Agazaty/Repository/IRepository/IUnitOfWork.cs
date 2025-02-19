namespace Agazaty.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IPermitLeaveRepository PermitLeave { get; }
        ICasualLeaveRepository CasualLeave { get; }
        void Save();
    }
}
