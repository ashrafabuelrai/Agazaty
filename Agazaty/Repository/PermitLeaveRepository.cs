using Agazaty.Data;
using Agazaty.Models;
using Agazaty.Repository.IRepository;

namespace Agazaty.Repository
{
    public class PermitLeaveRepository : Repository<PermitLeave>, IPermitLeaveRepository
    {
        private readonly ApplicationDbContext _db;

        public PermitLeaveRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PermitLeave permitLeave)
        {
            var old = _db.PermitLeaves.FirstOrDefault(p => p.Id == permitLeave.Id);
            old.Hours = permitLeave.Hours;
            old.Date = permitLeave.Date;
            old.PermitLeaveImages = permitLeave.PermitLeaveImages;
            old.EmployeeNationalNumber = permitLeave.EmployeeNationalNumber;
            _db.SaveChanges();
        }
    }
}
