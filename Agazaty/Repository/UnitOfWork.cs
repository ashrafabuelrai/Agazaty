using Agazaty.Data;
using Agazaty.Repository.IRepository;
using BulkyBook.DataAccess.Repository;

namespace Agazaty.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IPermitLeaveRepository PermitLeave { get; set; }
        public ICasualLeaveRepository CasualLeave { get; set; }
        public IPermitLeaveImageRepository PermitLeaveImage { get; set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            CasualLeave = new CasualLeaveRepository(_db);
            PermitLeave = new PermitLeaveRepository(_db);
            PermitLeaveImage = new PermitLeaveImageRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
