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
            _db.CasualLeaves.Update(casualLeave);
        }
    }
}
