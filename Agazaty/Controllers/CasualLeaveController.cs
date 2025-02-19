using Agazaty.Models;
using Agazaty.Models.DTO;
using Agazaty.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agazaty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasualLeaveController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public IMapper _mapper { get; }

        public CasualLeaveController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("{leaveID:int}")]
        public ActionResult<CasualLeaveDTO> GetCasualLeave(int leaveID)
        {
            var casualLeave = _unitOfWork.CasualLeave.Get(c => c.Id == leaveID);
            return Ok(_mapper.Map<CasualLeaveDTO>(casualLeave));
        }
        [HttpGet]
        public ActionResult<IEnumerable<CasualLeaveDTO>> GetAllCasualLeave()
        {
            var casualLeaves = _unitOfWork.CasualLeave.GetAll().ToList();
            return Ok(_mapper.Map<IEnumerable<CasualLeaveDTO>>(casualLeaves));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CasualLeave> CreateCasualLeave([FromBody] CreateCasualLeaveDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var casualLeave = _mapper.Map<CasualLeave>(model);
            _unitOfWork.CasualLeave.Add(casualLeave);
            _unitOfWork.Save();
            return Ok(casualLeave);
        }
        [HttpPut("{leaveID:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateCasualLeave(int leaveID, [FromBody] UpdateCasualLeaveDTO model)
        {
            if(leaveID==0||leaveID==null)
            {
                return BadRequest();
            }
            var casualLeave = _unitOfWork.CasualLeave.Get(c => c.Id == leaveID);
            if(casualLeave==null)
            {
                return NotFound();
            }
            casualLeave = _mapper.Map<CasualLeave>(model);
            _unitOfWork.CasualLeave.Update(casualLeave);
            _unitOfWork.Save();
            return NoContent();
        }
        [HttpDelete("{leaveID:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCasualLeave(int leaveID)
        {
            if (leaveID == 0 || leaveID == null)
            {
                return BadRequest();
            }
            var casualLeave = _unitOfWork.CasualLeave.Get(c => c.Id == leaveID);
            if (casualLeave == null)
            {
                return NotFound();
            }
            _unitOfWork.CasualLeave.Remove(casualLeave);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
