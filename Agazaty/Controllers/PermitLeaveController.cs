using Agazaty.Models;
using Agazaty.Models.DTO;
using Agazaty.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Agazaty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermitLeaveController : ControllerBase
    {
        protected ApiResponse _response { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public IMapper _mapper { get; }

        public PermitLeaveController(IMapper mapper, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _response = new ApiResponse();
        }
        [HttpGet("GetPermitLeave/{leaveID:int}", Name = "GetPermitLeave")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApiResponse> GetPermitLeave(int leaveID)
        {
            try
            {
                var permitLeave = _unitOfWork.PermitLeave.Get(c => c.Id == leaveID);
                if (permitLeave == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Permit Leave Not Found!" };
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<PermitLeaveDTO>(permitLeave);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("GetAllPermitLeaves", Name = "GetAllPermitLeaves")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApiResponse> GetAllPermitLeaves()
        {
            try
            {
                var permitLeaves = _unitOfWork.PermitLeave.GetAll().ToList();
                if (permitLeaves == null||permitLeaves.Count()==0)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Permit Leave Not Found!" };
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<IEnumerable<PermitLeaveDTO>>(permitLeaves);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("GetAllPermitLeavesByUserID/{userID}", Name = "GetAllPermitLeavesByUserID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApiResponse> GetAllPermitLeavesByUserID(string userID)
        {
            try
            {
                var permitLeaves = _unitOfWork.PermitLeave.GetAll(p => p.UserId == userID).ToList();
                if (permitLeaves == null || permitLeaves.Count() == 0)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Permit Leave Not Found!" };
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<IEnumerable<PermitLeaveDTO>>(permitLeaves);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("GetAllPermitLeavesByUserIDAndMonth/{userID}/{month:int}", Name = "GetAllPermitLeavesByUserIDAndMonth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApiResponse> GetAllPermitLeavesByUserIDAndMonth(string userID,int month)
        {
            try
            {
                var permitLeaves = _unitOfWork.PermitLeave.GetAll(p => p.UserId == userID &&
                                   p.Date.Month == month).ToList();
                if (permitLeaves == null || permitLeaves.Count() == 0)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Permit Leave Not Found!" };
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<IEnumerable<PermitLeaveDTO>>(permitLeaves);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("GetAllPermitLeavesByUserIDAndYear/{userID}/{year:int}", Name = "GetAllPermitLeavesByUserIDAndYear")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApiResponse> GetAllPermitLeavesByUserIDAndYear( string userID, int year)
        {
            try
            {
                var permitLeaves = _unitOfWork.PermitLeave.GetAll(p => p.UserId == userID &&
                                   p.Date.Year == year).ToList();
                if (permitLeaves == null || permitLeaves.Count() == 0)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Permit Leave Not Found!" };
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<IEnumerable<PermitLeaveDTO>>(permitLeaves);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("CreatePermitLeave",Name = "CreatePermitLeave")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ApiResponse> CreatePermitLeave([FromForm] CreatePermitLeaveDTO model,[FromForm] List<IFormFile>? files)
        {
            try
            {
                // check if it is allowed to take leave or not
                if(model==null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var permitLeave = _mapper.Map<PermitLeave>(model);

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string permitLeavePath = @"images\PermitLeaves\PermitLeaveUser-" + permitLeave.UserId;
                        string finalPath = Path.Combine(wwwRootPath, permitLeavePath);

                        if (!Directory.Exists(finalPath))
                        {
                            Directory.CreateDirectory(finalPath);
                        }
                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        PermitLeaveImage permitLeaveImage = new PermitLeaveImage()
                        {
                            ImageUrl = @"\" + permitLeavePath + @"\" + fileName,
                            LeaveId = permitLeave.Id
                        };


                        if (permitLeave.PermitLeaveImages == null)
                        {
                            permitLeave.PermitLeaveImages = new List<PermitLeaveImage>();
                        }
                        permitLeave.PermitLeaveImages.Add(permitLeaveImage);
                    }
                }
                _unitOfWork.PermitLeave.Add(permitLeave);
                _unitOfWork.Save();
                _response.Result = _mapper.Map<PermitLeaveDTO>(permitLeave);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetPermitLeave", new { leaveID = permitLeave.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPut("UpdatePermitLeave/{leaveID:int}", Name = "UpdatePermitLeave")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApiResponse> UpdatePermitLeave(int leaveID, [FromForm] UpdatePermitLeaveDTO model, [FromForm] List<IFormFile>? files)
        {
            try
            {
                if (leaveID == 0 || leaveID == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var permitLeave = _unitOfWork.PermitLeave.Get(c => c.Id == leaveID);
                if (permitLeave == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Permit Leave Not Found!" };
                    return NotFound(_response);
                }
                permitLeave = _mapper.Map<PermitLeave>(model);
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string permitLeavePath = @"images\PermitLeaves\PermitLeaveUser-" + permitLeave.UserId;
                        string finalPath = Path.Combine(wwwRootPath, permitLeavePath);

                        if (!Directory.Exists(finalPath))
                        {
                            Directory.CreateDirectory(finalPath);
                        }
                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        PermitLeaveImage permitLeaveImage = new PermitLeaveImage()
                        {
                            ImageUrl = @"\" + permitLeavePath + @"\" + fileName,
                            LeaveId = permitLeave.Id
                        };


                        if (permitLeave.PermitLeaveImages == null)
                        {
                            permitLeave.PermitLeaveImages = new List<PermitLeaveImage>();
                        }
                        permitLeave.PermitLeaveImages.Add(permitLeaveImage);
                    }
                }
                _unitOfWork.PermitLeave.Update(permitLeave);
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
        [HttpDelete("DeletePermitLeave/{leaveID:int}", Name = "DeletePermitLeave")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApiResponse> DeletePermitLeave(int leaveID)
        {
            try
            {
                if (leaveID == 0 || leaveID == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var permitLeave = _unitOfWork.PermitLeave.Get(c => c.Id == leaveID);
                if (permitLeave == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = new List<string>() { "Permit Leave Not Found!" };
                    return NotFound(_response);
                }
                string permitLeavePath = @"images\Permitleaves\PermitLeaveUser-" + permitLeave.Id;
                string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, permitLeavePath);

                if (Directory.Exists(finalPath))
                {
                    string[] filePaths = Directory.GetFiles(finalPath);
                    foreach (string filePath in filePaths)
                    {
                        System.IO.File.Delete(filePath);
                    }
                    Directory.Delete(finalPath);
                }

                _unitOfWork.PermitLeave.Remove(permitLeave);
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
        [HttpDelete("DeleteImage/{imageId:int}", Name = "DeleteImage")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApiResponse> DeleteImage(int imageId)
        {
            try
            {
                var imageToBeDeleted = _unitOfWork.PermitLeaveImage.Get(pi => pi.Id == imageId);
                if (imageToBeDeleted != null)
                {
                    if (!string.IsNullOrEmpty(imageToBeDeleted.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, imageToBeDeleted.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    _unitOfWork.PermitLeaveImage.Remove(imageToBeDeleted);
                    _unitOfWork.Save();
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);
                }
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { "Image Not Found!" };
                return NotFound(_response);
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
