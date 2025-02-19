using Agazaty.Models;
using Agazaty.Models.DTO;
using AutoMapper;

namespace Agazaty
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<PermitLeave, CreatePermitLeaveDTO>().ReverseMap();
            CreateMap<PermitLeave, UpdatePermitLeaveDTO>().ReverseMap();
            CreateMap<PermitLeave, PermitLeaveDTO>().ReverseMap();

            CreateMap<CasualLeave, CreateCasualLeaveDTO>().ReverseMap();
            CreateMap<CasualLeave, UpdateCasualLeaveDTO>().ReverseMap();
            CreateMap<CasualLeave, CasualLeaveDTO>().ReverseMap();

        }
    }
}
