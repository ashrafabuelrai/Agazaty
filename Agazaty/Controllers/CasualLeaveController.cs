using Agazaty.Models;
using Agazaty.Models.DTO;
using Agazaty.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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
        [HttpGet("GetCasualLeave/{leaveID:int}", Name = "GetCasualLeave")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CasualLeaveDTO> GetCasualLeave(int leaveID)
        {
            var casualLeave = _unitOfWork.CasualLeave.Get(c => c.Id == leaveID);
            if(casualLeave==null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CasualLeaveDTO>(casualLeave));
        }
        [HttpGet("GetAllCasualLeaves", Name = "GetAllCasualLeaves")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CasualLeaveDTO>> GetAllCasualLeaves()
        {
            var casualLeaves = _unitOfWork.CasualLeave.GetAll().ToList();
            if (casualLeaves == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<CasualLeaveDTO>>(casualLeaves));
        }
        [HttpGet("GetAllCasualLeavesByUserID/{userID}", Name = "GetAllCasualLeavesByUserID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CasualLeaveDTO>> GetAllCasualLeavesByUserID(string userID)
        {
            var casualLeaves = _unitOfWork.CasualLeave.GetAll(c=>c.UserId==userID).ToList();
            if(casualLeaves==null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<CasualLeaveDTO>>(casualLeaves));
        }
        [HttpGet("GetAllCasualLeavesByUserIDAndYear/{userID}/{year:int}", Name = "GetAllCasualLeavesByUserIDAndYear")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CasualLeaveDTO>> GetAllCasualLeavesByUserIDAndYear(string userID, int year)
        {
            var casualLeaves = _unitOfWork.CasualLeave.GetAll(c => c.UserId == userID 
                              && c.Year.Year==year).ToList();
            if (casualLeaves == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<CasualLeaveDTO>>(casualLeaves));
        }
        [HttpPost("CreateCasualLeave",Name = "CreateCasualLeave")]
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
        [HttpPut("UpdateCasualLeave/{leaveID:int}", Name = "UpdateCasualLeave")]
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
        [HttpDelete("DeleteCasualLeave/{leaveID:int}", Name = "DeleteCasualLeave")]
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
