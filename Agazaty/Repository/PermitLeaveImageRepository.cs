using Agazaty.Repository.IRepository;
using Agazaty.Data;
using Agazaty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Agazaty.Repository;

namespace BulkyBook.DataAccess.Repository
{
    public class PermitLeaveImageRepository : Repository<PermitLeaveImage>, IPermitLeaveImageRepository
    {
        private readonly ApplicationDbContext _db;
        public PermitLeaveImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(PermitLeaveImage permitLeaveImage)
        {
            _db.PermitLeaveImages.Update(permitLeaveImage);
        }
    }
}
