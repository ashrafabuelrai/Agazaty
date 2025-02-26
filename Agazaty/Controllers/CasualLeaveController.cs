using Agazaty.Models;
using Agazaty.Models.DTO;
using Agazaty.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Agazaty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasualLeaveController : ControllerBase
    {
        protected ApiResponse _response { get; set; }
        private readonly IUnitOfWork _unitOfWork;

        public IMapper _mapper { get; }

        public CasualLeaveController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _response = new ApiResponse();
        }
        [HttpGet("GetCasualLeave/{leaveID:int}", Name = "GetCasualLeave")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApiResponse> GetCasualLeave(int leaveID)
        {
            try
            {
                var casualLeave = _unitOfWork.CasualLeave.Get(c => c.Id == leaveID);
                if (casualLeave == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Casual Leave Not Found!" };
                    return NotFound(_response);
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<CasualLeaveDTO>(casualLeave);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("GetAllCasualLeaves", Name = "GetAllCasualLeaves")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApiResponse> GetAllCasualLeaves()
        {
            try
            {
                var casualLeaves = _unitOfWork.CasualLeave.GetAll().ToList();
                if (casualLeaves == null || casualLeaves.Count() == 0)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Casual Leave Not Found!" };
                    return NotFound(_response);
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<IEnumerable<CasualLeaveDTO>>(casualLeaves);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("GetAllCasualLeavesByUserID/{userID}", Name = "GetAllCasualLeavesByUserID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApiResponse> GetAllCasualLeavesByUserID(string userID)
        {
            try
            {
                var casualLeaves = _unitOfWork.CasualLeave.GetAll(c => c.UserId == userID).ToList();
                if (casualLeaves == null || casualLeaves.Count() == 0)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Casual Leave Not Found!" };
                    return NotFound(_response);
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<IEnumerable<CasualLeaveDTO>>(casualLeaves);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("GetAllCasualLeavesByUserIDAndYear/{userID}/{year:int}", Name = "GetAllCasualLeavesByUserIDAndYear")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApiResponse> GetAllCasualLeavesByUserIDAndYear(string userID, int year)
        {
            try
            {
                var casualLeaves = _unitOfWork.CasualLeave.GetAll(c => c.UserId == userID
                                  && c.Year == year).ToList();
                if (casualLeaves == null||casualLeaves.Count()==0)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Casual Leave Not Found!" };
                    return NotFound(_response);
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<IEnumerable<CasualLeaveDTO>>(casualLeaves);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("CreateCasualLeave",Name = "CreateCasualLeave")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ApiResponse> CreateCasualLeave([FromBody] CreateCasualLeaveDTO model)
        {
            try
            {
                if(model==null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                if (DateOnly.FromDateTime(model.StartDate)>= DateOnly.FromDateTime(DateTime.Now))
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "You have to choose on a past date" };
                    return BadRequest(_response);
                }
                if ((model.EndDate - model.StartDate).Days < 1)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "You have to choose at least one day " };
                    return BadRequest(_response);
                }
                if ((model.EndDate -model.StartDate).Days > 2)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "You have exceeded the allowed number of days " };
                    return BadRequest(_response);
                }
                var casualLeave = _mapper.Map<CasualLeave>(model);
                casualLeave.Year = model.StartDate.Year;
                _unitOfWork.CasualLeave.Add(casualLeave);
                _unitOfWork.Save();
                _response.StatusCode = HttpStatusCode.Created;
                _response.Result= _mapper.Map<CasualLeaveDTO>(casualLeave);
                
                return CreatedAtRoute("GetCasualLeave", new { leaveID = casualLeave.Id},_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPut("UpdateCasualLeave/{leaveID:int}", Name = "UpdateCasualLeave")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApiResponse> UpdateCasualLeave(int leaveID, [FromForm] UpdateCasualLeaveDTO model)
        {
            try
            {
                if (leaveID == 0 || leaveID == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var casualLeave = _unitOfWork.CasualLeave.Get(c => c.Id == leaveID);
                if (casualLeave == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Casual Leave Not Found!" };
                    return NotFound(_response);
                }
                casualLeave = _mapper.Map<CasualLeave>(model);
                _unitOfWork.CasualLeave.Update(casualLeave);
                _unitOfWork.Save();
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete("DeleteCasualLeave/{leaveID:int}", Name = "DeleteCasualLeave")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApiResponse> DeleteCasualLeave(int leaveID)
        {
            try
            {
                if (leaveID == 0 || leaveID == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var casualLeave = _unitOfWork.CasualLeave.Get(c => c.Id == leaveID);
                if (casualLeave == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Casual Leave Not Found!" };
                    return NotFound(_response);
                }
                _unitOfWork.CasualLeave.Remove(casualLeave);
                _unitOfWork.Save();
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        
    }
}
