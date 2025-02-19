using Agazaty.Data;
using Agazaty.Models;
using Agazaty.Repository.IRepository;

namespace Agazaty.Repository
{
    public class CasualLeaveRepository : Repository<CasualLeave>, ICasualLeaveRepository
    {
        private readonly ApplicationDbContext _db;

        public CasualLeaveRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CasualLeave casualLeave)
        {
            var old = _db.CasualLeaves.FirstOrDefault(c => c.Id == casualLeave.Id);
            old.StartDate = casualLeave.StartDate;
            old.EndDate = casualLeave.EndDate;
            old.Year = casualLeave.Year;
            
        }
    }
}
