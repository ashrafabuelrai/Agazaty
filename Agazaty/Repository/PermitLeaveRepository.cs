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
            _db.PermitLeaves.Update(permitLeave);
            
        }
    }
}
