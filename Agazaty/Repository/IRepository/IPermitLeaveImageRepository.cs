using Agazaty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agazaty.Repository.IRepository
{
    public interface IPermitLeaveImageRepository : IRepository<PermitLeaveImage>
    {
        void Update(PermitLeaveImage obj);
    }
}
